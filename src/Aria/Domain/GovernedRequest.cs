namespace Aria.Domain;

public sealed record GovernedRequest(
    SubjectId Subject,
    DomainId Domain,
    string Purpose)
{
    public static GovernedRequest Create(SubjectId subject, DomainId domain, string purpose)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(purpose);
        return new GovernedRequest(subject, domain, purpose);
    }
}

public readonly record struct SubjectId(string Value)
{
    public override string ToString() => Value;
}

public readonly record struct DomainId(string Value)
{
    public override string ToString() => Value;
}
