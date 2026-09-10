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
    [Fact] public void Delegation_must_not_exceed_active_delegator_authority() { var delegator = Authority(scope: ResourceScope.For("case-a"), mayDelegate: true); var delegateAuthority = Authority(scope: ResourceScope.For("case-a", "case-b"), delegator: delegator); var result = Processor(authorities: [delegateAuthority]).Process(Request(), "Review."); Assert.False(result.IsAllowed); Assert.Null(result.Proposal); }
    [Fact] public void Delegation_requires_explicit_delegation_permission() { var delegator = Authority(mayDelegate: false); var result = Processor(authorities: [Authority(delegator: delegator)]).Process(Request(), "Review."); Assert.False(result.IsAllowed); }
    [Fact] public void Authorization_is_evaluated_before_context_assembly() { var processor = new GovernedRequestProcessor(new Resolver(IdentityContext.Resolved(new SubjectId("subject-1"))), new FixedEvaluator(AuthorizationDecision.Deny(AuthRequest(), "Denied.")), new ThrowingAssembler()); var result = processor.Process(Request(), "Review."); Assert.False(result.IsAllowed); }
    [Fact] public void Context_is_minimum_necessary_and_preserves_metadata() { var result = Processor(material: [Material(), Material(resource: "other"), Material(domain: new DomainId("other")), Material(purpose: "export")]).Process(Request(), "Review."); var item = Assert.Single(result.Proposal!.Material); Assert.Equal("source-a", item.Provenance); Assert.Equal(Sensitivity.Confidential, item.Sensitivity); Assert.Equal(Uncertainty.Uncertain, result.Proposal.Uncertainty); }
    [Fact] public void Proposal_cannot_create_authority() { var result = Processor(authorities: []).Process(Request(), "Review."); Assert.Null(result.Proposal); Assert.False(result.IsAllowed); }
    [Fact] public void Changed_authorization_conditions_change_proposal_availability() { Assert.True(Processor().Process(Request(), "Review.").IsAllowed); Assert.False(Processor(authorities: [Authority(revoked: true)]).Process(Request(), "Review.").IsAllowed); }

    private static GovernedRequest Request() => GovernedRequest.Create(new SubjectId("subject-1"), new DomainId("domain-1"), "review", ResourceScope.For("case-a"));
    private static AuthorizationRequest AuthRequest() => new(new SubjectId("subject-1"), new DomainId("domain-1"), "review", ResourceScope.For("case-a"));
    private static PolicyRule Policy(bool available = true, PolicyEffect effect = PolicyEffect.Allow) => new("policy", new DomainId("domain-1"), "review", ResourceScope.For("case-a"), effect, available);
    private static AuthorityRecord Authority(SubjectId? holder = null, DomainId? domain = null, string purpose = "review", ResourceScope? scope = null, DateTimeOffset? expiresAt = null, bool revoked = false, AuthorityRecord? delegator = null, bool mayDelegate = false) => new(Guid.NewGuid().ToString(), holder ?? new SubjectId("subject-1"), domain ?? new DomainId("domain-1"), purpose, scope ?? ResourceScope.For("case-a"), Now.AddHours(-1), expiresAt ?? Now.AddHours(1), revoked, delegator, mayDelegate);
    private static ContextMaterial Material(string resource = "case-a", DomainId? domain = null, string purpose = "review") => new("summary", "content", resource, domain ?? new DomainId("domain-1"), purpose, Sensitivity.Confidential, "source-a", Uncertainty.Uncertain);
    private static GovernedRequestProcessor Processor(IdentityContext? identity = null, IEnumerable<PolicyRule>? policies = null, IEnumerable<AuthorityRecord>? authorities = null, IEnumerable<ContextMaterial>? material = null) => new(new Resolver(identity ?? IdentityContext.Resolved(new SubjectId("subject-1"))), new EffectiveAuthorityEvaluator(policies ?? [Policy()], authorities ?? [Authority()], () => Now), new MinimumNecessaryContextAssembler(material ?? [Material()]));
    private sealed class Resolver(IdentityContext identity) : IIdentityResolver { public IdentityContext Resolve(GovernedRequest request) => identity; }
    private sealed class FixedEvaluator(AuthorizationDecision decision) : IAuthorizationEvaluator { public AuthorizationDecision Evaluate(AuthorizationRequest request) => decision; }
    private sealed class ThrowingAssembler : IContextAssembler { public ContextAssemblyResult Assemble(GovernedRequest request, AuthorizationGrant authorization) => throw new Xunit.Sdk.XunitException("Context assembly must not occur."); }
}
