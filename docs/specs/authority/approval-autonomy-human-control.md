# Approval, Autonomy & Human Control Specification

**Specification ID:** ARIA-SPEC-AUTH-003
**Status:** Draft
**Authority:** Constitution; Invariants 22, 25, 38–41; Conceptual Architecture §8, §12; Implementation Architecture §12

## Scope
Defines approval, bounded autonomy, human control, disablement, override, and takeover requirements.

## Non-Goals
Does not replace authorization or execution governance.

## Actors / Components
Human approvers, authorized actors, Authority Plane, governance gate, autonomy controls, execution governance.

## Preconditions
An action has been interpreted and is subject to authorization and governance evaluation.

## Inputs
Proposed action, authorization, risk, autonomy level, approval requirement, approver identity, scope, purpose, resources, conditions, time limits.

## Required Behavior
1. Approval shall be explicit where required and bound to the action it approves.
2. Approval shall not create authority broader than the authorization already permits.
3. Material changes to an approved action shall trigger renewed governance and approval where required.
4. Autonomy shall be bounded by actor, action, resources, domain, purpose, limits, time, conditions, and revocation.
5. A.R.I.A. shall preserve a human-controlled path to disable autonomous operation.
6. A.R.I.A. shall support human takeover of governed operation.
7. Human control shall remain available independently of ordinary autonomous execution paths.
8. No model output or capability behavior shall substitute for required human approval.

## Authority / Governance Rules
Authorization answers whether an action may occur. Approval answers whether a particular governed action may proceed under the applicable human-control requirement. Autonomy is bounded standing authority, not model discretion.

## Data / Knowledge Rules
Approval evidence shall preserve the approved scope, purpose, conditions, material inputs, and validity window. Approval does not convert underlying information into truth.

## Trust / Security Requirements
Approval mechanisms shall resist forgery, replay, scope substitution, and unauthorized mutation. Disablement and takeover controls require stronger protection appropriate to risk.

## Failure / Uncertainty Semantics
Missing, invalid, stale, or unverifiable approval shall not be treated as approval. Loss of human-control mechanisms shall trigger restriction appropriate to risk.

## Temporal / Concurrency Semantics
Approval expiration, revocation, replay prevention, versioning, and TOCTOU protections shall be defined for consequential operations.

## Outputs
Approval required, approved, denied, autonomous-permitted, restricted, or human-takeover state.

## Accountability / Audit Requirements
Preserve approver identity, authorization basis, action scope, conditions, time, changes, and resulting execution linkage.

## Invariants Covered
22, 25, 38–41.

## Acceptance Criteria
- Approval cannot broaden authorization.
- Approved scope is bound to execution.
- Material changes invalidate prior approval when required.
- Human disablement and takeover remain available.

## Test Obligations
Test approval forgery/replay, scope changes, expired approval, autonomy boundaries, disablement, takeover, and loss of approval infrastructure.

## Open Questions / ADRs
Define risk tiers and exact autonomy-level transition rules.
