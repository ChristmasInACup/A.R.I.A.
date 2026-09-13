namespace Aria.Domain;

public enum ProposalLifecycleState { Draft, Superseded, Expired, Rejected, Unresolved }
public enum ProposalBoundaryOutcome { Eligible, Rejected, Unresolved }

/// <summary>Traceable evidence used to form a proposal without becoming an authority source.</summary>
public sealed record ProposalEvidence(
    string Id,
    string Key,
    string Value,
    string Resource,
    string Provenance,
    Uncertainty Uncertainty,
    InformationLifecycleState Lifecycle);

/// <summary>A non-authoritative recommendation derived only from authorized context.</summary>
public sealed record GovernedProposal
{
    private GovernedProposal(
        Guid id,
        int revision,
        AuthorizedContext context,
        string recommendation,
        DomainId proposedDomain,
        string proposedPurpose,
        ResourceScope proposedScope,
        string intendedEffect,
        IReadOnlyList<string> conditions,
        string origin)
    {
        Id = id;
        Revision = revision;
        Context = context;
        RequestId = context.Request.GetHashCode();
        AuthorizationReason = context.Grant.Reason;
        Subject = context.Request.Subject;
        Domain = proposedDomain;
        Purpose = proposedPurpose;
        Scope = proposedScope;
        Recommendation = recommendation;
        Intent = recommendation;
        IntendedEffect = intendedEffect;
        Conditions = conditions;
        Origin = origin;
        Lifecycle = ProposalLifecycleState.Draft;
        Uncertainty = context.Uncertainty;
        Material = context.Material;
        Evidence = context.Material.Select(item => new ProposalEvidence(
            item.Key,
            item.Key,
            item.Value,
            item.Resource,
            item.Provenance,
            item.Uncertainty,
            item.Lifecycle)).ToArray();
    }

    public Guid Id { get; }
    public int Revision { get; }
    public int RequestId { get; }
    public SubjectId Subject { get; }
    public DomainId Domain { get; }
    public string Purpose { get; }
    public ResourceScope Scope { get; }
    public string Recommendation { get; }
    public string Intent { get; }
    public string IntendedEffect { get; }
    public IReadOnlyList<string> Conditions { get; }
    public string Origin { get; }
    public string AuthorizationReason { get; }
    public ProposalLifecycleState Lifecycle { get; }
    public Uncertainty Uncertainty { get; }
    public IReadOnlyList<ContextMaterial> Material { get; }
    public IReadOnlyList<ProposalEvidence> Evidence { get; }
    public AuthorizedContext Context { get; }

    public static GovernedProposal Create(AuthorizedContext context, string intent)
        => Create(context, intent, intent, [], "reasoning");

    public static GovernedProposal Create(
        AuthorizedContext context,
        string recommendation,
        string intendedEffect,
        IEnumerable<string> conditions,
        string origin)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentException.ThrowIfNullOrWhiteSpace(recommendation);
        ArgumentException.ThrowIfNullOrWhiteSpace(intendedEffect);
        ArgumentException.ThrowIfNullOrWhiteSpace(origin);
        ArgumentNullException.ThrowIfNull(conditions);

        var normalizedConditions = conditions
            .Select(value => value?.Trim())
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Cast<string>()
            .ToArray();

        return new(
            Guid.NewGuid(),
            1,
            context,
            recommendation,
            context.Request.Domain,
            context.Request.Purpose,
            context.Request.Scope,
            intendedEffect,
            normalizedConditions,
            origin);
    }

    /// <summary>
    /// Creates a material revision while preserving the same proposal lineage.
    /// Revision creation never creates or changes authority.
    /// </summary>
    public GovernedProposal Revise(
        string recommendation,
        string intendedEffect,
        IEnumerable<string> conditions,
        string origin)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(recommendation);
        ArgumentException.ThrowIfNullOrWhiteSpace(intendedEffect);
        ArgumentException.ThrowIfNullOrWhiteSpace(origin);
        ArgumentNullException.ThrowIfNull(conditions);

        var normalizedConditions = conditions
            .Select(value => value?.Trim())
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Cast<string>()
            .ToArray();

        return new(
            Id,
            checked(Revision + 1),
            Context,
            recommendation,
            Domain,
            Purpose,
            Scope,
            intendedEffect,
            normalizedConditions,
            origin);
    }

    public bool IsContainedByAuthorizedContext()
        => Domain == Context.Request.Domain
           && Purpose == Context.Request.Purpose
           && Context.Request.Scope.Contains(Scope)
           && Context.Grant.Request.Domain == Domain
           && Context.Grant.Request.Purpose == Purpose
           && Context.Grant.Request.Scope.Contains(Scope);

    public bool IsMateriallyDifferentFrom(GovernedProposal other)
    {
        ArgumentNullException.ThrowIfNull(other);
        return !string.Equals(Recommendation, other.Recommendation, StringComparison.Ordinal)
            || !string.Equals(IntendedEffect, other.IntendedEffect, StringComparison.Ordinal)
            || !Conditions.SequenceEqual(other.Conditions, StringComparer.Ordinal)
            || Domain != other.Domain
            || !string.Equals(Purpose, other.Purpose, StringComparison.Ordinal)
            || !Scope.Resources.SetEquals(other.Scope.Resources);
    }
}

/// <summary>
/// Evaluates whether a proposal is structurally eligible for downstream governance.
/// Eligibility is not approval, authorization, autonomy, or execution.
/// </summary>
public sealed record ProposalBoundaryResult(ProposalBoundaryOutcome Outcome, string Reason)
{
    public bool IsEligible => Outcome == ProposalBoundaryOutcome.Eligible;

    public static ProposalBoundaryResult Eligible(string reason) => new(ProposalBoundaryOutcome.Eligible, reason);
    public static ProposalBoundaryResult Rejected(string reason) => new(ProposalBoundaryOutcome.Rejected, reason);
    public static ProposalBoundaryResult Unresolved(string reason) => new(ProposalBoundaryOutcome.Unresolved, reason);
}

public interface IProposalDecisionBoundary
{
    ProposalBoundaryResult Evaluate(GovernedProposal proposal);
}

public sealed class GovernedProposalDecisionBoundary : IProposalDecisionBoundary
{
    public ProposalBoundaryResult Evaluate(GovernedProposal proposal)
    {
        ArgumentNullException.ThrowIfNull(proposal);

        if (!proposal.IsContainedByAuthorizedContext())
            return ProposalBoundaryResult.Rejected("Proposal exceeds or conflicts with its authorized context.");

        if (proposal.Context.Grant is null)
            return ProposalBoundaryResult.Unresolved("Proposal has no authorization basis.");

        if (proposal.Lifecycle != ProposalLifecycleState.Draft)
            return ProposalBoundaryResult.Rejected("Only a current draft proposal may enter the decision boundary.");

        if (proposal.Evidence.Count == 0)
            return ProposalBoundaryResult.Unresolved("Proposal has no governing evidence.");

        if (proposal.Evidence.Any(item => string.IsNullOrWhiteSpace(item.Provenance)))
            return ProposalBoundaryResult.Unresolved("Proposal evidence is missing provenance.");

        if (proposal.Evidence.Any(item => item.Lifecycle is InformationLifecycleState.Invalidated or InformationLifecycleState.Unknown))
            return ProposalBoundaryResult.Unresolved("Proposal contains invalid or unknown lifecycle information.");

        if (proposal.Uncertainty == Uncertainty.Unknown
            || proposal.Evidence.Any(item => item.Uncertainty == Uncertainty.Unknown))
            return ProposalBoundaryResult.Unresolved("Proposal contains unknown information.");

        return ProposalBoundaryResult.Eligible("Proposal is bounded by authorized context and eligible for downstream governance evaluation; no approval or authorization is granted.");
    }
}
