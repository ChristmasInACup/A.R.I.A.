# A.R.I.A. Architecture Change Process

**Status:** Governance Process — Draft for Review

> **Purpose:** Define how architectural changes are proposed, reviewed, approved, and incorporated without allowing implementation to silently redefine architecture.  
> **What done looks like:** Every architectural change has a clear rationale, affected authority/invariants, explicit review, recorded decision, and traceable merge outcome.

## 1. Governing Principle

Architecture is changed deliberately, not accidentally through implementation.

> **Implementation may refine architecture; implementation may not silently create architecture.**

## 2. When This Process Applies

Use this process when a change affects or may affect:

- authority or authorization;
- identity or domain boundaries;
- knowledge ownership, provenance, or trust;
- capabilities or execution boundaries;
- autonomy or approval;
- provider/model governance;
- audit or accountability;
- security or containment;
- human control;
- constitutional or invariant boundaries;
- a fundamental architectural assumption.

Lower-level changes that remain within approved architecture may follow the normal development flow.

## 3. Process

```text
Identify Architectural Impact
        ↓
Describe Problem and Rationale
        ↓
Identify Affected Artifacts / Invariants
        ↓
ADR When Durable Decision Is Needed
        ↓
Architecture Review
        ↓
Revise Architecture / Supporting Artifacts
        ↓
Explicit Approval
        ↓
Merge
        ↓
Update Traceability / Changelog as Appropriate
```

## 4. Review Requirements

An architectural review should establish:

1. why the existing architecture is insufficient or needs refinement;
2. which higher-level artifacts are affected;
3. whether any invariant is changed or newly implicated;
4. whether authority, trust, knowledge, execution, or human-control boundaries change;
5. whether the change creates new failure or security modes;
6. whether the change remains technology-neutral at the conceptual level;
7. what implementation/specification work follows approval.

## 5. Approval Rules

- A change may not be considered architectural authority merely because it exists on a branch.
- A passing implementation test does not approve an architectural change.
- Approval applies only to the reviewed scope.
- Changes to the Constitution or invariants require the extraordinary governance appropriate to those artifacts.
- Conceptual architecture changes require explicit architectural review.
- Lower-layer artifacts must remain consistent with approved higher-layer decisions.

## 6. Merge Order

For work that changes architecture and implementation:

1. establish or update the architectural decision;
2. approve the architectural change;
3. update specifications or supporting governance artifacts;
4. implement capabilities through epics, stories, and tasks;
5. verify through tests and evidence.

Do not use implementation to establish an architecture retroactively unless the architectural change is deliberately reviewed and approved.

## 7. Records

Architectural decisions should remain traceable through:

- the affected architecture document;
- ADRs when durable rationale is needed;
- specification references;
- invariant traceability;
- pull requests and approval records;
- changelog entries for meaningful project-level evolution.

## 8. Reopening a Closed Architecture

The conceptual architecture is considered closed for implementation at the level of current first principles. A lower-layer problem may justify reopening it only when the existing boundaries cannot represent the requirement without contradiction or an unsafe exception.

A new implementation convenience, provider limitation, or local optimization is not by itself sufficient reason to reopen architecture.
