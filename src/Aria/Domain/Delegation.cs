namespace Aria.Domain;

/// <summary>Explicit, attributable grant of bounded authority from an issuer to a recipient.</summary>
public sealed record Delegation(
    string Id,
    SubjectId Issuer,
    SubjectId Recipient,
    string SourceAuthorityId,
    DomainId Domain,
    string Purpose,
    ResourceScope Scope,
    DateTimeOffset EffectiveFrom,
    DateTimeOffset ExpiresAt,
    bool IsRevoked,
    bool AllowsFurtherDelegation)
{
    public bool IsActiveAt(DateTimeOffset now)
        => !IsRevoked && EffectiveFrom <= now && now < ExpiresAt;
}
