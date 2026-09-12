using Aria.Domain;
using Xunit;

namespace Aria.Tests;

public sealed class DelegationConditionTests
{
    private static readonly DateTimeOffset Now = new(2026, 9, 9, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Unsupported_delegation_conditions_fail_closed()
    {
        var source = new AuthorityRecord(
            "root",
            new SubjectId("issuer"),
            new DomainId("domain-1"),
            "review",
            ResourceScope.For("case-a"),
            Now.AddHours(-1),
            Now.AddHours(1),
            false,
            MayDelegate: true);

        var delegated = new AuthorityRecord(
            "delegated",
            new SubjectId("subject-1"),
            new DomainId("domain-1"),
            "review",
            ResourceScope.For("case-a"),
            Now.AddHours(-1),
            Now.AddHours(1),
            false,
            DelegationId: "d1");

        var delegation = new Delegation(
            "d1",
            source.Holder,
            delegated.Holder,
            source.Id,
            source.Domain,
            source.Purpose,
            delegated.Scope,
            Now.AddHours(-1),
            Now.AddHours(1),
            false,
            false,
            Provenance: "test",
            Conditions: ["requires-unimplemented-condition"]);

        var evaluator = new EffectiveAuthorityEvaluator(
            [new PolicyRule("policy", source.Domain, source.Purpose, source.Scope, PolicyEffect.Allow)],
            [delegated, source],
            () => Now,
            [delegation]);

        var result = evaluator.Evaluate(new AuthorizationRequest(
            delegated.Holder,
            source.Domain,
            source.Purpose,
            delegated.Scope));

        Assert.False(result.IsAuthorized);
    }
}
