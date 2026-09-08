# Capability Contracts Specification

**Specification ID:** ARIA-SPEC-EXEC-002
**Status:** Approved
**Authority:** Invariants 2, 4, 25, 28–29, 33; Conceptual Architecture §6; Implementation Architecture §14

## Scope
Defines the bounded contract a capability must satisfy before it can participate in governed execution.

## Non-Goals
Does not grant authorization or decide whether a particular action is permitted.

## Required Behavior
Each capability shall declare its identity, offered operation, requirements, access boundary, authority boundary, result semantics, and accountability requirements. A capability shall request rather than manufacture authority. It shall operate only within the execution request and permissions supplied through governance. Capabilities shall not silently access unrelated domains or escalate privileges.

## Authority / Governance Rules
Capability availability is not permission. The capability contract shall make the authority boundary explicit and shall not become a competing source of authority.

## Data / Knowledge Rules
Capability inputs shall identify required information and applicable handling constraints. Results shall preserve relevant provenance and uncertainty.

## Trust / Security Requirements
Capabilities shall be bounded trust domains. High-impact capabilities require controls proportionate to risk. Compromise shall not automatically grant unrelated authority or audit/policy control.

## Failure / Uncertainty Semantics
Capabilities shall report explicit success, failure, partial, unavailable, or unknown results as applicable and shall not conceal uncertainty.

## Temporal / Concurrency Semantics
Capability invocation shall honor validity, authorization, idempotency, duplicate, timeout, and concurrency requirements established by execution governance.

## Outputs
Bounded capability result and accountability evidence.

## Accountability / Audit Requirements
Capability identity, requested operation, governance context, execution result, and material errors shall be attributable.

## Invariants Covered
2, 4, 25, 28, 29, 32, 33.

## Acceptance Criteria
Capabilities cannot create authority; declared boundaries are enforceable; compromised capability does not automatically gain unrelated authority.

## Test Obligations
Test privilege escalation, unauthorized domain access, malformed inputs, compromised capability behavior, partial results, duplicate invocation, and unavailable dependencies.

## Open Questions / ADRs
Define capability risk classes and contract validation rules.
