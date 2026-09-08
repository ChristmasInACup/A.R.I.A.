# A.R.I.A. Capability Governance

**Status:** Foundational governance artifact — Proposed  
**Baseline:** Phase 2 Capability Model  
**Last Updated:** 2026-09-07

## Purpose

Capability governance controls the transition from approved specifications into project-planning units without allowing planning artifacts to redefine architecture or authority.

## Lifecycle

```text
DERIVED → UNDER REVIEW → APPROVED → BASELINED
                         ↘ REVISED / RETIRED
```

- **Derived:** Capability identified from approved specifications and architecture.
- **Under Review:** Responsibility, boundaries, traceability, and dependencies are being evaluated.
- **Approved:** Capability definition accepted by the repository owner as a valid planning boundary.
- **Baselined:** Approved capability set is the current authoritative input to Epic decomposition.
- **Revised/Retired:** Changed or intentionally removed through governed change.

## Governance Rules

1. Capabilities must derive from approved specifications.
2. A capability must have one clear primary responsibility.
3. Capabilities may combine related specification requirements but may not merge responsibilities in ways that create ambiguous authority.
4. Capabilities must not create a new source of authority.
5. Capabilities must remain implementation-agnostic unless a higher-level architectural decision explicitly requires otherwise.
6. Capability boundaries must preserve identity, authority, knowledge, reasoning, execution, trust, and accountability distinctions.
7. A capability cannot authorize itself merely because it can technically perform an action.
8. Cross-domain behavior must remain explicitly governed.
9. Failure and uncertainty semantics must remain consistent with the approved specifications.
10. Capability changes that alter architectural boundaries must escalate to the appropriate higher-level governance artifact rather than being hidden in planning.
11. Epic and story decomposition must preserve the capability's approved responsibility and constraints.
12. The repository owner is the current architectural authority and final approver.

## Approval Criteria

Before the capability set is baselined, confirm:

- all approved specifications are represented or have an explicit documented reason for not producing a capability;
- each capability has clear primary responsibility;
- capability boundaries do not duplicate or compete with authority boundaries;
- capability inputs, outputs, and dependencies are conceptually clear;
- security, trust, failure, and uncertainty boundaries remain intact;
- cross-domain movement remains governed;
- no implementation technology has been introduced prematurely;
- traceability from specification to capability is explicit;
- the resulting set is suitable for Epic decomposition.

## Relationship to Other Layers

```text
Approved Specifications
          ↓
     Capabilities
          ↓
        Epics
          ↓
       Stories
```

Specifications remain normative requirements. Capabilities organize those requirements into meaningful areas of system responsibility. Epics and stories describe planned work beneath the capability boundary.

## Authority Boundary

Planning artifacts cannot override the Constitution, invariants, conceptual architecture, implementation architecture, or approved specifications.

If a capability appears impossible without changing a higher-level architectural boundary, stop capability decomposition and explicitly revisit the higher-level decision.

> **Planning decomposes authority; it does not create authority.**
