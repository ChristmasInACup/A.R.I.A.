namespace Aria.Domain;

public sealed record AuthorizationRequest(
    SubjectId Subject,
    DomainId Domain,
    string Purpose);

public sealed record AuthorizationDecision(bool IsAuthorized, string Reason)
{
    public static AuthorizationDecision Allow(string reason) => new(true, reason);

    public static AuthorizationDecision Deny(string reason) => new(false, reason);

    public bool TryCreateGrant(
        AuthorizationRequest request,
        out AuthorizationGrant? grant)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!IsAuthorized)
        {
            grant = null;
            return false;
        }

        grant = AuthorizationGrant.Create(request, Reason);
        return true;
    }
}

public sealed record AuthorizationGrant
{
    private AuthorizationGrant(
        SubjectId subject,
        DomainId domain,
        string purpose,
        string reason)
    {
        Subject = subject;
        Domain = domain;
        Purpose = purpose;
        Reason = reason;
    }

    public SubjectId Subject { get; }
    public DomainId Domain { get; }
    public string Purpose { get; }
    public string Reason { get; }

    internal static AuthorizationGrant Create(AuthorizationRequest request, string reason) =>
        new(request.Subject, request.Domain, request.Purpose, reason);

    public bool AppliesTo(GovernedRequest request) =>
        request.Subject == Subject &&
        request.Domain == Domain &&
        request.Purpose == Purpose;
}

public interface IAuthorizationEvaluator
{
    AuthorizationDecision Evaluate(AuthorizationRequest request);
}
