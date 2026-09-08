namespace Aria.Domain;

public sealed record AuthorizationRequest(
    SubjectId Subject,
    DomainId Domain,
    string Purpose);

public sealed record AuthorizationDecision
{
    private AuthorizationDecision(
        AuthorizationRequest request,
        bool isAuthorized,
        string reason)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrWhiteSpace(reason);

        Request = request;
        IsAuthorized = isAuthorized;
        Reason = reason;
    }

    public AuthorizationRequest Request { get; }
    public bool IsAuthorized { get; }
    public string Reason { get; }

    public static AuthorizationDecision Allow(
        AuthorizationRequest request,
        string reason) =>
        new(request, true, reason);

    public static AuthorizationDecision Deny(
        AuthorizationRequest request,
        string reason) =>
        new(request, false, reason);

    public bool TryCreateGrant(out AuthorizationGrant? grant)
    {
        if (!IsAuthorized)
        {
            grant = null;
            return false;
        }

        grant = AuthorizationGrant.Create(Request, Reason);
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
