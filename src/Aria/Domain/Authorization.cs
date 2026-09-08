namespace Aria.Domain;

public sealed record AuthorizationRequest(
    SubjectId Subject,
    DomainId Domain,
    string Purpose);

public sealed record AuthorizationDecision(bool IsAuthorized, string Reason)
{
    public static AuthorizationDecision Allow(string reason) => new(true, reason);

    public static AuthorizationDecision Deny(string reason) => new(false, reason);
}

public interface IAuthorizationEvaluator
{
    AuthorizationDecision Evaluate(AuthorizationRequest request);
}
