# Context Assembly Specification

**Specification ID:** ARIA-SPEC-REAS-001
**Status:** Draft
**Authority:** Invariants 9–16, 17–21, 27; Conceptual Architecture §4; Implementation Architecture §7

## Scope
Defines construction of the minimum necessary authorized task context presented to reasoning resources.

## Non-Goals
Does not authorize actions, establish knowledge truth, or perform execution.

## Required Behavior
Context shall be assembled for a specific task and purpose. Only information authorized for that task may enter context. Context shall apply domain, sensitivity, purpose, provenance, freshness, validity, and minimum-necessary constraints. Individually accessible facts shall not automatically be aggregated when the aggregate creates new sensitivity or consequential inference. Context shall not become general-purpose memory unless separately retained through knowledge governance.

## Authority / Governance Rules
Context assembly consumes authorization; it cannot create, broaden, or infer authority.

## Data / Knowledge Rules
Provenance and uncertainty shall remain attached to material context. Cross-domain movement shall be explicit and governed.

## Trust / Security Requirements
Untrusted content shall not alter governance instructions. Sensitive context exposure to providers shall follow provider governance and applicable policy.

## Failure / Uncertainty Semantics
Unavailable, conflicting, stale, or insufficient context shall remain explicit. Missing context shall not be fabricated.

## Temporal / Concurrency Semantics
Context shall reflect applicable authorization and knowledge validity at assembly and consequential use. Material changes may require reassembly.

## Outputs
A bounded task context, or explicit insufficient/denied/unresolved result.

## Accountability / Audit Requirements
For consequential tasks, preserve sufficient evidence of what categories of information and governance constraints materially shaped the context.

## Invariants Covered
9–16, 17–21, 27, 32.

## Acceptance Criteria
Only authorized information enters context; minimum necessary scope is enforced; aggregate inference is governed; provenance survives assembly.

## Test Obligations
Test cross-domain leakage, over-collection, aggregate inference, stale authorization, poisoned content, provider exposure, and missing context.

## Open Questions / ADRs
Define task-specific minimization policies and acceptable aggregate-inference controls.
