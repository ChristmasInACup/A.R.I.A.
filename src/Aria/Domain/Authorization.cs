namespace Aria.Domain;

public sealed record AuthorizationRequest(
    SubjectId Subject,
    DomainId Domain,
    string Purpose);

public sealed record AuthorizationDecision(bool IsAuthorized, string Reason)
{
    public static AuthorizationDecision Allow(string reason) => new(true, reason);

    public static AuthorizationDecision Deny(string reason) => new(false, reason);

    public bool TryCreateGrant(out AuthorizationGrant? grant)
    {
        if (!IsAuthorized)
        {
            grant = null;
            return false;
        }

        grant = AuthorizationGrant.Create(Reason);
        return true;
    }
}

public sealed record AuthorizationGrant
{
    private AuthorizationGrant(string reason) => Reason = reason;

    public string Reason { get; }

    internal static AuthorizationGrant Create(string reason) => new(reason);
}

public interface IAuthorizationEvaluator
{
    AuthorizationDecision Evaluate(AuthorizationRequest request);
}
