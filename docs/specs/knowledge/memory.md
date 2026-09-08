# Memory Specification

**Specification ID:** ARIA-SPEC-KNOW-002
**Status:** Approved
**Authority:** Invariants 10–13, 35; Conceptual Architecture §9; Implementation Architecture §6

## Scope
Defines deliberate retention of information as memory and the distinction between memory, knowledge, context, and truth.

## Non-Goals
Does not define authorization or task-specific context selection.

## Required Behavior
Encountered information shall not automatically become memory. Memory shall be deliberately retained under an applicable purpose and lifecycle. Memory shall preserve relevant provenance, ownership, domain, sensitivity, validity, freshness, retention, and sharing constraints. Memory shall not imply authority or truth. Invalidated or superseded memory shall not silently remain authoritative for governed use.

## Authority / Governance Rules
Memory creation, update, sharing, and deletion are governed knowledge operations. Memory cannot grant permission.

## Data / Knowledge Rules
Memory shall maintain source relationships and lifecycle state. Forgetting and retention shall be explicit governed behavior.

## Trust / Security Requirements
Memory access shall honor domain and sensitivity boundaries. Compromise of memory storage shall not create authority.

## Failure / Uncertainty Semantics
Missing memory shall be represented as missing; uncertain or stale memory shall not be presented as verified truth.

## Temporal / Concurrency Semantics
Memory shall support retention limits, invalidation, versioning, and conflict handling.

## Outputs
Governed retained information and its lifecycle state.

## Accountability / Audit Requirements
Material memory creation, mutation, sharing, invalidation, and deletion shall be attributable where required by risk.

## Invariants Covered
10–13, 31, 32, 35.

## Acceptance Criteria
Memory is deliberate; memory never authorizes; provenance and lifecycle survive retention; invalidation is enforceable.

## Test Obligations
Test automatic-retention attempts, stale memory, invalidation, deletion, cross-domain memory access, and derived-memory provenance.

## Open Questions / ADRs
Define retention classes and user/organizational forgetting controls where implementation requires them.
