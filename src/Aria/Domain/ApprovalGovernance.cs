namespace Aria.Domain;

public enum ApprovalRequirement
{
    NotRequired,
    Required
}

public enum ApprovalState
{
    Missing,
    Approved,
    Denied,
    Invalid,
    Expired,
    Revoked,
    Unverifiable
}

public enum ExecutionMode
{
    Human,
    Autonomous
}

public enum AutonomyState
{
    NotApplicable,
    Permitted,
    Disabled,
    Revoked,
    Restricted,
    Unverifiable
}

public enum FinalGovernanceOutcome
{
    Permitted,
    Blocked
}

/// <summary>Immutable approval evidence bound to one exact governed proposal and authorization basis.</summary>
public sealed record ApprovalEvidence
{
    private ApprovalEvidence(
        ApprovalState state,
        SubjectId? approver,
        Guid proposalId,
        int proposalRevision,
        SubjectId subject,
        DomainId domain,
        string purpose,
        ResourceScope scope,
        string recommendation,
        string intendedEffect,
        IReadOnlyList<string> conditions,
        string materialInputFingerprint,
        string authorityId,
        DateTimeOffset validFrom,
        DateTimeOffset expiresAt)
    {
        State = state;
        Approver = approver;
        ProposalId = proposalId;
        ProposalRevision = proposalRevision;
        Subject = subject;
        Domain = domain;
        Purpose = purpose;
        Scope = scope;
        Recommendation = recommendation;
        IntendedEffect = intendedEffect;
        Conditions = conditions;
        MaterialInputFingerprint = materialInputFingerprint;
        AuthorityId = authorityId;
        ValidFrom = validFrom;
        ExpiresAt = expiresAt;
    }

    public ApprovalState State { get; }
    public SubjectId? Approver { get; }
    public Guid ProposalId { get; }
    public int ProposalRevision { get; }
    public SubjectId Subject { get; }
    public DomainId Domain { get; }
    public string Purpose { get; }
    public ResourceScope Scope { get; }
    public string Recommendation { get; }
    public string IntendedEffect { get; }
    public IReadOnlyList<string> Conditions { get; }
    public string MaterialInputFingerprint { get; }
    public string AuthorityId { get; }
    public DateTimeOffset ValidFrom { get; }
    public DateTimeOffset ExpiresAt { get; }

    public static ApprovalEvidence Approved(GovernedProposal proposal, SubjectId approver, DateTimeOffset validFrom, DateTimeOffset expiresAt)
    {
        ArgumentNullException.ThrowIfNull(proposal);
        if (expiresAt <= validFrom) throw new ArgumentException("Approval validity must have a positive window.", nameof(expiresAt));

        return new(
            ApprovalState.Approved,
            approver,
            proposal.Id,
            proposal.Revision,
            proposal.Subject,
            proposal.Domain,
            proposal.Purpose,
            proposal.Scope,
            proposal.Recommendation,
            proposal.IntendedEffect,
            proposal.Conditions.ToArray(),
            MaterialInputFingerprint(proposal),
            proposal.Context.Grant.Authority.Id,
            validFrom,
            expiresAt);
    }

    public static ApprovalEvidence Unverifiable(GovernedProposal proposal)
    {
        ArgumentNullException.ThrowIfNull(proposal);
        return new(
            ApprovalState.Unverifiable,
            null,
            proposal.Id,
            proposal.Revision,
            proposal.Subject,
            proposal.Domain,
            proposal.Purpose,
            proposal.Scope,
            proposal.Recommendation,
            proposal.IntendedEffect,
            proposal.Conditions.ToArray(),
            MaterialInputFingerprint(proposal),
            proposal.Context.Grant.Authority.Id,
            DateTimeOffset.MinValue,
            DateTimeOffset.MaxValue);
    }

    public bool IsCurrentlyValidAt(DateTimeOffset now)
        => State == ApprovalState.Approved
           && ValidFrom <= now
           && now < ExpiresAt;

    public bool IsBoundTo(GovernedProposal proposal, string currentAuthorityId)
    {
        ArgumentNullException.ThrowIfNull(proposal);
        ArgumentException.ThrowIfNullOrWhiteSpace(currentAuthorityId);

        return ProposalId == proposal.Id
            && ProposalRevision == proposal.Revision
            && Subject == proposal.Subject
            && Domain == proposal.Domain
            && string.Equals(Purpose, proposal.Purpose, StringComparison.Ordinal)
            && Scope.Resources.SetEquals(proposal.Scope.Resources)
            && string.Equals(Recommendation, proposal.Recommendation, StringComparison.Ordinal)
            && string.Equals(IntendedEffect, proposal.IntendedEffect, StringComparison.Ordinal)
            && Conditions.SequenceEqual(proposal.Conditions, StringComparer.Ordinal)
            && string.Equals(MaterialInputFingerprint, MaterialInputFingerprint(proposal), StringComparison.Ordinal)
            && string.Equals(AuthorityId, currentAuthorityId, StringComparison.Ordinal);
    }

    private static string MaterialInputFingerprint(GovernedProposal proposal)
        => string.Join(
            "|",
            proposal.Material
                .OrderBy(item => item.Key, StringComparer.Ordinal)
                .ThenBy(item => item.Resource, StringComparer.Ordinal)
                .ThenBy(item => item.Provenance, StringComparer.Ordinal)
                .ThenBy(item => item.Value, StringComparer.Ordinal)
                .Select(item => string.Join("\u001f", item.Key, item.Value, item.Resource, item.Provenance, item.Uncertainty, item.Lifecycle)));
}

/// <summary>Explicit bounded autonomy permission. It is governance state, not a new authority source.</summary>
public sealed record AutonomyPermission
{
    private AutonomyPermission(
        AutonomyState state,
        SubjectId actor,
        Guid proposalId,
        int proposalRevision,
        SubjectId subject,
        DomainId domain,
        string purpose,
        ResourceScope scope,
        string intendedEffect,
        IReadOnlyList<string> conditions,
        string authorityId,
        DateTimeOffset validFrom,
        DateTimeOffset expiresAt)
    {
        State = state;
        Actor = actor;
        ProposalId = proposalId;
        ProposalRevision = proposalRevision;
        Subject = subject;
        Domain = domain;
        Purpose = purpose;
        Scope = scope;
        IntendedEffect = intendedEffect;
        Conditions = conditions;
        AuthorityId = authorityId;
        ValidFrom = validFrom;
        ExpiresAt = expiresAt;
    }

    public AutonomyState State { get; }
    public SubjectId Actor { get; }
    public Guid ProposalId { get; }
    public int ProposalRevision { get; }
    public SubjectId Subject { get; }
    public DomainId Domain { get; }
    public string Purpose { get; }
    public ResourceScope Scope { get; }
    public string IntendedEffect { get; }
    public IReadOnlyList<string> Conditions { get; }
    public string AuthorityId { get; }
    public DateTimeOffset ValidFrom { get; }
    public DateTimeOffset ExpiresAt { get; }

    public static AutonomyPermission Permitted(GovernedProposal proposal, SubjectId actor, DateTimeOffset validFrom, DateTimeOffset expiresAt)
    {
        ArgumentNullException.ThrowIfNull(proposal);
        if (expiresAt <= validFrom) throw new ArgumentException("Autonomy validity must have a positive window.", nameof(expiresAt));

        return new(
            AutonomyState.Permitted,
            actor,
            proposal.Id,
            proposal.Revision,
            proposal.Subject,
            proposal.Domain,
            proposal.Purpose,
            proposal.Scope,
            proposal.IntendedEffect,
            proposal.Conditions.ToArray(),
            proposal.Context.Grant.Authority.Id,
            validFrom,
            expiresAt);
    }

    public bool IsCurrentlyValidAt(DateTimeOffset now)
        => State == AutonomyState.Permitted
           && ValidFrom <= now
           && now < ExpiresAt;

    public bool IsBoundTo(GovernedProposal proposal, string currentAuthorityId, SubjectId actor)
    {
        ArgumentNullException.ThrowIfNull(proposal);
        ArgumentException.ThrowIfNullOrWhiteSpace(currentAuthorityId);

        return Actor == actor
            && ProposalId == proposal.Id
            && ProposalRevision == proposal.Revision
            && Subject == proposal.Subject
            && Domain == proposal.Domain
            && string.Equals(Purpose, proposal.Purpose, StringComparison.Ordinal)
            && Scope.Resources.SetEquals(proposal.Scope.Resources)
            && string.Equals(IntendedEffect, proposal.IntendedEffect, StringComparison.Ordinal)
            && Conditions.SequenceEqual(proposal.Conditions, StringComparer.Ordinal)
            && string.Equals(AuthorityId, currentAuthorityId, StringComparison.Ordinal);
    }
}

/// <summary>Current human-control state. Model or capability output cannot mutate this value.</summary>
public sealed record HumanControlState(bool Available, bool AutonomousOperationDisabled, bool TakeoverRequested)
{
    public static HumanControlState AvailableState() => new(true, false, false);
    public static HumanControlState Disabled() => new(true, true, false);
    public static HumanControlState Takeover() => new(true, true, true);
    public static HumanControlState Unavailable() => new(false, true, false);
}

/// <summary>Current governance inputs supplied by an authoritative governance layer.</summary>
public sealed record GovernanceState(
    ApprovalRequirement ApprovalRequirement,
    ExecutionMode ExecutionMode,
    AutonomyState AutonomyState,
    HumanControlState HumanControl,
    bool ApprovalRevoked = false)
{
    public static GovernanceState HumanApprovalRequired()
        => new(ApprovalRequirement.Required, ExecutionMode.Human, AutonomyState.NotApplicable, HumanControlState.AvailableState());

    public static GovernanceState Autonomous()
        => new(ApprovalRequirement.NotRequired, ExecutionMode.Autonomous, AutonomyState.Permitted, HumanControlState.AvailableState());
}

public sealed record FinalGovernanceResult(
    FinalGovernanceOutcome Outcome,
    string Reason,
    Guid ProposalId,
    int ProposalRevision,
    string? AuthorizationAuthorityId,
    ApprovalState ApprovalState,
    AutonomyState AutonomyState,
    bool HumanTakeoverRequested)
{
    public bool IsPermitted => Outcome == FinalGovernanceOutcome.Permitted;
    public bool IsBlocked => Outcome == FinalGovernanceOutcome.Blocked;

    public static FinalGovernanceResult Blocked(
        GovernedProposal proposal,
        string reason,
        ApprovalState approvalState = ApprovalState.Missing,
        AutonomyState autonomyState = AutonomyState.NotApplicable,
        string? authorizationAuthorityId = null,
        bool humanTakeoverRequested = false)
        => new(FinalGovernanceOutcome.Blocked, reason, proposal.Id, proposal.Revision, authorizationAuthorityId, approvalState, autonomyState, humanTakeoverRequested);

    public static FinalGovernanceResult Permitted(
        GovernedProposal proposal,
        string reason,
        ApprovalState approvalState,
        AutonomyState autonomyState,
        string authorizationAuthorityId,
        bool humanTakeoverRequested)
        => new(FinalGovernanceOutcome.Permitted, reason, proposal.Id, proposal.Revision, authorizationAuthorityId, approvalState, autonomyState, humanTakeoverRequested);
}

public interface IConsequentialGovernanceValidator
{
    FinalGovernanceResult Validate(
        GovernedProposal proposal,
        SubjectId actor,
        GovernanceState governance,
        ApprovalEvidence? approval = null,
        AutonomyPermission? autonomy = null);
}

/// <summary>Immediate pre-execution governance boundary. It validates; it never invokes or executes a capability.</summary>
public sealed class ConsequentialGovernanceValidator : IConsequentialGovernanceValidator
{
    private readonly IAuthorizationEvaluator _authorizationEvaluator;
    private readonly Func<DateTimeOffset> _clock;

    public ConsequentialGovernanceValidator(IAuthorizationEvaluator authorizationEvaluator, Func<DateTimeOffset> clock)
    {
        _authorizationEvaluator = authorizationEvaluator ?? throw new ArgumentNullException(nameof(authorizationEvaluator));
        _clock = clock ?? throw new ArgumentNullException(nameof(clock));
    }

    public FinalGovernanceResult Validate(
        GovernedProposal proposal,
        SubjectId actor,
        GovernanceState governance,
        ApprovalEvidence? approval = null,
        AutonomyPermission? autonomy = null)
    {
        ArgumentNullException.ThrowIfNull(proposal);
        ArgumentNullException.ThrowIfNull(governance);
        ArgumentNullException.ThrowIfNull(governance.HumanControl);

        var now = _clock();
        var request = new AuthorizationRequest(proposal.Subject, proposal.Domain, proposal.Purpose, proposal.Scope);
        var authorization = _authorizationEvaluator.Evaluate(request);

        if (!authorization.IsAuthorized || authorization.Grant is null)
            return FinalGovernanceResult.Blocked(proposal, $"Current authorization is not valid: {authorization.Reason}", authorizationAuthorityId: null);

        var authority = authorization.Grant.Authority;
        if (!authority.IsActiveAt(now))
            return FinalGovernanceResult.Blocked(proposal, "Authorization expired or was revoked before final validation.", authorizationAuthorityId: authority.Id);

        if (!governance.HumanControl.Available)
            return FinalGovernanceResult.Blocked(proposal, "Required human-control infrastructure is unavailable.", authorizationAuthorityId: authority.Id);

        if (governance.ExecutionMode == ExecutionMode.Autonomous)
        {
            if (governance.ApprovalRequirement == ApprovalRequirement.Required)
                return FinalGovernanceResult.Blocked(proposal, "Autonomous operation cannot satisfy a required human approval.", authorizationAuthorityId: authority.Id);

            if (governance.HumanControl.AutonomousOperationDisabled || governance.HumanControl.TakeoverRequested)
                return FinalGovernanceResult.Blocked(proposal, "Autonomous operation is disabled or under human takeover.", autonomyState: AutonomyState.Disabled, authorizationAuthorityId: authority.Id, humanTakeoverRequested: governance.HumanControl.TakeoverRequested);

            if (governance.AutonomyState != AutonomyState.Permitted)
                return FinalGovernanceResult.Blocked(proposal, "Autonomous permission is not currently permitted.", autonomyState: governance.AutonomyState, authorizationAuthorityId: authority.Id);

            if (autonomy is null)
                return FinalGovernanceResult.Blocked(proposal, "Autonomous operation requires explicit bounded autonomy evidence.", autonomyState: AutonomyState.Restricted, authorizationAuthorityId: authority.Id);

            if (autonomy.State != AutonomyState.Permitted)
                return FinalGovernanceResult.Blocked(proposal, "Autonomous permission is disabled, revoked, restricted, or unverifiable.", autonomyState: autonomy.State, authorizationAuthorityId: authority.Id);

            if (!autonomy.IsBoundTo(proposal, authority.Id, actor) || !autonomy.IsCurrentlyValidAt(now))
                return FinalGovernanceResult.Blocked(proposal, "Autonomy evidence is stale, expired, scope-substituted, or bound to a different governed action.", autonomyState: autonomy.State, authorizationAuthorityId: authority.Id);

            return FinalGovernanceResult.Permitted(proposal, "Current authorization and explicitly bounded autonomous governance state permit the action; no execution was performed.", ApprovalState.Missing, AutonomyState.Permitted, authority.Id, false);
        }

        if (governance.ApprovalRequirement == ApprovalRequirement.Required)
        {
            if (approval is null)
                return FinalGovernanceResult.Blocked(proposal, "Explicit human approval is required but missing.", authorizationAuthorityId: authority.Id);

            if (governance.ApprovalRevoked || approval.State == ApprovalState.Revoked)
                return FinalGovernanceResult.Blocked(proposal, "Required approval has been revoked.", approval.State, authorizationAuthorityId: authority.Id);

            if (approval.State != ApprovalState.Approved)
                return FinalGovernanceResult.Blocked(proposal, "Required approval is missing, denied, invalid, expired, or unverifiable.", approval.State, authorizationAuthorityId: authority.Id);

            if (!approval.IsBoundTo(proposal, authority.Id))
                return FinalGovernanceResult.Blocked(proposal, "Approval is not bound to the current governed action, revision, scope, material inputs, or authorization basis.", approval.State, authorizationAuthorityId: authority.Id);

            if (!approval.IsCurrentlyValidAt(now))
                return FinalGovernanceResult.Blocked(proposal, "Required approval is outside its validity window.", ApprovalState.Expired, authorizationAuthorityId: authority.Id);
        }

        return FinalGovernanceResult.Permitted(
            proposal,
            governance.ApprovalRequirement == ApprovalRequirement.Required
                ? "Current authorization and explicit human approval permit the action; no execution was performed."
                : "Current authorization and applicable governance checks permit the action; no execution was performed.",
            governance.ApprovalRequirement == ApprovalRequirement.Required ? ApprovalState.Approved : ApprovalState.Missing,
            AutonomyState.NotApplicable,
            authority.Id,
            governance.HumanControl.TakeoverRequested);
    }
}
