namespace Aria.Domain;

public enum Sensitivity { Public, Internal, Confidential }
public enum Uncertainty { Known, Uncertain, Unknown }
public enum InformationLifecycleState { Available, Stale, Disputed, Invalidated, Unknown }

/// <summary>Governed material is data, never an authority source.</summary>
public sealed record ContextMaterial(
    string Key,
    string Value,
    string Resource,
    DomainId Domain,
    string Purpose,
    Sensitivity Sensitivity,
    string Provenance,
    Uncertainty Uncertainty,
    SubjectId? Owner = null,
    InformationLifecycleState Lifecycle = InformationLifecycleState.Available,
    DateTimeOffset? FreshUntil = null,
    DateTimeOffset? ValidFrom = null,
    DateTimeOffset? ValidUntil = null);

/// <summary>Information whose governance metadata must survive use and transformation.</summary>
public sealed record GovernedInformation(
    string Id,
    string Key,
    string Value,
    string Resource,
    SubjectId Owner,
    DomainId Domain,
    string Purpose,
    Sensitivity Sensitivity,
    string Provenance,
    Uncertainty Uncertainty,
    InformationLifecycleState Lifecycle = InformationLifecycleState.Available,
    DateTimeOffset? FreshUntil = null,
    DateTimeOffset? ValidFrom = null,
    DateTimeOffset? ValidUntil = null)
{
    public ContextMaterial ToContextMaterial(DateTimeOffset now)
    {
        var effectiveLifecycle = Lifecycle;
        if (effectiveLifecycle == InformationLifecycleState.Available && FreshUntil is { } freshUntil && now >= freshUntil)
            effectiveLifecycle = InformationLifecycleState.Stale;

        return new(Id, Value, Resource, Domain, Purpose, Sensitivity, Provenance, Uncertainty, Owner, effectiveLifecycle, FreshUntil, ValidFrom, ValidUntil);
    }
}

public sealed record AuthorizedContext
{
    internal AuthorizedContext(GovernedRequest request, AuthorizationGrant grant, IReadOnlyList<ContextMaterial> material) => (Request, Grant, Material) = (request, grant, material);
    public GovernedRequest Request { get; }
    public AuthorizationGrant Grant { get; }
    public IReadOnlyList<ContextMaterial> Material { get; }
    public Uncertainty Uncertainty => Material.Any(item => item.Uncertainty == Uncertainty.Unknown) ? Uncertainty.Unknown : Material.Any(item => item.Uncertainty == Uncertainty.Uncertain) ? Uncertainty.Uncertain : Uncertainty.Known;
}

public sealed record ContextAssemblyResult(bool IsAssembled, string Reason, AuthorizedContext? Context)
{
    public static ContextAssemblyResult Assembled(AuthorizedContext context) => new(true, "Authorized minimum-necessary context assembled.", context);
    public static ContextAssemblyResult Denied(string reason) => new(false, reason, null);
}

public interface IContextAssembler { ContextAssemblyResult Assemble(GovernedRequest request, AuthorizationGrant authorization); }

/// <summary>Assembles context exclusively from governed information after authorization succeeds.</summary>
public sealed class GovernedKnowledgeContextAssembler : IContextAssembler
{
    private readonly IReadOnlyList<GovernedInformation> _information;
    private readonly Func<DateTimeOffset> _clock;

    public GovernedKnowledgeContextAssembler(IEnumerable<GovernedInformation> information, Func<DateTimeOffset> clock)
    {
        _information = information?.ToArray() ?? throw new ArgumentNullException(nameof(information));
        _clock = clock ?? throw new ArgumentNullException(nameof(clock));
    }

    public ContextAssemblyResult Assemble(GovernedRequest request, AuthorizationGrant authorization)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(authorization);

        if (!authorization.AppliesTo(request))
            return ContextAssemblyResult.Denied("Authorization does not apply to the requested context.");

        var now = _clock();
        if (_information.GroupBy(item => item.Id, StringComparer.Ordinal).Any(group => group.Count() > 1))
            return ContextAssemblyResult.Denied("Knowledge state is ambiguous.");

        var selected = _information
            .Where(item => IsUsableForRequest(item, request, authorization, now))
            .Select(item => item.ToContextMaterial(now))
            .ToArray();

        return selected.Length == 0
            ? ContextAssemblyResult.Denied("No governed context material is available for the request.")
            : ContextAssemblyResult.Assembled(new AuthorizedContext(request, authorization, selected));
    }

    private static bool IsUsableForRequest(
        GovernedInformation information,
        GovernedRequest request,
        AuthorizationGrant authorization,
        DateTimeOffset now)
    {
        if (string.IsNullOrWhiteSpace(information.Id)
            || string.IsNullOrWhiteSpace(information.Key)
            || string.IsNullOrWhiteSpace(information.Value)
            || string.IsNullOrWhiteSpace(information.Resource)
            || string.IsNullOrWhiteSpace(information.Provenance))
            return false;

        if (information.Domain != request.Domain
            || information.Domain != authorization.Request.Domain
            || information.Purpose != request.Purpose
            || information.Purpose != authorization.Request.Purpose
            || !request.Scope.Contains(information.Resource)
            || !authorization.Request.Scope.Contains(information.Resource))
            return false;

        if (information.Lifecycle == InformationLifecycleState.Invalidated)
            return false;

        if (information.ValidFrom is { } validFrom && now < validFrom)
            return false;

        if (information.ValidUntil is { } validUntil && now >= validUntil)
            return false;

        return true;
    }
}
