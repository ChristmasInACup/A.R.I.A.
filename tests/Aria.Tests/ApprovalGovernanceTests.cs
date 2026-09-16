using Aria.Domain;

namespace Aria.Tests;

public sealed class ApprovalGovernanceTests
{
    private static readonly DateTimeOffset Now = new(2026, 9, 16, 22, 0, 0, TimeSpan.Zero);
    private const string Subject = "subject-1";
    private const string Domain = "domain-1";
    private const string Approver = "approver-1";
    private static readonly ResourceScope Scope = ResourceScope.For("case-a");

    private static AuthorityRecord Authority(
        ResourceScope? scope = null,
        string purpose = "review",
        DateTimeOffset? effectiveFrom = null,
        DateTimeOffset? expiresAt = null,
        bool revoked = false,
        string id = "authority-1")
        => new(id, Subject, Domain, purpose, scope ?? Scope, effectiveFrom ?? Now.AddHours(-1), expiresAt ?? Now.AddHours(1), revoked);

    private static GovernedProposal Proposal(AuthorityRecord? authority = null)
    {
        authority ??= Authority();
        var request = new AuthorizationRequest(Subject, Domain, "review", authority.Scope);
        var grant = new AuthorizationGrant(request, authority, "test");
        var context = GovernedKnowledgeContext.Create(
            request,
            grant,
            [new GovernedInformation("info-1", "fact", "value", "resource", "source", Uncertainty.Known, InformationLifecycleState.Current)],
            Now);

        return GovernedProposal.Create(context, "review", "review", ["case-a"], "review", [], "test");
    }

    private static GovernedProposal Proposal(ResourceScope scope)
    {
        var authority = Authority(scope: scope);
        return Proposal(authority);
    }

    private static ConsequentialGovernanceValidator Validator(
        AuthorityRecord authority,
        AuthorityRecord? authorityForEvaluator = null,
        Func<DateTimeOffset>? clock = null)
    {
        var evaluator = new EffectiveAuthorityEvaluator(
            [new PolicyRule("allow", Domain, "review", PolicyEffect.Allow)],
            [authorityForEvaluator ?? authority],
            [],
            clock ?? (() => Now));
        return new ConsequentialGovernanceValidator(evaluator, clock ?? (() => Now));
    }

    private static ApprovalEvidence Approval(GovernedProposal proposal)
        => ApprovalEvidence.Approved(proposal, Approver, Now.AddMinutes(-5), Now.AddMinutes(5));

    [Fact]
    public void Valid_required_approval_permits_final_governance_validation()
    {
        var proposal = Proposal();
        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), Approval(proposal));

        Assert.True(result.IsPermitted);
        Assert.Equal(FinalGovernanceOutcome.Permitted, result.Outcome);
    }

    [Fact]
    public void Missing_required_approval_blocks()
    {
        var proposal = Proposal();
        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), null);

        Assert.True(result.IsBlocked);
    }

    [Fact]
    public void Invalid_or_unverifiable_approval_blocks()
    {
        var proposal = Proposal();
        var approval = ApprovalEvidence.Unverifiable(proposal, Approver);
        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), approval);

        Assert.True(result.IsBlocked);
    }

    [Fact]
    public void Expired_approval_blocks_at_final_validation()
    {
        var proposal = Proposal();
        var approval = ApprovalEvidence.Approved(proposal, Approver, Now.AddHours(-2), Now.AddHours(-1));
        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), approval);

        Assert.True(result.IsBlocked);
    }

    [Fact]
    public void Revoked_approval_blocks_even_when_other_fields_match()
    {
        var proposal = Proposal();
        var approval = ApprovalEvidence.Approved(proposal, Approver, Now.AddMinutes(-5), Now.AddMinutes(5)) with { State = ApprovalState.Revoked };
        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), approval);

        Assert.True(result.IsBlocked);
    }

    [Fact]
    public void Approval_for_different_revision_cannot_be_replayed()
    {
        var proposal = Proposal();
        var approval = Approval(proposal);
        var revised = proposal.Revise("review revised", Domain, "review", Scope, "review", [], "test");
        var result = Validator(revised.Context.Grant.Authority).Validate(revised, Subject, GovernanceState.HumanApprovalRequired(), approval);

        Assert.True(result.IsBlocked);
    }

    [Fact]
    public void Approval_does_not_broaden_authorization()
    {
        var proposal = GovernedProposal.Create(Context(ResourceScope.For("case-a")), "review", Domain, "review", ResourceScope.For("case-a", "case-b"), "review", [], "test");
        var approval = ApprovalEvidence.Approved(proposal, Approver, Now.AddMinutes(-5), Now.AddMinutes(5));

        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), approval);

        Assert.True(result.IsBlocked);
        Assert.Contains("authorized context", result.Reason, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Approval_bound_to_old_authority_cannot_survive_authority_replacement()
    {
        var proposal = Proposal();
        var approval = Approval(proposal);
        var replacement = new AuthorityRecord("replacement-authority", Subject, Domain, "review", Scope, Now.AddHours(-1), Now.AddHours(1), false);
        var validator = Validator(replacement, authorityForEvaluator: replacement);
        var result = validator.Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), approval);

        Assert.True(result.IsBlocked);
    }

    [Fact]
    public void Current_revocation_between_stages_blocks()
    {
        var proposal = Proposal();
        var approval = Approval(proposal);
        var revoked = proposal.Context.Grant.Authority with { IsRevoked = true };
        var result = Validator(revoked, authorityForEvaluator: revoked).Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), approval);

        Assert.True(result.IsBlocked);
    }

    [Fact]
    public void Current_expiration_between_stages_blocks()
    {
        var proposal = Proposal();
        var approval = Approval(proposal);
        var expired = proposal.Context.Grant.Authority with { ExpiresAt = Now.AddMinutes(-1) };
        var result = Validator(expired, authorityForEvaluator: expired).Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), approval);

        Assert.True(result.IsBlocked);
    }

    [Fact]
    public void Not_required_approval_can_reach_final_validation_without_approval()
    {
        var proposal = Proposal();
        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.Autonomous(), null);

        Assert.True(result.IsPermitted);
    }

    [Fact]
    public void Autonomous_permission_is_explicit_and_bounded()
    {
        var proposal = Proposal();
        var autonomy = AutonomyPermission.Permitted(proposal, Subject, Now.AddMinutes(-5), Now.AddMinutes(5));
        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.Autonomous(), null, autonomy);

        Assert.True(result.IsPermitted);
    }

    [Fact]
    public void Autonomy_expiration_blocks_at_final_validation()
    {
        var proposal = Proposal();
        var autonomy = AutonomyPermission.Permitted(proposal, Subject, Now.AddHours(-2), Now.AddHours(-1));
        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.Autonomous(), null, autonomy);

        Assert.True(result.IsBlocked);
    }

    [Fact]
    public void Autonomous_operation_cannot_satisfy_required_human_approval()
    {
        var proposal = Proposal();
        var autonomy = AutonomyPermission.Permitted(proposal, Subject, Now.AddMinutes(-5), Now.AddMinutes(5));
        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), null, autonomy);

        Assert.True(result.IsBlocked);
    }

    [Fact]
    public void Disabled_autonomy_blocks_autonomous_continuation()
    {
        var proposal = Proposal();
        var autonomy = AutonomyPermission.Permitted(proposal, Subject, Now.AddMinutes(-5), Now.AddMinutes(5));
        var governance = GovernanceState.Autonomous() with { HumanControl = HumanControlState.Disabled };
        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, governance, null, autonomy);

        Assert.True(result.IsBlocked);
    }

    [Fact]
    public void Human_takeover_blocks_autonomous_continuation_and_is_explicit()
    {
        var proposal = Proposal();
        var autonomy = AutonomyPermission.Permitted(proposal, Subject, Now.AddMinutes(-5), Now.AddMinutes(5));
        var governance = GovernanceState.Autonomous() with { HumanControl = HumanControlState.Takeover };
        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, governance, null, autonomy);

        Assert.True(result.IsBlocked);
        Assert.True(result.TakeoverRequested);
    }

    [Fact]
    public void Loss_of_human_control_path_restricts_autonomous_operation()
    {
        var proposal = Proposal();
        var autonomy = AutonomyPermission.Permitted(proposal, Subject, Now.AddMinutes(-5), Now.AddMinutes(5));
        var governance = GovernanceState.Autonomous() with { HumanControl = HumanControlState.Unavailable };
        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, governance, null, autonomy);

        Assert.True(result.IsBlocked);
    }

    [Fact]
    public void Autonomy_for_different_actor_cannot_be_replayed()
    {
        var proposal = Proposal();
        var autonomy = AutonomyPermission.Permitted(proposal, "different-actor", Now.AddMinutes(-5), Now.AddMinutes(5));
        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.Autonomous(), null, autonomy);

        Assert.True(result.IsBlocked);
    }

    [Fact]
    public void Model_content_cannot_claim_approval()
    {
        var proposal = Proposal();
        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), null);

        Assert.True(result.IsBlocked);
    }

    [Fact]
    public void Proposal_revision_cannot_change_scope_beyond_authorized_context()
    {
        var proposal = Proposal();

        Assert.Throws<InvalidOperationException>(() =>
            proposal.Revise("review", Domain, "review", ResourceScope.For("case-a", "case-b"), "review", [], "test"));
    }

    [Fact]
    public void Proposal_revision_cannot_change_authorized_purpose()
    {
        var proposal = Proposal();

        Assert.Throws<InvalidOperationException>(() =>
            proposal.Revise("review", Domain, "export", Scope, "review", [], "test"));
    }

    [Fact]
    public void Validator_rejects_a_proposal_outside_its_authorized_context()
    {
        var proposal = GovernedProposal.Create(Context(), "review", Domain, "review", ResourceScope.For("case-a", "case-b"), "review", [], "test");
        var approval = ApprovalEvidence.Approved(proposal, Approver, Now.AddMinutes(-5), Now.AddMinutes(5));

        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), approval);

        Assert.True(result.IsBlocked);
        Assert.Contains("authorized context", result.Reason, StringComparison.OrdinalIgnoreCase);
    }

    private static GovernedKnowledgeContext Context(ResourceScope? scope = null)
    {
        var authority = Authority(scope: scope);
        var request = new AuthorizationRequest(Subject, Domain, "review", authority.Scope);
        var grant = new AuthorizationGrant(request, authority, "test");
        return GovernedKnowledgeContext.Create(
            request,
            grant,
            [new GovernedInformation("info-1", "fact", "value", "resource", "source", Uncertainty.Known, InformationLifecycleState.Current)],
            Now);
    }
}