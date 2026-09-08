namespace Aria.Domain;

public sealed record AuthorizedContext(
    SubjectId Subject,
    DomainId Domain,
    string Purpose,
    IReadOnlyDictionary<string, string> Values)
{
    private AuthorizedContext(
        SubjectId subject,
        DomainId domain,
        string purpose,
        IReadOnlyDictionary<string, string> values,
        AuthorizationGrant grant)
        : this(subject, domain, purpose, values)
    {
        Grant = grant;
    }

    public AuthorizationGrant Grant { get; }

    internal static AuthorizedContext Create(
        GovernedRequest request,
        AuthorizationGrant grant,
        IReadOnlyDictionary<string, string> values)
    {
        ArgumentNullException.ThrowIfNull(grant);
        ArgumentNullException.ThrowIfNull(values);

        var snapshot = new Dictionary<string, string>(values);
        return new AuthorizedContext(
            request.Subject,
            request.Domain,
            request.Purpose,
            new System.Collections.ObjectModel.ReadOnlyDictionary<string, string>(snapshot),
            grant);
    }
}

public interface IContextAssembler
{
    AuthorizedContext Assemble(GovernedRequest request, AuthorizationGrant authorization);
}
