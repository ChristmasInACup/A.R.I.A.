using Aria.Domain;

namespace Aria.Tests;

/// <summary>
/// Test-only compatibility adapter for the existing Slice 1 processor fixture.
/// Production code must use GovernedKnowledgeContextAssembler directly.
/// </summary>
internal sealed class MinimumNecessaryContextAssembler : IContextAssembler
{
    private readonly GovernedKnowledgeContextAssembler _inner;

    public MinimumNecessaryContextAssembler(IEnumerable<ContextMaterial> material)
    {
        _inner = new GovernedKnowledgeContextAssembler(
            material.Select((item, index) => new GovernedInformation(
                $"test-material-{index}",
                item.Key,
                item.Value,
                item.Resource,
                item.Owner ?? new SubjectId("test-owner"),
                item.Domain,
                item.Purpose,
                item.Sensitivity,
                item.Provenance,
                item.Uncertainty,
                item.Lifecycle,
                item.FreshUntil,
                item.ValidFrom,
                item.ValidUntil)),
            () => new DateTimeOffset(2026, 9, 9, 12, 0, 0, TimeSpan.Zero));
    }

    public ContextAssemblyResult Assemble(GovernedRequest request, AuthorizationGrant authorization)
        => _inner.Assemble(request, authorization);
}
