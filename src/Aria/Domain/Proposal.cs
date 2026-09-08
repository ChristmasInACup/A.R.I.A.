namespace Aria.Domain;

public sealed record GovernedProposal(
    SubjectId Subject,
    DomainId Domain,
    string Purpose,
    AuthorizedContext Context,
    string Intent)
{
    public static GovernedProposal Create(AuthorizedContext context, string intent)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(intent);
        return new GovernedProposal(
            context.Subject,
            context.Domain,
            context.Purpose,
            context,
            intent);
    }
}
