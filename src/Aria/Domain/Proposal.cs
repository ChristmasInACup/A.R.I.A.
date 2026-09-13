namespace Aria.Domain;

public enum ProposalLifecycleState { Draft, Superseded, Expired, Rejected, Unresolved }

/// <summary>Traceable evidence used to form a proposal without becoming an authority source.</summary>
public sealed record ProposalEvidence(
    string Id,
    string Key,
    string Value,
    string Resource,
    string Provenance,
    Uncertainty Uncertainty,
    InformationLifecycleState Lifecycle);

/// <summary>A non-authoritative recommendation derived from authorized context.</summary>
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
        RequestFingerprint = BuildRequestFingerprint(context.Request);
        AuthorizationReason = context.Grant.Reason;
        Subject = context.Request.Subject;
        Domain = proposedDomain;
        Purpose = proposedPurpose;
        Scope = proposedScope;
        Recommendation = recommendation;
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
    public string RequestFingerprint { get; }
    public SubjectId Subject { get; }
    public DomainId Domain { get; }
    public string Purpose { get; }
    public ResourceScope Scope { get; }
    public string Recommendation { get; }
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
        => Create(context, recommendation, context.Request.Domain, context.Request.Purpose, context.Request.Scope, intendedEffect, conditions, origin);

    public static GovernedProposal Create(
        AuthorizedContext context,
        string recommendation,
        DomainId proposedDomain,
        string proposedPurpose,
        ResourceScope proposedScope,
        string intendedEffect,
        IEnumerable<string> conditions,
        string origin)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentException.ThrowIfNullOrWhiteSpace(recommendation);
        ArgumentException.ThrowIfNullOrWhiteSpace(proposedPurpose);
        ArgumentNullException.ThrowIfNull(proposedScope);
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
            proposedDomain,
            proposedPurpose,
            proposedScope,
            intendedEffect,
            normalizedConditions,
            origin);
    }

    /// <summary>Creates a material revision while preserving proposal lineage.</summary>
    public GovernedProposal Revise(
        string recommendation,
        string intendedEffect,
        IEnumerable<string> conditions,
        string origin)
        => Revise(recommendation, Domain, Purpose, Scope, intendedEffect, conditions, origin);

    public GovernedProposal Revise(
        string recommendation,
        DomainId proposedDomain,
        string proposedPurpose,
        ResourceScope proposedScope,
        string intendedEffect,
        IEnumerable<string> conditions,
        string origin)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(recommendation);
        ArgumentException.ThrowIfNullOrWhiteSpace(proposedPurpose);
        ArgumentNullException.ThrowIfNull(proposedScope);
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
            proposedDomain,
            proposedPurpose,
            proposedScope,
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

    private static string BuildRequestFingerprint(GovernedRequest request)
        => string.Join(
            "|",
            request.Subject.Value,
            request.Domain.Value,
            request.Purpose,
            string.Join(",", request.Scope.Resources.Order(StringComparer.Ordinal)));
}
