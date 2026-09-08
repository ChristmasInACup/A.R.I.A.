# A.R.I.A. Epic Model

**Status:** Approved — Phase 3 Epic Baseline  
**Authority:** Derived from the approved Capability Baseline and all higher-level architectural artifacts  
**Last Updated:** 2026-09-08

This document defines the 12 approved major bodies of work beneath the capability layer. The approved model is unchanged from the Phase 3 review; this revision records its approved/baselined state following PR #7.

## 1. Purpose

This document defines the major bodies of work required to realize A.R.I.A.'s approved capabilities.

Epics are planning boundaries. They organize substantial work beneath the capability layer without redefining architecture, authority, or approved requirements.

Epics do **not** define implementation technologies, repositories, services, classes, APIs, databases, deployment units, vendors, schedules, or individual tasks.

## 2. Derivation Rule

Each epic must:

- trace to one or more approved capabilities;
- preserve the responsibilities and boundaries of those capabilities;
- trace ultimately to approved specifications and higher-level architecture;
- represent a meaningful body of work rather than a single feature, task, or implementation detail;
- remain implementation-agnostic at this planning layer.

A capability may be realized by more than one epic when its responsibility contains genuinely distinct bodies of work. Related capabilities may share an epic when separating them would create artificial boundaries, provided their authority boundaries remain explicit.

## 3. Approved Epic Model

The approved Phase 3 model contains the following 12 epics:

1. **EPIC-01 — Identity & Domain Foundation** — CAP-01
2. **EPIC-02 — Authority & Delegation Governance** — CAP-02
3. **EPIC-03 — Human Control & Autonomy Governance** — CAP-03
4. **EPIC-04 — Knowledge, Memory & Provenance** — CAP-04
5. **EPIC-05 — Context & Reasoning Coordination** — CAP-05, CAP-06
6. **EPIC-06 — Decision & Proposal Governance** — CAP-07
7. **EPIC-07 — Consequential Action Governance** — CAP-08
8. **EPIC-08 — Bounded Capability Execution** — CAP-09
9. **EPIC-09 — External Outcome & State Reconciliation** — CAP-10
10. **EPIC-10 — Accountability & Audit Evidence** — CAP-11
11. **EPIC-11 — Observability, Assurance & Containment** — CAP-12, CAP-13
12. **EPIC-12 — Configuration, Governance & Evolution** — CAP-14

The detailed responsibility and boundary definitions approved in PR #7 remain authoritative in this model.

## 4. Cross-Epic Rules

1. No epic creates an independent source of authority.
2. Epic boundaries cannot override approved specifications or higher-level architecture.
3. Identity, authorization, knowledge, reasoning, execution, trust, accountability, and human control remain distinct concerns even when work is grouped into one epic.
4. Human approval remains distinct from authorization.
5. Capability availability remains distinct from permission to use a capability.
6. Dispatch remains distinct from external outcome.
7. Accountability evidence remains distinct from ordinary memory and observability.
8. Trust degradation reduces capability before governance boundaries are weakened.
9. Cross-domain movement remains explicitly governed.
10. Governance-affecting changes remain governed changes.
11. Failure and uncertainty cannot silently become authority, approval, trust, or success.
12. Epic decomposition must not introduce implementation commitments prematurely.

## 5. Epic Acceptance Boundary

An epic is ready to become a story-planning input only when:

- its body of work is substantial and coherent;
- its owning capability or capabilities are explicit;
- its primary specification sources are traceable;
- its responsibility and boundaries are unambiguous;
- relationships with adjacent epics are understood;
- authority, trust, security, failure, and uncertainty constraints are preserved;
- no implementation technology has been introduced as an unstated requirement;
- its future stories can be decomposed without redefining the capability or specification layer.

## 6. Capability-to-Epic Traceability

| Capability | Primary Epic | Additional Epic Context |
|---|---|---|
| CAP-01 Establish Identity and Domain Context | EPIC-01 | — |
| CAP-02 Determine Effective Authority | EPIC-02 | — |
| CAP-03 Govern Approval and Autonomy | EPIC-03 | — |
| CAP-04 Govern Knowledge and Memory | EPIC-04 | EPIC-05 uses governed knowledge |
| CAP-05 Assemble Authorized Task Context | EPIC-05 | EPIC-04, EPIC-02 |
| CAP-06 Coordinate Governed Reasoning | EPIC-05 | — |
| CAP-07 Form and Govern Decisions and Proposals | EPIC-06 | — |
| CAP-08 Validate Consequential Actions | EPIC-07 | EPIC-02, EPIC-03 |
| CAP-09 Invoke Bounded Capabilities | EPIC-08 | EPIC-07 |
| CAP-10 Represent External Outcomes | EPIC-09 | EPIC-08 |
| CAP-11 Preserve Accountability Evidence | EPIC-10 | All consequential epics |
| CAP-12 Observe and Assure System Behavior | EPIC-11 | All relevant boundaries |
| CAP-13 Contain Trust and Security Failures | EPIC-11 | All relevant capabilities |
| CAP-14 Govern Configuration and Evolution | EPIC-12 | All governance-affecting state |

## 7. Traceability Position

```text
Constitution
    ↓
Invariant
    ↓
Conceptual Architecture
    ↓
Implementation Architecture
    ↓
Approved Specification
    ↓
Capability
    ↓
Epic
    ↓
Story
    ↓
Task
    ↓
Implementation
    ↓
Test
    ↓
Evidence
```

The epic layer organizes bodies of work; it does not replace specification-level requirements or capability-level responsibilities.

## 8. What This Baseline Does Not Decide

This epic model intentionally does not decide:

- programming languages or frameworks;
- databases or storage technologies;
- APIs or protocols;
- deployment topology;
- vendors or providers;
- detailed internal component boundaries;
- user-interface design;
- story-level acceptance criteria;
- task sequencing or implementation estimates;
- detailed policy precedence, identity assurance, risk tiers, or autonomy-transition mechanisms deferred to ADRs.

## Governing Principle

> **Epics organize substantial work required to realize approved capabilities. They do not create authority, redefine requirements, or decide implementation.**
