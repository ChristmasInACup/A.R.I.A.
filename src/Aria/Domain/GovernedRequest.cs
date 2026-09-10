namespace Aria.Domain;

public sealed record ResourceScope
{
    private ResourceScope(IReadOnlySet<string> resources) => Resources = resources;
    public IReadOnlySet<string> Resources { get; }

    public static ResourceScope For(params string[] resources)
    {
        ArgumentNullException.ThrowIfNull(resources);
        var set = resources.Select(value => value?.Trim()).Where(value => !string.IsNullOrWhiteSpace(value)).Cast<string>().ToHashSet(StringComparer.Ordinal);
        if (set.Count == 0 || set.Count != resources.Length)
            throw new ArgumentException("A scope requires non-empty resources.", nameof(resources));
        return new ResourceScope(set);
    }

    public bool Contains(ResourceScope requested) => requested.Resources.IsSubsetOf(Resources);
    public bool Contains(string resource) => Resources.Contains(resource);
}

public sealed record GovernedRequest(SubjectId Subject, DomainId Domain, string Purpose, ResourceScope Scope)
{
    public static GovernedRequest Create(SubjectId subject, DomainId domain, string purpose, ResourceScope scope)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(purpose);
        ArgumentNullException.ThrowIfNull(scope);
        return new(subject, domain, purpose, scope);
    }
}

public readonly record struct SubjectId
{
    public SubjectId(string value) { ArgumentException.ThrowIfNullOrWhiteSpace(value); Value = value; }
    public string Value { get; }
    public override string ToString() => Value;
}

public readonly record struct DomainId
{
    public DomainId(string value) { ArgumentException.ThrowIfNullOrWhiteSpace(value); Value = value; }
    public string Value { get; }
    public override string ToString() => Value;
}
