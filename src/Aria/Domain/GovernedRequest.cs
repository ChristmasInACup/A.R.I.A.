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
    public SubjectId(string value) : this()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        Value = value;
    }

    public override string ToString() => Value;
}

public readonly record struct DomainId(string Value)
{
    public DomainId(string value) : this()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        Value = value;
    }

    public override string ToString() => Value;
}
