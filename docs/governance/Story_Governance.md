# A.R.I.A. Story Governance

**Status:** Foundational governance artifact — Proposed  
**Baseline:** Phase 4 Story Model  
**Last Updated:** 2026-09-08

## Purpose

Story governance controls the transition from approved epics into bounded, reviewable work without allowing planning artifacts to redefine architecture, authority, or requirements.

## Lifecycle

```text
DERIVED → UNDER REVIEW → APPROVED → BASELINED
                         ↘ REVISED / RETIRED
```

- **Derived:** Story identified from an approved Epic and its source Capability/Specifications.
- **Under Review:** Scope, acceptance, traceability, dependencies, and governance boundaries are evaluated.
- **Approved:** Story definition accepted by the repository owner as a valid planning unit.
- **Baselined:** Approved Story set is the authoritative input to Task decomposition.
- **Revised/Retired:** Changed or intentionally removed through governed change.

## Governance Rules

1. Every Story must belong to one primary approved Epic.
2. Every Story must trace to approved Capability and specification requirements.
3. A Story must represent a coherent, independently reviewable outcome rather than an implementation task.
4. Story acceptance criteria must be observable without prescribing implementation technology.
5. Stories must preserve all applicable authority, identity, knowledge, reasoning, execution, trust, accountability, domain, and human-control boundaries.
6. Stories must not create a new source of authority or broaden an existing one.
7. Story dependencies do not transfer authority between Stories.
8. Failure, uncertainty, and unknown states must remain explicit where applicable.
9. Cross-domain behavior must remain explicitly governed.
10. Deferred architectural decisions must not be resolved by assumption at Story level.
11. If a Story requires changing a higher-level artifact, Story decomposition stops and the change is escalated through the appropriate governance process.
12. The repository owner is the current architectural authority and final approver.

## Review Criteria

Before the Story set is baselined, confirm:

- all approved Epics have appropriate Story coverage or an explicit documented reason for any intentional omission;
- each Story has one primary Epic and clear capability/specification traceability;
- each Story represents bounded, reviewable work rather than a task or implementation detail;
- acceptance conditions are clear and testable without selecting technology;
- dependencies do not create hidden authority paths;
- authority, security, trust, failure, uncertainty, accountability, domain, and human-control boundaries remain intact;
- no implementation technology, vendor, API, database, deployment choice, or internal code structure has been smuggled into the planning layer;
- the resulting Stories are suitable for Task decomposition;
- no Story requires an unstated higher-level architectural change.

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

Specifications remain normative requirements. Capabilities organize responsibility. Epics organize substantial bodies of work. Stories define bounded outcomes beneath those epics. Tasks describe the implementation work required to complete approved Stories.

## Authority Boundary

Planning artifacts cannot override the Constitution, invariants, conceptual architecture, implementation architecture, approved specifications, approved capabilities, or approved epics.

> **Stories define bounded work; they do not create authority.**
