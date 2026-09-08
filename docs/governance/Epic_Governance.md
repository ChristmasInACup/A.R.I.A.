# A.R.I.A. Epic Governance

**Status:** Foundational governance artifact — Proposed  
**Baseline:** Phase 3 Epic Model  
**Last Updated:** 2026-09-07

## Purpose

Epic governance controls the transition from approved capabilities into major bodies of planned work without allowing planning artifacts to redefine architecture, authority, or requirements.

## Lifecycle

```text
DERIVED → UNDER REVIEW → APPROVED → BASELINED
                         ↘ REVISED / RETIRED
```

- **Derived:** Epic identified from the approved capability baseline.
- **Under Review:** Responsibility, boundaries, traceability, relationships, and decomposition suitability are evaluated.
- **Approved:** Epic definition accepted by the repository owner as a valid planning boundary.
- **Baselined:** Approved epic set is the authoritative input to Story decomposition.
- **Revised/Retired:** Changed or intentionally removed through governed change.

## Governance Rules

1. Every epic must derive from one or more approved capabilities.
2. Every epic must have a coherent, substantial body of work.
3. Epics must preserve the responsibility and authority boundaries of their source capabilities.
4. Epics must not create a new source of authority or silently merge distinct governance responsibilities.
5. Epics must remain implementation-agnostic unless a higher-level architectural decision explicitly requires otherwise.
6. Cross-epic relationships must not become hidden authority paths.
7. Failure, uncertainty, security, trust, and human-control semantics must remain consistent with approved specifications and capabilities.
8. Cross-domain movement remains explicitly governed.
9. Stories and tasks derived from an epic must remain subordinate to its approved capability and specification boundaries.
10. If realizing an epic appears to require an architectural change, decomposition stops and the higher-level decision is revisited through the appropriate governance process.
11. The repository owner is the current architectural authority and final approver.

## Review Criteria

Before the epic set is baselined, confirm:

- every approved capability is represented by one or more epics, or an explicit documented reason exists for not creating one;
- each epic has a clear primary responsibility;
- each epic represents meaningful work rather than a feature, story, or task;
- capability-to-epic traceability is explicit;
- adjacent epic boundaries are coherent and non-competing;
- authority, trust, security, failure, uncertainty, and human-control boundaries remain intact;
- no implementation technology has been introduced prematurely;
- the epic set is suitable for Story decomposition;
- no epic requires an unstated change to a higher-level architectural artifact.

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
```

Specifications remain normative requirements. Capabilities organize requirements into areas of responsibility. Epics organize substantial bodies of work beneath those capabilities. Stories and tasks describe progressively smaller planned work beneath the epic boundary.

## Authority Boundary

Planning artifacts cannot override the Constitution, invariants, conceptual architecture, implementation architecture, approved specifications, or approved capabilities.

> **Epics organize work; they do not create authority.**
