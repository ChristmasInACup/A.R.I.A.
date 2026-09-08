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

public readonly record struct SubjectId
{
    public SubjectId(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}

public readonly record struct DomainId
{
    public DomainId(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}
