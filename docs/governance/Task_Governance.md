# A.R.I.A. Task Governance

**Status:** Foundational governance artifact — Proposed  
**Baseline:** Phase 5 Task Model  
**Last Updated:** 2026-09-08

## Purpose

Task governance controls the transition from approved Stories into actionable implementation work without allowing Tasks to redefine architecture, authority, requirements, or deferred decisions.

## Lifecycle

```text
DERIVED → UNDER REVIEW → APPROVED → BASELINED
                         ↘ REVISED / RETIRED
```

- **Derived:** Task identified from an approved Story and its upward traceability.
- **Under Review:** Objective, completion condition, dependencies, acceptance obligations, and governance boundaries are evaluated.
- **Approved:** Task definition accepted by the repository owner as a valid implementation-planning unit.
- **Baselined:** Approved Task set is the authoritative input to implementation planning and execution.
- **Revised/Retired:** Changed or intentionally removed through governed change.

## Governance Rules

1. Every Task must belong to exactly one primary approved Story.
2. Every Task must preserve traceability to the Story, Epic, Capability, and governing Specification.
3. A Task must have a bounded objective and completion condition.
4. Dependencies must be explicit where they materially affect completion.
5. Tasks may describe implementation work but cannot independently authorize unapproved technology or architecture choices.
6. Tasks must preserve all applicable authority, identity, knowledge, reasoning, execution, trust, accountability, domain, security, and human-control boundaries.
7. A Task must not create a new source of authority or broaden an existing one.
8. Failure, uncertainty, and unknown states must remain explicit where applicable.
9. Cross-domain behavior must remain explicitly governed.
10. Deferred architectural decisions must not be resolved by assumption at Task level.
11. If a Task requires changing a higher-level artifact, Task execution stops and the change is escalated through the appropriate governance process.
12. Test and evidence obligations may be identified by Tasks, but their existence must not be claimed until actually produced and validated.
13. The repository owner is the current architectural authority and final approver.

## Review Criteria

Before the Task set is baselined, confirm:

- every approved Story has appropriate Task coverage or an explicit documented reason for intentional omission;
- each Task has one primary Story and clear upward traceability;
- each Task is bounded and independently actionable;
- completion conditions are observable;
- dependencies do not create hidden authority paths;
- applicable acceptance, test, and evidence obligations are identified without false claims;
- authority, security, trust, failure, uncertainty, accountability, domain, and human-control boundaries remain intact;
- no unapproved technology, vendor, API, database, deployment choice, or internal code structure has been smuggled into the planning layer;
- the resulting Tasks are suitable to hand to implementation work;
- no Task requires an unstated higher-level architectural change.

## Relationship to Other Layers

```text
Approved Specifications
          ↓
     Capabilities
          ↓
        Epics
          ↓
       Stories
          ↓
        Tasks
          ↓
    Implementation
          ↓
        Tests
          ↓
       Evidence
```

Specifications remain normative requirements. Capabilities organize responsibility. Epics organize substantial bodies of work. Stories define bounded outcomes. Tasks make those approved Stories actionable for implementation.

## Authority Boundary

Planning artifacts cannot override the Constitution, invariants, conceptual architecture, implementation architecture, approved specifications, approved capabilities, approved epics, or approved stories.

> **Tasks make approved Stories actionable; they do not create authority.**
