namespace Aria.Domain;

public sealed record AuthorizedContext(
    SubjectId Subject,
    DomainId Domain,
    string Purpose,
    IReadOnlyDictionary<string, string> Values)
{
    public static AuthorizedContext Create(
        SubjectId subject,
        DomainId domain,
        string purpose,
        IReadOnlyDictionary<string, string> values)
    {
        ArgumentNullException.ThrowIfNull(values);
        return new AuthorizedContext(subject, domain, purpose, new Dictionary<string, string>(values));
    }
}

public interface IContextAssembler
{
    AuthorizedContext Assemble(GovernedRequest request, AuthorizationDecision authorization);
}
