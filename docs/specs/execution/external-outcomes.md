# External Outcomes Specification

**Specification ID:** ARIA-SPEC-EXEC-004
**Status:** Draft
**Authority:** Invariants 23–26; Conceptual Architecture §5, §12; Implementation Architecture §15

## Scope
Defines honest representation of what actually happened outside A.R.I.A. after an execution attempt.

## Non-Goals
Does not authorize execution or control external systems beyond the governed request.

## Required Behavior
A.R.I.A. shall distinguish execution dispatch from external outcome. External reality is authoritative regarding whether an external effect occurred. Outcomes shall support success, failure, partial, unknown, and conflicting states as applicable. A.R.I.A. shall not infer success solely from dispatch or a local acknowledgement when external effect remains uncertain.

## Authority / Governance Rules
An outcome cannot grant authority or retroactively legitimize unauthorized execution.

## Data / Knowledge Rules
Outcome evidence shall preserve source, time, external-system identity, correlation, and uncertainty where material.

## Trust / Security Requirements
External systems shall be treated as potentially unreliable or compromised. Outcome evidence shall be protected from unauthorized alteration.

## Failure / Uncertainty Semantics
Unknown is a first-class result. Conflicting external evidence remains conflicting until governed resolution is established.

## Temporal / Concurrency Semantics
Outcome handling shall account for delayed effects, duplicate delivery, eventual consistency, retries, and reconciliation.

## Outputs
Honest external outcome state and supporting evidence.

## Accountability / Audit Requirements
Link external outcomes to the originating execution intent and capability invocation.

## Invariants Covered
23–26, 32.

## Acceptance Criteria
Dispatch is never equated with success; unknown remains unknown; partial and conflicting effects remain visible.

## Test Obligations
Test timeouts, lost responses, duplicate effects, partial completion, contradictory evidence, delayed external effects, and reconciliation.

## Open Questions / ADRs
Define domain-specific reconciliation requirements for external systems with asynchronous outcomes.
