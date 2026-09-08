namespace Aria.Domain;

public sealed record AuthorizedContext
{
    private AuthorizedContext(
        SubjectId subject,
        DomainId domain,
        string purpose,
        IReadOnlyDictionary<string, string> values,
        AuthorizationGrant grant)
    {
        Subject = subject;
        Domain = domain;
        Purpose = purpose;
        Values = values;
        Grant = grant;
    }

    public SubjectId Subject { get; }
    public DomainId Domain { get; }
    public string Purpose { get; }
    public IReadOnlyDictionary<string, string> Values { get; }
    public AuthorizationGrant Grant { get; }

    public static AuthorizedContext Create(
        GovernedRequest request,
        AuthorizationGrant grant,
        IReadOnlyDictionary<string, string> values)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(grant);
        ArgumentNullException.ThrowIfNull(values);

        if (!grant.AppliesTo(request))
        {
            throw new ArgumentException("The authorization grant does not apply to the request.", nameof(grant));
        }

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
