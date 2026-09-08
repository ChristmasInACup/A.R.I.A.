# A.R.I.A. Capability Model

**Status:** Approved — Phase 2 Capability Baseline  
**Authority:** Derived from the Constitution, Architectural Invariants, approved Conceptual Architecture, Implementation Architecture, and approved Specification Baseline  
**Last Updated:** 2026-09-07

## 1. Purpose

This document defines the logical capabilities A.R.I.A. must provide to satisfy the approved architecture and specifications.

Capabilities describe **what A.R.I.A. must be able to do**. They do not define implementation technologies, services, classes, APIs, databases, deployment units, or other mechanisms for doing it.

A capability is not an authority source. A capability may perform or provide something only within the applicable governance boundaries.

## 2. Derivation Rule

Capabilities are derived from responsibilities and behavioral requirements in the approved specification set. They are not a one-to-one copy of specifications or implementation components.

A single specification may contribute to multiple capabilities, and a capability may require multiple specifications to define its complete boundary.

## 3. Capability Model

### CAP-01 — Establish Identity and Domain Context

A.R.I.A. must be able to establish the participating actor or subject and the relevant domain context, including unresolved or invalid identity states.

**Primary specifications:** ARIA-SPEC-ID-001

**Boundary:** Establishes identity/domain context; does not grant authority.

---

### CAP-02 — Determine Effective Authority

A.R.I.A. must be able to evaluate whether a requested operation is authorized under applicable policy, authority, delegation, domain, purpose, scope, and lifecycle constraints.

**Primary specifications:** ARIA-SPEC-AUTH-001, ARIA-SPEC-AUTH-002

**Boundary:** Determines authority; does not execute the operation.

---

### CAP-03 — Govern Approval and Autonomy

A.R.I.A. must be able to determine when human approval is required, validate approval scope, enforce bounded autonomy, and preserve human disablement and takeover.

**Primary specifications:** ARIA-SPEC-AUTH-003

**Boundary:** Approval and autonomy constrain execution; neither replaces authorization.

---

### CAP-04 — Govern Knowledge and Memory

A.R.I.A. must be able to acquire, classify, retain, transform, invalidate, share, and retrieve governed information and deliberate memory while preserving ownership, provenance, lifecycle, domain, sensitivity, and uncertainty semantics.

**Primary specifications:** ARIA-SPEC-KNOW-001, ARIA-SPEC-KNOW-002, ARIA-SPEC-KNOW-003

**Boundary:** Knowledge and memory provide governed information; they do not create authority or become truth merely through persistence.

---

### CAP-05 — Assemble Authorized Task Context

A.R.I.A. must be able to construct minimum-necessary context for a specific task while enforcing purpose, authorization, domain, sensitivity, provenance, and aggregation constraints.

**Primary specifications:** ARIA-SPEC-REAS-001; supporting knowledge and authority specifications

**Boundary:** Context is task-scoped state unless deliberately retained through Knowledge Governance.

---

### CAP-06 — Coordinate Governed Reasoning

A.R.I.A. must be able to coordinate reasoning resources to analyze authorized context, represent uncertainty, compare or combine reasoning where appropriate, and produce conclusions or recommendations without creating authority.

**Primary specifications:** ARIA-SPEC-REAS-002, ARIA-SPEC-REAS-003

**Boundary:** Reasoning informs decisions; it does not authorize actions.

---

### CAP-07 — Form and Govern Decisions and Proposals

A.R.I.A. must be able to represent intended behavior as a structured decision or proposal with purpose, scope, affected resources/domains, material inputs, uncertainty, and lifecycle state.

**Primary specifications:** ARIA-SPEC-EXEC-001

**Boundary:** A proposal is governable intent, not authorization.

---

### CAP-08 — Validate Consequential Actions

A.R.I.A. must be able to perform final governance evaluation immediately before consequential execution, including revalidation of authority, scope, approval/autonomy state, conditions, and material changes.

**Primary specifications:** ARIA-SPEC-EXEC-003; supporting authority and approval specifications

**Boundary:** Final governance precedes execution; capability availability never substitutes for permission.

---

### CAP-09 — Invoke Bounded Capabilities

A.R.I.A. must be able to invoke declared capabilities under explicit contracts, requirements, access boundaries, authority boundaries, and accountability obligations.

**Primary specifications:** ARIA-SPEC-EXEC-002

**Boundary:** Execution mechanisms act within granted scope; they cannot grant themselves authority.

---

### CAP-10 — Represent External Outcomes

A.R.I.A. must be able to distinguish dispatch from actual external outcome and represent success, failure, partial completion, delay, conflict, or unknown external state honestly.

**Primary specifications:** ARIA-SPEC-EXEC-004

**Boundary:** External reality determines what actually happened; authorization does not establish outcome.

---

### CAP-11 — Preserve Accountability Evidence

A.R.I.A. must be able to preserve sufficient governed evidence to reconstruct significant events, including actor, domain, authority, policy, context, proposal, approval/autonomy, execution, and external outcome as applicable.

**Primary specifications:** ARIA-SPEC-ASR-001

**Boundary:** Accountability evidence is distinct from ordinary memory and operational observability.

---

### CAP-12 — Observe and Assure System Behavior

A.R.I.A. must be able to provide governed operational visibility and security assurance sufficient to detect, investigate, validate, and demonstrate relevant system conditions without turning observability into authority.

**Primary specifications:** ARIA-SPEC-ASR-002

**Boundary:** Observability supports assurance; it does not authorize action or rewrite historical accountability evidence.

---

### CAP-13 — Contain Trust and Security Failures

A.R.I.A. must be able to reduce capability, isolate compromised or untrusted components, preserve governance boundaries, and recover trusted capability through explicit recovery processes.

**Primary specifications:** ARIA-SPEC-EVOL-001

**Boundary:** Loss of trust reduces capability; it never creates authority.

---

### CAP-14 — Govern Configuration and Evolution

A.R.I.A. must be able to govern changes to policies, domains, authority relationships, autonomy, provider restrictions, capability registrations, security requirements, retention, and other governance-affecting state.

**Primary specifications:** ARIA-SPEC-EVOL-002

**Boundary:** Governance-affecting configuration changes are themselves governed changes.

## 4. Capability Relationships

The capabilities form a governed flow rather than a simple linear execution pipeline:

```text
Identity / Domain
       │
       ├──────────────► Authority
       │                    │
Knowledge / Memory ────────┤
       │                    │
       └────► Context ◄─────┘
                 │
                 ▼
             Reasoning
                 │
                 ▼
        Decision / Proposal
                 │
                 ▼
      Approval / Autonomy
                 │
                 ▼
      Consequential Validation
                 │
                 ▼
       Bounded Capability
                 │
                 ▼
        External Outcome
                 │
                 ▼
           Accountability

Observability / Assurance ─────────► all governed boundaries
Trust / Containment ───────────────► all relevant capabilities
Configuration / Evolution ─────────► governance state and change
```

The diagram represents logical relationships, not implementation dependencies.

## 5. Cross-Capability Rules

1. No capability is an independent source of authority.
2. Identity does not imply authorization.
3. Knowledge does not imply authority.
4. Reasoning does not imply authority.
5. Provider or model trust does not imply authorization or correctness.
6. Capability availability does not imply permission to invoke it.
7. Approval does not replace authorization and cannot silently broaden scope.
8. Dispatch does not imply external success.
9. Observability does not replace accountability.
10. Trust degradation reduces capability before governance boundaries are weakened.
11. Cross-domain movement is explicit and governed.
12. Governance-affecting configuration changes are themselves governed.
13. Human disablement and takeover remain available where required by the architecture.
14. Failure, uncertainty, and unknown states cannot silently become authority or success.

## 6. Capability Acceptance Boundary

A capability is ready to become an implementation-planning input only when:

- its responsibility is unambiguous;
- its primary specification sources are identified;
- its authority boundary is explicit;
- its inputs and outputs can be expressed without implementation assumptions;
- failure and uncertainty semantics are identified;
- security/trust boundaries are preserved;
- dependencies on other capabilities are explicit;
- ownership does not create a competing source of authority;
- acceptance can be traced back to approved specifications and higher-level architecture.

## 7. Traceability

The capability baseline must remain traceable:

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

The capability layer does not replace specification-level requirements. It organizes them into implementable areas of responsibility while remaining subordinate to the approved specification layer.

## 8. What This Baseline Does Not Decide

This capability model intentionally does not decide:

- programming languages or frameworks;
- databases or storage technologies;
- APIs or protocols;
- deployment topology;
- specific vendors or providers;
- internal class/module/service boundaries;
- user-interface design;
- detailed policy precedence mechanisms;
- detailed identity assurance levels;
- detailed risk-tier or autonomy-transition models;
- other decisions explicitly deferred to ADRs.

Those decisions remain below or alongside this capability layer and must not be smuggled into capabilities.

## Governing Principle

> **Capabilities define what A.R.I.A. must be able to do. They do not define who may do it, how it is implemented, or what authority it possesses.**
