# A.R.I.A. Epic Model

**Status:** Proposed — Phase 3 Epic Baseline  
**Authority:** Derived from the approved Capability Baseline and all higher-level architectural artifacts  
**Last Updated:** 2026-09-07

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

## 3. Epic Model

### EPIC-01 — Identity & Domain Foundation

**Purpose:** Establish the governed identity, subject, lifecycle, and domain-context foundations required for all subsequent governed activity.

**Capabilities:** CAP-01 — Establish Identity and Domain Context

**Primary specifications:** ARIA-SPEC-ID-001

**Major responsibility:** Provide reliable identity/domain context, including explicit unresolved, invalid, expired, revoked, or conflicting states, without granting authority.

**Boundary:** This epic establishes context for governance; it does not determine authorization, trust, approval, or execution.

---

### EPIC-02 — Authority & Delegation Governance

**Purpose:** Establish the mechanisms and governing work required to determine effective authority and bounded delegation.

**Capabilities:** CAP-02 — Determine Effective Authority

**Primary specifications:** ARIA-SPEC-AUTH-001, ARIA-SPEC-AUTH-002

**Major responsibility:** Govern authorization decisions using applicable policy, authority, delegation, domain, purpose, scope, and lifecycle constraints.

**Boundary:** Authority determination does not itself execute an action and does not arise from identity, technical reachability, knowledge, reasoning, or capability availability.

---

### EPIC-03 — Human Control & Autonomy Governance

**Purpose:** Establish governed approval, bounded autonomy, human disablement, and human takeover.

**Capabilities:** CAP-03 — Govern Approval and Autonomy

**Primary specifications:** ARIA-SPEC-AUTH-003

**Major responsibility:** Determine and enforce when human approval is required, what an approval covers, how autonomous operation remains bounded, and how human control supersedes autonomous operation.

**Boundary:** Approval/autonomy constraints do not replace authorization or broaden authority.

---

### EPIC-04 — Knowledge, Memory & Provenance

**Purpose:** Establish governed information and memory lifecycle capabilities while preserving ownership, provenance, sensitivity, domain, validity, and uncertainty.

**Capabilities:** CAP-04 — Govern Knowledge and Memory

**Primary specifications:** ARIA-SPEC-KNOW-001, ARIA-SPEC-KNOW-002, ARIA-SPEC-KNOW-003

**Major responsibility:** Govern acquisition, classification, retention, transformation, invalidation, sharing, retrieval, memory, and provenance of information.

**Boundary:** Persistence does not make information true; knowledge and memory do not create authority.

---

### EPIC-05 — Context & Reasoning Coordination

**Purpose:** Establish the governed preparation and coordination of information and reasoning resources for authorized tasks.

**Capabilities:** CAP-05 — Assemble Authorized Task Context; CAP-06 — Coordinate Governed Reasoning

**Primary specifications:** ARIA-SPEC-REAS-001, ARIA-SPEC-REAS-002, ARIA-SPEC-REAS-003; supporting knowledge and authority specifications

**Major responsibility:** Assemble minimum-necessary authorized context and coordinate reasoning while preserving uncertainty, provenance, trust boundaries, and provider/model separation.

**Boundary:** Context and reasoning inform decisions but cannot create authority, upgrade truth status, or silently cross governance boundaries.

---

### EPIC-06 — Decision & Proposal Governance

**Purpose:** Establish the governed representation of intended behavior before consequential action.

**Capabilities:** CAP-07 — Form and Govern Decisions and Proposals

**Primary specifications:** ARIA-SPEC-EXEC-001

**Major responsibility:** Represent decisions and proposals with purpose, scope, affected resources/domains, material inputs, uncertainty, and lifecycle state so they can be evaluated by downstream governance.

**Boundary:** A decision/proposal represents governed intent; it is not itself authorization or execution.

---

### EPIC-07 — Consequential Action Governance

**Purpose:** Establish the final governance boundary that must be satisfied before consequential execution.

**Capabilities:** CAP-08 — Validate Consequential Actions

**Primary specifications:** ARIA-SPEC-EXEC-003; supporting authority and approval specifications

**Major responsibility:** Revalidate authority, scope, approval/autonomy state, relevant conditions, and material changes immediately before consequential execution.

**Boundary:** This epic governs whether execution may proceed; it does not perform the underlying capability or determine external outcome.

---

### EPIC-08 — Bounded Capability Execution

**Purpose:** Establish governed invocation of declared capabilities under explicit contracts and accountability constraints.

**Capabilities:** CAP-09 — Invoke Bounded Capabilities

**Primary specifications:** ARIA-SPEC-EXEC-002

**Major responsibility:** Invoke capabilities within granted scope, contractual requirements, access boundaries, authority boundaries, and accountability obligations.

**Boundary:** Execution mechanisms cannot grant themselves authority, broaden authorization, or declare external success.

---

### EPIC-09 — External Outcome & State Reconciliation

**Purpose:** Establish honest representation of what happened outside A.R.I.A.'s immediate execution boundary.

**Capabilities:** CAP-10 — Represent External Outcomes

**Primary specifications:** ARIA-SPEC-EXEC-004

**Major responsibility:** Distinguish dispatch from actual external outcome and represent success, failure, partial completion, delay, conflict, and unknown state according to available evidence.

**Boundary:** Internal intent, authorization, dispatch, or confidence cannot substitute for external outcome evidence.

---

### EPIC-10 — Accountability & Audit Evidence

**Purpose:** Establish preservation of governed evidence sufficient to reconstruct significant activity and decisions.

**Capabilities:** CAP-11 — Preserve Accountability Evidence

**Primary specifications:** ARIA-SPEC-ASR-001

**Major responsibility:** Preserve attributable evidence connecting relevant identity/domain, authority, policy, context, proposal, approval/autonomy, execution, and external outcome events as applicable.

**Boundary:** Accountability evidence is a governance boundary distinct from ordinary memory and operational observability.

---

### EPIC-11 — Observability, Assurance & Containment

**Purpose:** Establish operational visibility, security assurance, trust-boundary enforcement, containment, and recovery of trusted capability.

**Capabilities:** CAP-12 — Observe and Assure System Behavior; CAP-13 — Contain Trust and Security Failures

**Primary specifications:** ARIA-SPEC-ASR-002, ARIA-SPEC-EVOL-001

**Major responsibility:** Detect and investigate relevant system conditions, evaluate assurance signals, contain compromised or untrusted components, reduce capability when trust is lost, and support governed recovery.

**Boundary:** Observability does not authorize action or rewrite accountability evidence. Trust degradation reduces capability rather than creating authority.

---

### EPIC-12 — Configuration, Governance & Evolution

**Purpose:** Establish governed change to the state and relationships that affect A.R.I.A.'s behavior, authority, security, and organizational evolution.

**Capabilities:** CAP-14 — Govern Configuration and Evolution

**Primary specifications:** ARIA-SPEC-EVOL-002

**Major responsibility:** Govern changes to policies, domains, authority relationships, autonomy, provider restrictions, capability registrations, security requirements, retention, and other governance-affecting state.

**Boundary:** Governance-affecting configuration is itself governed. Ordinary implementation change cannot silently alter higher-level architecture or constitutional authority.

## 4. Epic Relationships

The epic model preserves the governed flow established by the capability model:

```text
EPIC-01 Identity & Domain
          │
          ├──────────────► EPIC-02 Authority & Delegation
          │                         │
EPIC-04 Knowledge & Memory ─────────┤
          │                         │
          └────► EPIC-05 Context & Reasoning ◄────┘
                            │
                            ▼
                 EPIC-06 Decision & Proposal
                            │
                            ▼
                 EPIC-03 Human Control
                            │
                            ▼
              EPIC-07 Consequential Governance
                            │
                            ▼
                 EPIC-08 Capability Execution
                            │
                            ▼
                 EPIC-09 External Outcome
                            │
                            ▼
                 EPIC-10 Accountability

EPIC-11 Observability / Assurance / Containment
      └────────► all relevant governed boundaries

EPIC-12 Configuration / Evolution
      └────────► governance state and change
```

The ordering expresses logical governance relationships, not implementation dependencies or a requirement that all activity follow one fixed runtime sequence.

## 5. Cross-Epic Rules

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

## 6. Epic Acceptance Boundary

An epic is ready to become a story-planning input only when:

- its body of work is substantial and coherent;
- its owning capability or capabilities are explicit;
- its primary specification sources are traceable;
- its responsibility and boundaries are unambiguous;
- relationships with adjacent epics are understood;
- authority, trust, security, failure, and uncertainty constraints are preserved;
- no implementation technology has been introduced as an unstated requirement;
- its future stories can be decomposed without redefining the capability or specification layer.

## 7. Capability-to-Epic Traceability

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

## 8. Traceability Position

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

## 9. What This Baseline Does Not Decide

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
