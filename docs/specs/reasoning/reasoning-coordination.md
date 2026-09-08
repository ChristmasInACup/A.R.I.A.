# Reasoning Coordination Specification

**Specification ID:** ARIA-SPEC-REAS-002
**Status:** Approved
**Authority:** Invariants 1, 17–21, 27; Conceptual Architecture §4, §10; Implementation Architecture §8

## Scope
Defines how A.R.I.A. coordinates reasoning resources over governed context to produce conclusions, recommendations, and proposals.

## Non-Goals
Does not authorize actions, mutate policy, or execute capabilities.

## Required Behavior
Reasoning shall operate only on context supplied through governed context assembly. Reasoning may produce conclusions, recommendations, alternatives, uncertainty, and proposed actions. Reasoning output shall remain non-authoritative until evaluated by governance. Reasoning shall not modify authorization, policy, delegation, domain, or approval state. Multiple reasoning resources may be coordinated only within applicable provider governance.

## Authority / Governance Rules
Reasoning cannot create authority. Model confidence, provider trust, or persuasive output cannot substitute for authorization or approval.

## Data / Knowledge Rules
Reasoning inputs and material outputs shall preserve applicable provenance, purpose, sensitivity, and uncertainty. Hidden model chain-of-thought is not required as governance evidence.

## Trust / Security Requirements
Reasoning resources are bounded trust domains. Untrusted model output shall not become executable instruction or governance mutation without independent evaluation.

## Failure / Uncertainty Semantics
Provider failure, disagreement, insufficient context, or uncertain conclusions shall remain explicit. A.R.I.A. shall not manufacture confidence.

## Temporal / Concurrency Semantics
Reasoning results may become stale when authorization, context, policy, or external state changes; consequential use shall account for such changes.

## Outputs
Conclusion, recommendation, alternative, proposal input, uncertainty, or explicit inability to reason.

## Accountability / Audit Requirements
For consequential actions, preserve sufficient governance-level evidence of reasoning resources used, relevant inputs, material recommendation, and uncertainty without requiring private chain-of-thought.

## Invariants Covered
1, 17–21, 27, 32–33.

## Acceptance Criteria
Reasoning cannot authorize; provider/model output cannot mutate governance; uncertainty remains explicit; provider replacement does not alter authority semantics.

## Test Obligations
Test prompt injection, malicious model output, model disagreement, provider failure, confidence manipulation, stale reasoning, and attempted governance mutation.

## Open Questions / ADRs
Define acceptable multi-provider arbitration strategies without treating consensus as authority.
