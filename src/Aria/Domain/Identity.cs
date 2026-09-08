namespace Aria.Domain;

public sealed record IdentityContext(SubjectId Subject, bool IsResolved)
{
    public static IdentityContext Resolved(SubjectId subject) => new(subject, true);

    public static IdentityContext Unresolved() => new(default, false);
}

public interface IIdentityResolver
{
    IdentityContext Resolve(GovernedRequest request);
}
