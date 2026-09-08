# A.R.I.A. Implementation Governance

**Status:** Foundational governance artifact — Proposed  
**Baseline:** Implementation Readiness  
**Last Updated:** 2026-09-08

## Purpose

Implementation Governance controls the transition from approved Tasks into actual implementation while preserving the authority of the architecture and planning baselines.

## Lifecycle

```text
READY → IN PROGRESS → VALIDATION → ACCEPTED
             ↘ BLOCKED / ESCALATED
```

## Governance Rules

1. Implementation work must originate from an approved Task.
2. Each implementation change must remain traceable to its Task and therefore to its Story, Epic, Capability, and governing Specification.
3. Implementation must not create, remove, or broaden authority outside approved governance.
4. Implementation must not silently change requirements or higher-level architecture.
5. Unresolved architectural decisions must be identified and governed rather than assumed.
6. Security, trust, accountability, human-control, domain, and failure boundaries must remain intact.
7. A change that requires higher-level architectural approval must stop at that boundary until the appropriate decision is approved.
8. Tests and evidence are separate validation artifacts; implementation completion is not proof of requirement satisfaction.
9. The repository owner remains the final architectural authority and approver.

## Implementation Change Review

Before an implementation change is accepted, confirm:

- the parent Task is identified and approved;
- the change satisfies the Task completion condition;
- required acceptance and validation obligations are identified;
- traceability remains intact;
- no higher-level requirement or architectural boundary has been changed by assumption;
- applicable security, trust, failure, uncertainty, accountability, and human-control behavior is preserved;
- any necessary architectural or policy decision has been separately governed;
- implementation does not substitute technical availability for authorization.

## Escalation

When implementation exposes a conflict with an approved higher-level artifact, the implementation work is blocked at that boundary. The conflict must be resolved through the applicable architecture, specification, or decision-record process before implementation continues.

> **Implementation is subordinate to governance; successful code does not grant authority.**

## Relationship to Validation

```text
Approved Task
     ↓
Implementation
     ↓
Test
     ↓
Evidence
     ↓
Acceptance
```

A passing test is evidence for a particular condition; it does not independently authorize deployment, broaden authority, or redefine the architecture.
