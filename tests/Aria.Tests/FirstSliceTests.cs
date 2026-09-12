using Aria.Domain;
using Aria.Governance;
using Xunit;

namespace Aria.Tests;

/// <summary>Traceability: CAP-01/02/05/07; EPIC-02–05; ARIA-SPEC-ID-001, AUTH-001/002, REAS-001, EXEC-001.</summary>
public sealed class FirstSliceTests
{
    private static readonly DateTimeOffset Now = new(2026, 9, 9, 12, 0, 0, TimeSpan.Zero);

    [Fact] public void End_to_end_permitted_request_produces_a_draft_proposal() { var result = Processor().Process(Request(), "Review the case."); Assert.True(result.IsAllowed); Assert.NotNull(result.Proposal); Assert.Equal(ProposalLifecycleState.Draft, result.Proposal.Lifecycle); Assert.Equal("Review the case.", result.Proposal.Intent); }
    [Fact] public void Identity_alone_does_not_authorize() { var result = Processor(authorities: []).Process(Request(), "Review."); Assert.False(result.IsAllowed); Assert.Null(result.Proposal); }
    [Theory]
    [InlineData(IdentityResolutionStatus.Unresolved)]
    [InlineData(IdentityResolutionStatus.Invalid)]
    [InlineData(IdentityResolutionStatus.Mismatched)]
    public void Invalid_or_unresolved_identity_terminates_before_authorization(IdentityResolutionStatus status)
    {
        var identity = status switch { IdentityResolutionStatus.Unresolved => IdentityContext.Unresolved(), IdentityResolutionStatus.Invalid => IdentityContext.Invalid(), _ => IdentityContext.Mismatched(new SubjectId("other")) };
        var result = Processor(identity: identity).Process(Request(), "Review."); Assert.False(result.IsAllowed); Assert.Null(result.Proposal);
    }
    [Fact] public void Missing_or_unavailable_policy_fails_closed() { var result = Processor(policies: [Policy(false)]).Process(Request(), "Review."); Assert.False(result.IsAllowed); Assert.Null(result.Proposal); Assert.Contains("unavailable", result.Reason, StringComparison.OrdinalIgnoreCase); }
    [Fact] public void Policy_deny_overrides_authority() { var result = Processor(policies: [Policy(), Policy(true, PolicyEffect.Deny)]).Process(Request(), "Review."); Assert.False(result.IsAllowed); Assert.Null(result.Proposal); }
    [Fact] public void Expired_or_revoked_authority_is_denied() { foreach (var authority in new[] { Authority(expiresAt: Now), Authority(revoked: true) }) { var result = Processor(authorities: [authority]).Process(Request(), "Review."); Assert.False(result.IsAllowed); Assert.Null(result.Proposal); } }
    [Fact] public void Purpose_domain_and_scope_mismatch_are_denied() { foreach (var authority in new[] { Authority(purpose: "export"), Authority(domain: new DomainId("other")), Authority(scope: ResourceScope.For("other")) }) { var result = Processor(authorities: [authority]).Process(Request(), "Review."); Assert.False(result.IsAllowed); Assert.Null(result.Proposal); } }

    [Fact] public void Explicit_delegation_to_correct_recipient_authorizes()
    {
        var root = Authority(holder: new SubjectId("issuer"), mayDelegate: true);
        var delegated = Authority(delegationId: "delegation-1");
        var delegation = DelegationTo(root, delegated, "delegation-1");
        var result = Processor(authorities: [delegated, root], delegations: [delegation]).Process(Request(), "Review.");
        Assert.True(result.IsAllowed);
    }

    [Fact] public void Delegation_to_different_recipient_does_not_authorize()
    {
        var root = Authority(holder: new SubjectId("issuer"), mayDelegate: true);
        var delegated = Authority(delegationId: "delegation-1");
        var delegation = DelegationTo(root, delegated, "delegation-1", recipient: new SubjectId("other"));
        var result = Processor(authorities: [delegated, root], delegations: [delegation]).Process(Request(), "Review.");
        Assert.False(result.IsAllowed);
    }

    [Fact] public void Forged_unrelated_authority_cannot_create_implicit_delegation()
    {
        var unrelatedSource = Authority(holder: new SubjectId("issuer"), mayDelegate: true);
        var delegated = Authority(delegationId: null);
        var result = Processor(authorities: [delegated, unrelatedSource]).Process(Request(), "Review.");
        Assert.False(result.IsAllowed);
    }

    [Fact] public void Delegation_cannot_exceed_source_scope()
    {
        var root = Authority(holder: new SubjectId("issuer"), scope: ResourceScope.For("case-a"), mayDelegate: true);
        var delegated = Authority(delegationId: "delegation-1", scope: ResourceScope.For("case-a", "case-b"));
        var delegation = DelegationTo(root, delegated, "delegation-1");
        var result = Processor(authorities: [delegated, root], delegations: [delegation]).Process(Request(), "Review.");
        Assert.False(result.IsAllowed);
    }

    [Fact] public void Delegation_cannot_cross_domain_or_purpose()
    {
        var root = Authority(holder: new SubjectId("issuer"), mayDelegate: true);
        foreach (var delegation in new[]
        {
            DelegationTo(root, Authority(delegationId: "domain"), "domain", domain: new DomainId("other")),
            DelegationTo(root, Authority(delegationId: "purpose"), "purpose", purpose: "export")
        })
        {
            var delegated = Authority(delegationId: delegation.Id);
            var result = Processor(authorities: [delegated, root], delegations: [delegation]).Process(Request(), "Review.");
            Assert.False(result.IsAllowed);
        }
    }

    [Fact] public void Issuer_without_MayDelegate_cannot_delegate()
    {
        var root = Authority(holder: new SubjectId("issuer"), mayDelegate: false);
        var delegated = Authority(delegationId: "delegation-1");
        var delegation = DelegationTo(root, delegated, "delegation-1");
        var result = Processor(authorities: [delegated, root], delegations: [delegation]).Process(Request(), "Review.");
        Assert.False(result.IsAllowed);
    }

    [Fact] public void Expired_or_revoked_delegation_is_denied()
    {
        var root = Authority(holder: new SubjectId("issuer"), mayDelegate: true);
        foreach (var delegation in new[]
        {
            DelegationTo(root, Authority(delegationId: "expired"), "expired", expiresAt: Now),
            DelegationTo(root, Authority(delegationId: "revoked"), "revoked", revoked: true)
        })
        {
            var delegated = Authority(delegationId: delegation.Id);
            var result = Processor(authorities: [delegated, root], delegations: [delegation]).Process(Request(), "Review.");
            Assert.False(result.IsAllowed);
        }
    }

    [Fact] public void Transitive_delegation_requires_explicit_further_delegation_permission()
    {
        var root = Authority(holder: new SubjectId("root"), mayDelegate: true);
        var middle = Authority(holder: new SubjectId("middle"), delegationId: "d1", mayDelegate: true);
        var leaf = Authority(delegationId: "d2");
        var d1 = DelegationTo(root, middle, "d1", recipient: middle.Holder, allowsFurtherDelegation: false);
        var d2 = DelegationTo(middle, leaf, "d2");
        var result = Processor(authorities: [leaf, middle, root], delegations: [d1, d2]).Process(Request(), "Review.");
        Assert.False(result.IsAllowed);
    }

    [Fact] public void Transitive_delegation_succeeds_when_each_level_is_explicit_and_bounded()
    {
        var root = Authority(holder: new SubjectId("root"), mayDelegate: true);
        var middle = Authority(holder: new SubjectId("middle"), delegationId: "d1", mayDelegate: true);
        var leaf = Authority(delegationId: "d2");
        var d1 = DelegationTo(root, middle, "d1", recipient: middle.Holder, allowsFurtherDelegation: true);
        var d2 = DelegationTo(middle, leaf, "d2");
        var result = Processor(authorities: [leaf, middle, root], delegations: [d1, d2]).Process(Request(), "Review.");
        Assert.True(result.IsAllowed);
    }

    [Fact] public void Transitive_delegation_rejects_wrong_ancestor_domain()
    {
        var root = Authority(holder: new SubjectId("root"), domain: new DomainId("other"), mayDelegate: true);
        var middle = Authority(holder: new SubjectId("middle"), delegationId: "d1", mayDelegate: true);
        var leaf = Authority(delegationId: "d2");
        var d1 = DelegationTo(root, middle, "d1", recipient: middle.Holder, allowsFurtherDelegation: true);
        var d2 = DelegationTo(middle, leaf, "d2");
        var result = Processor(authorities: [leaf, middle, root], delegations: [d1, d2]).Process(Request(), "Review.");
        Assert.False(result.IsAllowed);
    }

    [Fact] public void Transitive_delegation_rejects_wrong_ancestor_purpose()
    {
        var root = Authority(holder: new SubjectId("root"), purpose: "export", mayDelegate: true);
        var middle = Authority(holder: new SubjectId("middle"), delegationId: "d1", mayDelegate: true);
        var leaf = Authority(delegationId: "d2");
        var d1 = DelegationTo(root, middle, "d1", recipient: middle.Holder, allowsFurtherDelegation: true);
        var d2 = DelegationTo(middle, leaf, "d2");
        var result = Processor(authorities: [leaf, middle, root], delegations: [d1, d2]).Process(Request(), "Review.");
        Assert.False(result.IsAllowed);
    }

    [Fact] public void Transitive_delegation_rejects_insufficient_ancestor_scope()
    {
        var root = Authority(holder: new SubjectId("root"), scope: ResourceScope.For("other"), mayDelegate: true);
        var middle = Authority(holder: new SubjectId("middle"), delegationId: "d1", mayDelegate: true);
        var leaf = Authority(delegationId: "d2");
        var d1 = DelegationTo(root, middle, "d1", recipient: middle.Holder, allowsFurtherDelegation: true);
        var d2 = DelegationTo(middle, leaf, "d2");
        var result = Processor(authorities: [leaf, middle, root], delegations: [d1, d2]).Process(Request(), "Review.");
        Assert.False(result.IsAllowed);
    }

    [Fact] public void Delegation_cycle_or_repeated_identity_fails_closed()
    {
        var first = Authority(holder: new SubjectId("first"), delegationId: "d1", mayDelegate: true);
        var second = Authority(holder: new SubjectId("second"), delegationId: "d2", mayDelegate: true);
        var d1 = DelegationTo(second, first, "d1", recipient: first.Holder, allowsFurtherDelegation: true);
        var d2 = DelegationTo(first, second, "d2", recipient: second.Holder, allowsFurtherDelegation: true);
        var result = Processor(authorities: [first, second], delegations: [d1, d2]).Process(RequestFor(first.Holder), "Review.");
        Assert.False(result.IsAllowed);
    }

    [Fact] public void Longer_bounded_delegation_chain_remains_authorized()
    {
        var root = Authority(holder: new SubjectId("root"), mayDelegate: true);
        var a = Authority(holder: new SubjectId("a"), delegationId: "d1", mayDelegate: true);
        var b = Authority(holder: new SubjectId("b"), delegationId: "d2", mayDelegate: true);
        var leaf = Authority(delegationId: "d3");
        var d1 = DelegationTo(root, a, "d1", recipient: a.Holder, allowsFurtherDelegation: true);
        var d2 = DelegationTo(a, b, "d2", recipient: b.Holder, allowsFurtherDelegation: true);
        var d3 = DelegationTo(b, leaf, "d3");
        var result = Processor(authorities: [leaf, b, a, root], delegations: [d1, d2, d3]).Process(Request(), "Review.");
        Assert.True(result.IsAllowed);
    }

    [Fact] public void Authorization_is_evaluated_before_context_assembly() { var processor = new GovernedRequestProcessor(new Resolver(IdentityContext.Resolved(new SubjectId("subject-1"))), new FixedEvaluator(AuthorizationDecision.Deny(AuthRequest(), "Denied.")), new ThrowingAssembler()); var result = processor.Process(Request(), "Review."); Assert.False(result.IsAllowed); }
    [Fact] public void Context_is_minimum_necessary_and_preserves_metadata() { var result = Processor(material: [Material(), Material(resource: "other"), Material(domain: new DomainId("other")), Material(purpose: "export")]).Process(Request(), "Review."); var item = Assert.Single(result.Proposal!.Material); Assert.Equal("source-a", item.Provenance); Assert.Equal(Sensitivity.Confidential, item.Sensitivity); Assert.Equal(Uncertainty.Uncertain, result.Proposal.Uncertainty); }
    [Fact] public void Proposal_cannot_create_authority() { var result = Processor(authorities: []).Process(Request(), "Review."); Assert.Null(result.Proposal); Assert.False(result.IsAllowed); }
    [Fact] public void Changed_authorization_conditions_change_proposal_availability() { Assert.True(Processor().Process(Request(), "Review.").IsAllowed); Assert.False(Processor(authorities: [Authority(revoked: true)]).Process(Request(), "Review.").IsAllowed); }

    private static GovernedRequest Request() => RequestFor(new SubjectId("subject-1"));
    private static GovernedRequest RequestFor(SubjectId subject) => GovernedRequest.Create(subject, new DomainId("domain-1"), "review", ResourceScope.For("case-a"));
    private static AuthorizationRequest AuthRequest() => new(new SubjectId("subject-1"), new DomainId("domain-1"), "review", ResourceScope.For("case-a"));
    private static PolicyRule Policy(bool available = true, PolicyEffect effect = PolicyEffect.Allow) => new("policy", new DomainId("domain-1"), "review", ResourceScope.For("case-a"), effect, available);
    private static AuthorityRecord Authority(SubjectId? holder = null, DomainId? domain = null, string purpose = "review", ResourceScope? scope = null, DateTimeOffset? expiresAt = null, bool revoked = false, string? delegationId = null, bool mayDelegate = false) => new(Guid.NewGuid().ToString(), holder ?? new SubjectId("subject-1"), domain ?? new DomainId("domain-1"), purpose, scope ?? ResourceScope.For("case-a"), Now.AddHours(-1), expiresAt ?? Now.AddHours(1), revoked, delegationId, mayDelegate);
    private static Delegation DelegationTo(AuthorityRecord source, AuthorityRecord recipientAuthority, string id, SubjectId? recipient = null, DomainId? domain = null, string? purpose = null, ResourceScope? scope = null, DateTimeOffset? expiresAt = null, bool revoked = false, bool allowsFurtherDelegation = false) => new(id, source.Holder, recipient ?? recipientAuthority.Holder, source.Id, domain ?? source.Domain, purpose ?? source.Purpose, scope ?? recipientAuthority.Scope, Now.AddHours(-1), expiresAt ?? Now.AddHours(1), revoked, allowsFurtherDelegation);
    private static ContextMaterial Material(string resource = "case-a", DomainId? domain = null, string purpose = "review") => new("summary", "content", resource, domain ?? new DomainId("domain-1"), purpose, Sensitivity.Confidential, "source-a", Uncertainty.Uncertain);
    private static GovernedRequestProcessor Processor(IdentityContext? identity = null, IEnumerable<PolicyRule>? policies = null, IEnumerable<AuthorityRecord>? authorities = null, IEnumerable<ContextMaterial>? material = null, IEnumerable<Delegation>? delegations = null) => new(new Resolver(identity ?? IdentityContext.Resolved(new SubjectId("subject-1"))), new EffectiveAuthorityEvaluator(policies ?? [Policy()], authorities ?? [Authority()], () => Now, delegations), new MinimumNecessaryContextAssembler(material ?? [Material()]));
    private sealed class Resolver(IdentityContext identity) : IIdentityResolver { public IdentityContext Resolve(GovernedRequest request) => identity; }
    private sealed class FixedEvaluator(AuthorizationDecision decision) : IAuthorizationEvaluator { public AuthorizationDecision Evaluate(AuthorizationRequest request) => decision; }
    private sealed class ThrowingAssembler : IContextAssembler { public ContextAssemblyResult Assemble(GovernedRequest request, AuthorizationGrant authorization) => throw new Xunit.Sdk.XunitException("Context assembly must not occur."); }
}
