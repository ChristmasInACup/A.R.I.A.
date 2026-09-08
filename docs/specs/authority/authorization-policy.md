# Authorization & Policy Specification

**Specification ID:** ARIA-SPEC-AUTH-001
**Status:** Approved
**Authority:** Constitution; Invariants 1–9, 25, 27, 31, 38; Conceptual Architecture §3–4; Implementation Architecture §5

## Scope
Defines whether an action is authorized under applicable policy, purpose, domain, scope, conditions, and validity.

## Non-Goals
Does not establish identity, perform reasoning, execute capabilities, or replace human approval.

## Actors / Components
Actors, Identity & Domain, Authority Plane, policy sources, delegation authority, governance gate, execution governance.

## Preconditions
Identity/domain context and the proposed action are sufficiently established for evaluation.

## Inputs
Actor, domain, requested action, purpose, resources, policy, authority basis, delegation state, temporal validity, relevant conditions.

## Required Behavior
1. Authorization shall be an explicit governance decision.
2. Authorization shall be bound to actor, action, purpose, domain, scope, and applicable conditions.
3. Technical reachability, identity, knowledge, reasoning output, capability availability, or provider output shall never substitute for authorization.
4. Authorization shall not implicitly escalate, broaden, or become transferable.
5. Authorization shall be evaluated again at consequential use when required by risk or change.
6. A purpose-specific authorization shall not silently authorize another purpose.
7. Conflicting, expired, revoked, or unavailable policy/authority shall produce an explicit governed result rather than an assumption.
8. External content shall not modify authorization or policy merely by entering context.

## Authority / Governance Rules
This specification is the normative owner of action authorization and policy evaluation. Delegated authority is consumed only within its defined limits. Approval is not treated as unrestricted authorization.

## Data / Knowledge Rules
Policy and authority state are authoritative governance state. Cached or derived copies cannot silently become competing authorities.

## Trust / Security Requirements
Authorization decisions require trusted governance inputs appropriate to risk. Compromise of a reasoning or capability component shall not grant policy control.

## Failure / Uncertainty Semantics
If authorization cannot be established for a consequential action, the action shall fail closed or enter an explicitly restricted mode.

## Temporal / Concurrency Semantics
Expiration, revocation, effective time, version, replay, and time-of-check/time-of-use requirements shall be enforced at consequential use.

## Outputs
Authorized, denied, conditionally authorized, unresolved, or restricted governance result with applicable scope and conditions.

## Accountability / Audit Requirements
Record the actor, purpose, authority basis, applicable policy, scope, decision, validity, and decision context for significant actions.

## Invariants Covered
1–9, 25, 27, 31, 38.

## Acceptance Criteria
- No action becomes authorized solely through technical access or reasoning.
- Purpose and scope cannot silently broaden.
- Expired or revoked authorization cannot authorize consequential execution.
- Governance unavailability cannot silently become approval.

## Test Obligations
Positive/negative authorization, purpose changes, scope escalation, revocation races, expired authority, conflicting policy, provider manipulation, and unavailable governance tests.

## Open Questions / ADRs
Define policy precedence and conflict resolution where multiple valid policy sources exist, without creating competing authority sources.
