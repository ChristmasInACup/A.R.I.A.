namespace Aria.Domain;

public enum ProposalBoundaryOutcome { Eligible, Rejected, Unresolved }

/// <summary>Result of structural governance evaluation. Eligibility is not approval, authorization, autonomy, or execution.</summary>
public sealed record ProposalBoundaryResult(ProposalBoundaryOutcome Outcome, string Reason)
{
    public bool IsEligible => Outcome == ProposalBoundaryOutcome.Eligible;
    public static ProposalBoundaryResult Eligible(string reason) => new(ProposalBoundaryOutcome.Eligible, reason);
    public static ProposalBoundaryResult Rejected(string reason) => new(ProposalBoundaryOutcome.Rejected, reason);
    public static ProposalBoundaryResult Unresolved(string reason) => new(ProposalBoundaryOutcome.Unresolved, reason);
}

public interface IProposalDecisionBoundary
{
    ProposalBoundaryResult Evaluate(GovernedProposal proposal);
}

public sealed class GovernedProposalDecisionBoundary : IProposalDecisionBoundary
{
    private readonly Func<DateTimeOffset> _clock;

    public GovernedProposalDecisionBoundary(Func<DateTimeOffset> clock)
        => _clock = clock ?? throw new ArgumentNullException(nameof(clock));

    public ProposalBoundaryResult Evaluate(GovernedProposal proposal)
    {
        ArgumentNullException.ThrowIfNull(proposal);
        var now = _clock();

        if (!proposal.IsContainedByAuthorizedContext())
            return ProposalBoundaryResult.Rejected("Proposal exceeds or conflicts with its authorized context.");

        if (!proposal.Context.Grant.Authority.IsActiveAt(now))
            return ProposalBoundaryResult.Unresolved("Proposal authorization basis is stale, expired, or revoked.");

        if (proposal.Lifecycle != ProposalLifecycleState.Draft)
            return ProposalBoundaryResult.Rejected("Only a current draft proposal may enter the decision boundary.");

        if (proposal.Evidence.Count == 0)
            return ProposalBoundaryResult.Unresolved("Proposal has no governing evidence.");

        if (proposal.Evidence.Any(item => string.IsNullOrWhiteSpace(item.Provenance)))
            return ProposalBoundaryResult.Unresolved("Proposal evidence is missing provenance.");

        if (proposal.Evidence.Any(item => item.Lifecycle is InformationLifecycleState.Invalidated or InformationLifecycleState.Unknown))
            return ProposalBoundaryResult.Unresolved("Proposal contains invalid or unknown lifecycle information.");

        if (proposal.Evidence.Any(item => item.Lifecycle is InformationLifecycleState.Stale or InformationLifecycleState.Disputed))
            return ProposalBoundaryResult.Unresolved("Proposal contains stale or disputed information.");

        if (proposal.Uncertainty == Uncertainty.Unknown
            || proposal.Evidence.Any(item => item.Uncertainty == Uncertainty.Unknown))
            return ProposalBoundaryResult.Unresolved("Proposal contains unknown information.");

        if (proposal.Evidence
            .GroupBy(item => item.Key, StringComparer.Ordinal)
            .Any(group => group.Select(item => item.Value).Distinct(StringComparer.Ordinal).Count() > 1))
            return ProposalBoundaryResult.Unresolved("Proposal contains conflicting material evidence.");

        return ProposalBoundaryResult.Eligible("Proposal is bounded by authorized context and eligible for downstream governance evaluation; no approval or authorization is granted.");
    }
}
