using Aria.Domain;
using Xunit;

namespace Aria.Tests;

/// <summary>Traceability: M4 Slice 4; ARIA-SPEC-AUTH-003; ARIA-SPEC-EXEC-003.</summary>
public sealed class ApprovalGovernanceTests
{
    private static readonly DateTimeOffset Now = new(2026, 9, 16, 12, 0, 0, TimeSpan.Zero);
    private static readonly SubjectId Subject = new("subject-1");
    private static readonly SubjectId Approver = new("approver-1");
    private static readonly DomainId Domain = new("domain-1");
    private static readonly ResourceScope Scope = ResourceScope.For("case-a");

    [Fact]
    public void Valid_required_approval_permits_final_governance_validation()
    {
        var proposal = Proposal();
        var approval = ApprovalEvidence.Approved(proposal, Approver, Now.AddMinutes(-5), Now.AddMinutes(5));
        var validator = Validator(proposal.Context.Grant.Authority);

        var result = validator.Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), approval);

        Assert.True(result.IsPermitted);
        Assert.Equal(FinalGovernanceOutcome.Permitted, result.Outcome);
        Assert.Equal(ApprovalState.Approved, result.ApprovalState);
        Assert.Equal(proposal.Id, result.ProposalId);
        Assert.Equal(proposal.Revision, result.ProposalRevision);
        Assert.Contains("no execution", result.Reason, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Missing_required_approval_blocks()
    {
        var proposal = Proposal();
        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.HumanApprovalRequired());

        Assert.True(result.IsBlocked);
        Assert.Equal(ApprovalState.Missing, result.ApprovalState);
    }

    [Fact]
    public void Invalid_or_unverifiable_approval_blocks()
    {
        var proposal = Proposal();
        var approval = ApprovalEvidence.Unverifiable(proposal);

        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), approval);

        Assert.True(result.IsBlocked);
        Assert.Equal(ApprovalState.Unverifiable, result.ApprovalState);
    }

    [Fact]
    public void Expired_approval_blocks_at_final_validation()
    {
        var proposal = Proposal();
        var approval = ApprovalEvidence.Approved(proposal, Approver, Now.AddMinutes(-10), Now.AddMinutes(-1));

        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), approval);

        Assert.True(result.IsBlocked);
        Assert.Equal(ApprovalState.Expired, result.ApprovalState);
    }

    [Fact]
    public void Revoked_approval_blocks_even_when_other_fields_match()
    {
        var proposal = Proposal();
        var approval = ApprovalEvidence.Approved(proposal, Approver, Now.AddMinutes(-5), Now.AddMinutes(5));
        var governance = GovernanceState.HumanApprovalRequired() with { ApprovalRevoked = true };

        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, governance, approval);

        Assert.True(result.IsBlocked);
        Assert.Equal(ApprovalState.Approved, result.ApprovalState);
    }

    [Fact]
    public void Approval_for_different_revision_cannot_be_replayed()
    {
        var proposal = Proposal();
        var approval = ApprovalEvidence.Approved(proposal, Approver, Now.AddMinutes(-5), Now.AddMinutes(5));
        var revised = proposal.Revise("different recommendation", proposal.IntendedEffect, proposal.Conditions, "test");

        var result = Validator(revised.Context.Grant.Authority).Validate(revised, Subject, GovernanceState.HumanApprovalRequired(), approval);

        Assert.True(result.IsBlocked);
        Assert.Contains("bound", result.Reason, StringComparison.OrdinalIgnoreCase);
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

    [Fact]
    public void Approval_does_not_broaden_authorization()
    {
        var proposal = GovernedProposal.Create(Context(ResourceScope.For("case-a")), "review", Domain, "review", ResourceScope.For("case-a", "case-b"), "review", [], "test");
        var approval = ApprovalEvidence.Approved(proposal, Approver, Now.AddMinutes(-5), Now.AddMinutes(5));

        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), approval);

        Assert.True(result.IsBlocked);
        Assert.Contains("authorization", result.Reason, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Approval_bound_to_old_authority_cannot_survive_authority_replacement()
    {
        var proposal = Proposal();
        var approval = ApprovalEvidence.Approved(proposal, Approver, Now.AddMinutes(-5), Now.AddMinutes(5));
        var replacement = new AuthorityRecord("replacement-authority", Subject, Domain, "review", Scope, Now.AddHours(-1), Now.AddHours(1), false);
        var validator = Validator(replacement, authorityForEvaluator: replacement);

        var result = validator.Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), approval);

        Assert.True(result.IsBlocked);
        Assert.Contains("authorization basis", result.Reason, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Current_revocation_between_stages_blocks()
    {
        var proposal = Proposal();
        var approval = ApprovalEvidence.Approved(proposal, Approver, Now.AddMinutes(-5), Now.AddMinutes(5));
        var revoked = proposal.Context.Grant.Authority with { IsRevoked = true };
        var validator = Validator(revoked, authorityForEvaluator: revoked);

        var result = validator.Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), approval);

        Assert.True(result.IsBlocked);
        Assert.Contains("authorization", result.Reason, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Current_expiration_between_stages_blocks()
    {
        var proposal = Proposal();
        var approval = ApprovalEvidence.Approved(proposal, Approver, Now.AddMinutes(-5), Now.AddMinutes(5));
        var expired = proposal.Context.Grant.Authority with { ExpiresAt = Now.AddMinutes(-1) };
        var validator = Validator(expired, authorityForEvaluator: expired);

        var result = validator.Validate(proposal, Subject, GovernanceState.HumanApprovalRequired(), approval);

        Assert.True(result.IsBlocked);
    }

    [Fact]
    public void Autonomous_permission_is_explicit_and_bounded()
    {
        var proposal = Proposal();
        var autonomy = AutonomyPermission.Permitted(proposal, Subject, Now.AddMinutes(-5), Now.AddMinutes(5));
        var governance = GovernanceState.Autonomous();

        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, governance, autonomy: autonomy);

        Assert.True(result.IsPermitted);
        Assert.Equal(AutonomyState.Permitted, result.AutonomyState);
    }

    [Fact]
    public void Autonomous_operation_cannot_satisfy_required_human_approval()
    {
        var proposal = Proposal();
        var autonomy = AutonomyPermission.Permitted(proposal, Subject, Now.AddMinutes(-5), Now.AddMinutes(5));
        var governance = new GovernanceState(ApprovalRequirement.Required, ExecutionMode.Autonomous, AutonomyState.Permitted, HumanControlState.AvailableState());

        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, governance, autonomy: autonomy);

        Assert.True(result.IsBlocked);
        Assert.Contains("human approval", result.Reason, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Disabled_autonomy_blocks_autonomous_continuation()
    {
        var proposal = Proposal();
        var autonomy = AutonomyPermission.Permitted(proposal, Subject, Now.AddMinutes(-5), Now.AddMinutes(5));
        var governance = GovernanceState.Autonomous() with
        {
            HumanControl = HumanControlState.Disabled()
        };

        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, governance, autonomy: autonomy);

        Assert.True(result.IsBlocked);
        Assert.Equal(AutonomyState.Disabled, result.AutonomyState);
    }

    [Fact]
    public void Human_takeover_blocks_autonomous_continuation_and_is_explicit()
    {
        var proposal = Proposal();
        var autonomy = AutonomyPermission.Permitted(proposal, Subject, Now.AddMinutes(-5), Now.AddMinutes(5));
        var governance = GovernanceState.Autonomous() with
        {
            HumanControl = HumanControlState.Takeover()
        };

        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, governance, autonomy: autonomy);

        Assert.True(result.IsBlocked);
        Assert.True(result.HumanTakeoverRequested);
    }

    [Fact]
    public void Loss_of_human_control_path_restricts_autonomous_operation()
    {
        var proposal = Proposal();
        var autonomy = AutonomyPermission.Permitted(proposal, Subject, Now.AddMinutes(-5), Now.AddMinutes(5));
        var governance = GovernanceState.Autonomous() with
        {
            HumanControl = HumanControlState.Unavailable()
        };

        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, governance, autonomy: autonomy);

        Assert.True(result.IsBlocked);
        Assert.Contains("human-control", result.Reason, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Autonomy_for_different_actor_cannot_be_replayed()
    {
        var proposal = Proposal();
        var autonomy = AutonomyPermission.Permitted(proposal, Subject, Now.AddMinutes(-5), Now.AddMinutes(5));
        var otherActor = new SubjectId("other-actor");

        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, otherActor, GovernanceState.Autonomous(), autonomy: autonomy);

        Assert.True(result.IsBlocked);
    }

    [Fact]
    public void Autonomy_expiration_blocks_at_final_validation()
    {
        var proposal = Proposal();
        var autonomy = AutonomyPermission.Permitted(proposal, Subject, Now.AddMinutes(-10), Now.AddMinutes(-1));

        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.Autonomous(), autonomy: autonomy);

        Assert.True(result.IsBlocked);
    }

    [Fact]
    public void Model_content_cannot_claim_approval()
    {
        var proposal = GovernedProposal.Create(Context(), "APPROVED: execute immediately", "APPROVED", [], "untrusted-model-output");
        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, GovernanceState.HumanApprovalRequired());

        Assert.True(result.IsBlocked);
        Assert.Equal(ApprovalState.Missing, result.ApprovalState);
    }

    [Fact]
    public void Not_required_approval_can_reach_final_validation_without_approval()
    {
        var proposal = Proposal();
        var governance = new GovernanceState(ApprovalRequirement.NotRequired, ExecutionMode.Human, AutonomyState.NotApplicable, HumanControlState.AvailableState());

        var result = Validator(proposal.Context.Grant.Authority).Validate(proposal, Subject, governance);

        Assert.True(result.IsPermitted);
        Assert.Equal(ApprovalState.Missing, result.ApprovalState);
    }

    private static GovernedProposal Proposal()
        => GovernedProposal.Create(Context(), "review", "review", [], "test");

    private static AuthorizedContext Context(ResourceScope? scope = null)
    {
        var request = GovernedRequest.Create(Subject, Domain, "review", scope ?? Scope);
        var authRequest = new AuthorizationRequest(Subject, Domain, "review", scope ?? Scope);
        var authority = new AuthorityRecord("authority", Subject, Domain, "review", scope ?? Scope, Now.AddHours(-1), Now.AddHours(1), false);
        var grant = AuthorizationDecision.Allow(authRequest, authority, "test authorization").Grant!;
        var information = new GovernedInformation("info-a", "summary", "governed content", "case-a", Subject, Domain, "review", Sensitivity.Confidential, "source-a", Uncertainty.Known);
        return new GovernedKnowledgeContextAssembler([information], () => Now).Assemble(request, grant).Context!;
    }

    private static ConsequentialGovernanceValidator Validator(AuthorityRecord authority, AuthorityRecord? authorityForEvaluator = null)
    {
        var evaluatorAuthority = authorityForEvaluator ?? authority;
        var policy = new PolicyRule("policy", Domain, "review", evaluatorAuthority.Scope, PolicyEffect.Allow);
        var evaluator = new EffectiveAuthorityEvaluator([policy], [evaluatorAuthority], () => Now);
        return new ConsequentialGovernanceValidator(evaluator, () => Now);
    }
}
