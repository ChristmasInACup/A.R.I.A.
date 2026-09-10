namespace Aria.Domain;

public sealed record AuthorizationRequest(SubjectId Subject, DomainId Domain, string Purpose, ResourceScope Scope);
public enum AuthorizationOutcome { Authorized, Denied, Unresolved }
public enum PolicyEffect { Allow, Deny }

public sealed record PolicyRule(string Id, DomainId Domain, string Purpose, ResourceScope Scope, PolicyEffect Effect, bool IsAvailable = true)
{
    public bool AppliesTo(AuthorizationRequest request) => Domain == request.Domain && Purpose == request.Purpose && Scope.Contains(request.Scope);
}

/// <summary>Authority state: explicit, bounded, revocable, and optionally delegated.</summary>
public sealed record AuthorityRecord(string Id, SubjectId Holder, DomainId Domain, string Purpose, ResourceScope Scope, DateTimeOffset EffectiveFrom, DateTimeOffset ExpiresAt, bool IsRevoked, AuthorityRecord? DelegatorAuthority = null, bool MayDelegate = false)
{
    public bool AppliesTo(AuthorizationRequest request) => Holder == request.Subject && Domain == request.Domain && Purpose == request.Purpose && Scope.Contains(request.Scope);
    public bool IsActiveAt(DateTimeOffset now) => !IsRevoked && EffectiveFrom <= now && now < ExpiresAt;
}

public sealed record AuthorizationGrant
{
    internal AuthorizationGrant(AuthorizationRequest request, AuthorityRecord authority, string reason) => (Request, Authority, Reason) = (request, authority, reason);
    public AuthorizationRequest Request { get; }
    public AuthorityRecord Authority { get; }
    public string Reason { get; }
    public bool AppliesTo(GovernedRequest request) => Request.Subject == request.Subject && Request.Domain == request.Domain && Request.Purpose == request.Purpose && Request.Scope.Contains(request.Scope);
}

public sealed record AuthorizationDecision
{
    private AuthorizationDecision(AuthorizationRequest request, AuthorizationOutcome outcome, string reason, AuthorizationGrant? grant) => (Request, Outcome, Reason, Grant) = (request, outcome, reason, grant);
    public AuthorizationRequest Request { get; }
    public AuthorizationOutcome Outcome { get; }
    public string Reason { get; }
    public AuthorizationGrant? Grant { get; }
    public bool IsAuthorized => Outcome == AuthorizationOutcome.Authorized && Grant is not null;
    public static AuthorizationDecision Allow(AuthorizationRequest request, AuthorityRecord authority, string reason) => new(request, AuthorizationOutcome.Authorized, reason, new AuthorizationGrant(request, authority, reason));
    public static AuthorizationDecision Deny(AuthorizationRequest request, string reason) => new(request, AuthorizationOutcome.Denied, reason, null);
    public static AuthorizationDecision Unresolved(AuthorizationRequest request, string reason) => new(request, AuthorizationOutcome.Unresolved, reason, null);
}

public interface IAuthorizationEvaluator { AuthorizationDecision Evaluate(AuthorizationRequest request); }

public sealed class EffectiveAuthorityEvaluator : IAuthorizationEvaluator
{
    private readonly IReadOnlyList<PolicyRule> _policies;
    private readonly IReadOnlyList<AuthorityRecord> _authorities;
    private readonly Func<DateTimeOffset> _clock;
    public EffectiveAuthorityEvaluator(IEnumerable<PolicyRule> policies, IEnumerable<AuthorityRecord> authorities, Func<DateTimeOffset> clock)
    { _policies = policies?.ToArray() ?? throw new ArgumentNullException(nameof(policies)); _authorities = authorities?.ToArray() ?? throw new ArgumentNullException(nameof(authorities)); _clock = clock ?? throw new ArgumentNullException(nameof(clock)); }

    public AuthorizationDecision Evaluate(AuthorizationRequest request)
    {
        var policies = _policies.Where(rule => rule.AppliesTo(request)).ToArray();
        if (policies.Any(rule => !rule.IsAvailable)) return AuthorizationDecision.Unresolved(request, "Applicable policy is unavailable.");
        if (policies.Length == 0 || !policies.Any(rule => rule.Effect == PolicyEffect.Allow)) return AuthorizationDecision.Deny(request, "No applicable allowing policy exists.");
        if (policies.Any(rule => rule.Effect == PolicyEffect.Deny)) return AuthorizationDecision.Deny(request, "An applicable policy denies the request.");
        var now = _clock();
        var authority = _authorities.FirstOrDefault(candidate => IsEffective(candidate, request, now, new HashSet<string>(StringComparer.Ordinal)));
        return authority is null ? AuthorizationDecision.Deny(request, "No effective authority applies to the request.") : AuthorizationDecision.Allow(request, authority, "Applicable policy and effective authority allow the request.");
    }

    private static bool IsEffective(AuthorityRecord authority, AuthorizationRequest request, DateTimeOffset now, ISet<string> visited)
    {
        if (!visited.Add(authority.Id) || !authority.AppliesTo(request) || !authority.IsActiveAt(now)) return false;
        return authority.DelegatorAuthority is null ||
            (authority.DelegatorAuthority.MayDelegate && authority.DelegatorAuthority.Domain == authority.Domain && authority.DelegatorAuthority.Purpose == authority.Purpose && authority.DelegatorAuthority.Scope.Contains(authority.Scope) && IsDelegatorEffective(authority.DelegatorAuthority, now, visited));
    }

    private static bool IsDelegatorEffective(AuthorityRecord authority, DateTimeOffset now, ISet<string> visited) =>
        visited.Add(authority.Id) && authority.IsActiveAt(now) && (authority.DelegatorAuthority is null || (authority.DelegatorAuthority.MayDelegate && authority.DelegatorAuthority.Scope.Contains(authority.Scope) && IsDelegatorEffective(authority.DelegatorAuthority, now, visited)));
}
