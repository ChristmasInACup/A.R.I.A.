# Knowledge Lifecycle Specification

**Specification ID:** ARIA-SPEC-KNOW-001
**Status:** Approved
**Authority:** Invariants 10, 12–16; Conceptual Architecture §3, §4, §9; Implementation Architecture §6

## Scope
Defines governance of information from acquisition through use, update, invalidation, sharing, retention, and disposal.

## Non-Goals
Does not authorize actions or define task-specific context assembly.

## Required Behavior
Knowledge shall retain ownership, domain, purpose, sensitivity, provenance, validity, freshness, and lifecycle state as applicable. Acquisition, transformation, sharing, cross-domain movement, retention, invalidation, and deletion shall be governed operations. Persistence shall never imply truth or authority. Derived information shall retain appropriate relationships to source information. Individually authorized information shall not automatically become authorized for aggregate inference.

## Authority / Governance Rules
Knowledge access and movement require authorization appropriate to the actor, purpose, domain, sensitivity, and operation. Knowledge cannot grant authority.

## Data / Knowledge Rules
Lifecycle states shall distinguish at minimum available, stale, disputed, invalidated, and unknown where applicable. Ownership and governance metadata shall survive copies and transformations.

## Trust / Security Requirements
Sensitive knowledge shall be exposed only through governed access. Compromise of a knowledge component shall not create authority.

## Failure / Uncertainty Semantics
Unavailable, conflicting, stale, or invalid knowledge shall remain explicit and shall not be silently replaced with fabricated certainty.

## Temporal / Concurrency Semantics
Changes shall support version, effective time, freshness, invalidation, and retention semantics where relevant.

## Outputs
Governed knowledge state or explicit unavailable/invalid/conflicting result.

## Accountability / Audit Requirements
Significant lifecycle and cross-domain changes shall be attributable and reconstructable.

## Invariants Covered
10, 12–16, 31, 32.

## Acceptance Criteria
Knowledge never becomes authority; provenance survives transformation; cross-domain movement is explicit; stale/invalid states remain distinguishable.

## Test Obligations
Test poisoning, stale data, invalidation, cross-domain transfer, retention, transformation, aggregation, and compromise.

## Open Questions / ADRs
Define domain-specific retention and sensitivity taxonomies when required.
