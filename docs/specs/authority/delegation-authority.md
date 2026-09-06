# Delegation & Authority Specification

**Specification ID:** ARIA-SPEC-AUTH-002
**Status:** Draft
**Authority:** Constitution; Invariants 5–7, 31, 38; Conceptual Architecture §3, §7–8; Implementation Architecture §5

## Scope
Defines creation, propagation, limitation, expiration, and revocation of delegated authority.

## Non-Goals
Does not authenticate actors, define general policy, or perform execution.

## Actors / Components
Delegators, delegates, Identity & Domain, Authority Plane, policy, governance gate, accountability.

## Preconditions
A delegator has established authority that is valid for the delegation in question.

## Inputs
Delegator authority, delegate identity, delegated action, purpose, domain, limits, conditions, validity window, revocation state.

## Required Behavior
1. Delegated authority shall be explicitly created and attributable.
2. A delegation shall not exceed the delegator's current authority.
3. Delegation shall define scope, purpose, domain, conditions, and validity appropriate to risk.
4. Delegation shall not implicitly propagate to unrelated actors or domains.
5. A delegate shall not further delegate authority unless explicitly permitted.
6. Revocation or expiration shall prevent consequential use after validity ends.
7. Changes to authority affecting existing delegations shall be evaluated for continued validity.

## Authority / Governance Rules
Delegation is a bounded transfer or grant of authority, not ownership of the underlying constitutional authority. No delegation can override higher-level invariants or policy.

## Data / Knowledge Rules
Delegation state shall preserve issuer, recipient, scope, purpose, domain, conditions, provenance, validity, and revocation state.

## Trust / Security Requirements
Delegation records are security-critical governance state and shall be protected from unauthorized mutation.

## Failure / Uncertainty Semantics
Unverifiable delegation shall not authorize consequential action. Conflicting or ambiguous delegation shall require governance resolution.

## Temporal / Concurrency Semantics
Delegation shall support effective time, expiration, revocation, versioning, and prevention of stale-use races.

## Outputs
A bounded delegation decision or explicit denial/unresolved result.

## Accountability / Audit Requirements
Record who delegated, to whom, what authority was delegated, why, limits, duration, and subsequent revocation or use where significant.

## Invariants Covered
5–7, 31, 38.

## Acceptance Criteria
- Delegation never exceeds delegator authority.
- Delegation cannot propagate implicitly.
- Expired/revoked delegation cannot authorize consequential action.
- Delegation changes are attributable.

## Test Obligations
Test over-delegation, transitive delegation, revocation, expiration, conflicting delegations, compromised delegators, and concurrent authority changes.

## Open Questions / ADRs
Define exceptional emergency delegation rules without weakening ordinary authority boundaries.
