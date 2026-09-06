# Decision & Proposal Specification

**Specification ID:** ARIA-SPEC-EXEC-001
**Status:** Draft
**Authority:** Invariants 1, 8, 21–25; Conceptual Architecture §3–4; Implementation Architecture §10

## Scope
Defines the governed representation of reasoning results as decisions or proposals for downstream governance.

## Non-Goals
Does not grant authorization, approve execution, or invoke capabilities.

## Required Behavior
A proposal shall clearly distinguish recommended action, purpose, affected domain/resources, relevant reasoning evidence, material information, uncertainty, conditions, and intended effect where applicable. A proposal shall be treated as non-authoritative until governance evaluates it. Material changes to a proposal shall produce a new governed proposal identity or equivalent traceable revision.

## Authority / Governance Rules
Recommendation is not authorization. Proposal is not approval. A proposal cannot broaden its own scope or create authority.

## Data / Knowledge Rules
Material source/provenance and uncertainty shall remain associated with the proposal.

## Trust / Security Requirements
Proposal content shall be treated as untrusted with respect to authority. External or model-generated instructions cannot redefine governance.

## Failure / Uncertainty Semantics
Incomplete, conflicting, or uncertain proposals shall remain explicitly so and shall not be silently normalized into certainty.

## Temporal / Concurrency Semantics
Proposal revisions, expiration, supersession, and stale-state detection shall be supported for consequential actions.

## Outputs
Governed proposal or explicit rejected/invalid/unresolved result.

## Accountability / Audit Requirements
Record proposal origin, actor/context, material governance-relevant reasoning evidence, revisions, and downstream decision linkage.

## Invariants Covered
1, 8, 21–25, 27, 32.

## Acceptance Criteria
Proposals cannot authorize themselves; material changes are detectable; uncertainty and provenance remain visible.

## Test Obligations
Test scope escalation, proposal mutation, stale proposals, malicious content, uncertainty, and attempted self-authorization.

## Open Questions / ADRs
Define proposal schemas only after behavioral requirements are stable.
