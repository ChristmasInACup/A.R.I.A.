# A.R.I.A. Story Model

**Status:** Proposed — Phase 4 Story Baseline  
**Authority:** Derived from the approved Capability Baseline and approved Epic Model  
**Last Updated:** 2026-09-08

## 1. Purpose

This document decomposes the 12 approved planning epics into concrete, independently reviewable bodies of planned work at the Story level.

Stories describe **what a bounded piece of work must accomplish and how its completion can be accepted**. They do not define implementation technology, code structure, APIs, databases, deployment mechanisms, vendors, estimates, or task sequencing.

A Story must remain subordinate to its Epic, Capability, approved Specifications, and all higher-level architectural artifacts.

## 2. Story Derivation Rules

Each Story must:

- belong to exactly one primary Epic;
- trace to at least one approved Capability and its governing specification(s);
- represent a coherent, independently reviewable outcome;
- have acceptance conditions that describe observable required behavior or governance state without prescribing implementation;
- preserve authority, identity, knowledge, reasoning, execution, trust, accountability, domain, and human-control boundaries;
- identify relevant failure or uncertainty behavior where applicable;
- avoid introducing requirements not present in higher-level artifacts;
- stop and escalate when completion would require changing a higher-level architectural decision.

Stories may have dependencies on other Stories, but a dependency does not transfer authority between them.

## 3. Story Model

### EPIC-01 — Identity & Domain Foundation

**STORY-01 — Establish Subject Identity Context**  
**Capability:** CAP-01  
**Specifications:** ARIA-SPEC-ID-001  
**Outcome:** A governed interaction can establish the relevant actor or system subject and its lifecycle state without granting authority.  
**Acceptance:** Identity context distinguishes subject from authorization; lifecycle state is explicit; unresolved identity remains unresolved.

**STORY-02 — Establish Governed Domain Context**  
**Capability:** CAP-01  
**Specifications:** ARIA-SPEC-ID-001  
**Outcome:** Relevant domain context is explicitly represented when it affects governance.  
**Acceptance:** Domain is distinct from permission; applicable domain context is attributable; domain movement is not implicit.

**STORY-03 — Handle Invalid or Conflicting Identity**  
**Capability:** CAP-01  
**Specifications:** ARIA-SPEC-ID-001  
**Outcome:** Ambiguous, expired, revoked, unavailable, or conflicting identity produces an explicit non-authoritative state.  
**Acceptance:** Invalid identity cannot silently become trusted or authorized; consequential activity requiring resolved identity does not proceed.

### EPIC-02 — Authority & Delegation Governance

**STORY-04 — Evaluate Effective Authorization**  
**Capability:** CAP-02  
**Specifications:** ARIA-SPEC-AUTH-001  
**Outcome:** A requested operation can be evaluated against applicable authority, policy, scope, purpose, domain, and lifecycle conditions.  
**Acceptance:** Authorization is explicit; identity, reachability, or capability availability cannot substitute for it.

**STORY-05 — Enforce Bounded Delegation**  
**Capability:** CAP-02  
**Specifications:** ARIA-SPEC-AUTH-002  
**Outcome:** Delegated authority remains bounded by the authority of its source and the granted scope.  
**Acceptance:** Delegation cannot exceed delegator authority; authority does not propagate implicitly.

**STORY-06 — Revalidate Authority at Use**  
**Capability:** CAP-02  
**Specifications:** ARIA-SPEC-AUTH-001, ARIA-SPEC-EXEC-003  
**Outcome:** Consequential use relies on authority valid at the applicable decision boundary.  
**Acceptance:** Revocation, expiry, scope change, or other material authority change cannot be silently ignored.

### EPIC-03 — Human Control & Autonomy Governance

**STORY-07 — Determine Approval Requirement**  
**Capability:** CAP-03  
**Specifications:** ARIA-SPEC-AUTH-003  
**Outcome:** A governed operation can determine whether human approval is required.  
**Acceptance:** Approval requirements are explicit; approval is distinct from authorization.

**STORY-08 — Bind Approval to Governed Action**  
**Capability:** CAP-03  
**Specifications:** ARIA-SPEC-AUTH-003  
**Outcome:** Human approval applies only to the governed scope and action for which it was granted.  
**Acceptance:** Material changes cannot silently reuse approval; approval cannot broaden authority.

**STORY-09 — Preserve Human Disablement and Takeover**  
**Capability:** CAP-03  
**Specifications:** ARIA-SPEC-AUTH-003, ARIA-SPEC-EVOL-001  
**Outcome:** Human control can restrict, disable, or supersede autonomous operation where required.  
**Acceptance:** Disablement and takeover do not depend on normal autonomous reasoning; human control remains authoritative.

### EPIC-04 — Knowledge, Memory & Provenance

**STORY-10 — Govern Information Lifecycle**  
**Capability:** CAP-04  
**Specifications:** ARIA-SPEC-KNOW-001  
**Outcome:** Information can be governed through acquisition, classification, retention, transformation, invalidation, sharing, and retrieval.  
**Acceptance:** Ownership, lifecycle, sensitivity, and domain constraints remain explicit.

**STORY-11 — Govern Deliberate Memory**  
**Capability:** CAP-04  
**Specifications:** ARIA-SPEC-KNOW-002  
**Outcome:** Retained memory has explicit lifecycle and governance semantics distinct from transient task context.  
**Acceptance:** Memory cannot create authority; retention does not silently make information authoritative.

**STORY-12 — Preserve Provenance and Information Trust**  
**Capability:** CAP-04  
**Specifications:** ARIA-SPEC-KNOW-003  
**Outcome:** Information retains sufficient provenance and trust status to distinguish source, derivation, uncertainty, and validity.  
**Acceptance:** Persistence does not establish truth; derived information preserves required governance relationships.

### EPIC-05 — Context & Reasoning Coordination

**STORY-13 — Assemble Minimum-Necessary Task Context**  
**Capability:** CAP-05  
**Specifications:** ARIA-SPEC-REAS-001  
**Outcome:** A task receives only context permitted and necessary for its authorized purpose.  
**Acceptance:** Purpose, authorization, domain, sensitivity, provenance, and aggregation constraints are enforced conceptually.

**STORY-14 — Govern Cross-Domain Context Assembly**  
**Capability:** CAP-05  
**Specifications:** ARIA-SPEC-REAS-001, ARIA-SPEC-KNOW-001  
**Outcome:** Context crossing domain boundaries is explicitly governed.  
**Acceptance:** Cross-domain information cannot enter task context merely because it is technically reachable.

**STORY-15 — Coordinate Reasoning with Explicit Uncertainty**  
**Capability:** CAP-06  
**Specifications:** ARIA-SPEC-REAS-002, ARIA-SPEC-REAS-003  
**Outcome:** Reasoning can analyze authorized context while representing uncertainty and preserving provider/model trust boundaries.  
**Acceptance:** Reasoning output cannot create authority or upgrade information to truth merely through confidence.

### EPIC-06 — Decision & Proposal Governance

**STORY-16 — Represent Governed Intent**  
**Capability:** CAP-07  
**Specifications:** ARIA-SPEC-EXEC-001  
**Outcome:** Intended behavior is represented as a structured decision or proposal.  
**Acceptance:** Purpose, scope, affected resources/domains, material inputs, uncertainty, and lifecycle are explicit.

**STORY-17 — Preserve Decision Traceability**  
**Capability:** CAP-07  
**Specifications:** ARIA-SPEC-EXEC-001, ARIA-SPEC-ASR-001  
**Outcome:** A decision or proposal can be connected to the material context and governance basis that produced it.  
**Acceptance:** Decision representation does not become authorization; material uncertainty remains visible.

**STORY-18 — Handle Changed or Superseded Proposals**  
**Capability:** CAP-07  
**Specifications:** ARIA-SPEC-EXEC-001, ARIA-SPEC-EXEC-003  
**Outcome:** A proposal whose material conditions change can be identified as no longer valid for the intended consequential action.  
**Acceptance:** Material change triggers appropriate downstream revalidation rather than silent reuse.

### EPIC-07 — Consequential Action Governance

**STORY-19 — Perform Final Consequential Governance Check**  
**Capability:** CAP-08  
**Specifications:** ARIA-SPEC-EXEC-003  
**Outcome:** A consequential action reaches execution only after required governance conditions are evaluated.  
**Acceptance:** Authority, scope, approval/autonomy, and relevant conditions are revalidated at the consequential boundary.

**STORY-20 — Reject Governance-Boundary Violations**  
**Capability:** CAP-08  
**Specifications:** ARIA-SPEC-EXEC-003, ARIA-SPEC-AUTH-001, ARIA-SPEC-AUTH-003  
**Outcome:** Actions lacking required authority or approval are prevented from consequential execution.  
**Acceptance:** Technical availability cannot bypass governance; failure does not grant additional authority.

**STORY-21 — Handle Material Concurrent Change**  
**Capability:** CAP-08  
**Specifications:** ARIA-SPEC-EXEC-003, ARIA-SPEC-AUTH-001  
**Outcome:** Material changes between planning and consequential use are recognized before action proceeds.  
**Acceptance:** Stale authority, approval, scope, or conditions cannot silently govern a changed action.

### EPIC-08 — Bounded Capability Execution

**STORY-22 — Define Governed Capability Contract**  
**Capability:** CAP-09  
**Specifications:** ARIA-SPEC-EXEC-002  
**Outcome:** A declared capability has explicit contractual boundaries relevant to its governed invocation.  
**Acceptance:** Requirements, access, authority, and accountability boundaries are explicit without prescribing implementation.

**STORY-23 — Invoke Capability Within Granted Scope**  
**Capability:** CAP-09  
**Specifications:** ARIA-SPEC-EXEC-002  
**Outcome:** An approved capability invocation remains within its granted scope and contract.  
**Acceptance:** Capability availability cannot broaden authority or permission.

**STORY-24 — Contain Capability Failure**  
**Capability:** CAP-09  
**Specifications:** ARIA-SPEC-EXEC-002, ARIA-SPEC-EVOL-001  
**Outcome:** Capability failure is represented and contained without creating new authority or falsely declaring success.  
**Acceptance:** Failure remains attributable; containment reduces capability where required.

### EPIC-09 — External Outcome & State Reconciliation

**STORY-25 — Distinguish Dispatch from Outcome**  
**Capability:** CAP-10  
**Specifications:** ARIA-SPEC-EXEC-004  
**Outcome:** Dispatch of an action is represented separately from what occurred externally.  
**Acceptance:** Dispatch cannot be represented as external success without appropriate outcome evidence.

**STORY-26 — Represent Partial, Failed, Delayed, or Conflicting Outcomes**  
**Capability:** CAP-10  
**Specifications:** ARIA-SPEC-EXEC-004  
**Outcome:** External results can be represented honestly across non-success states.  
**Acceptance:** Partial, failed, delayed, and conflicting states remain distinguishable.

**STORY-27 — Preserve Unknown External State**  
**Capability:** CAP-10  
**Specifications:** ARIA-SPEC-EXEC-004  
**Outcome:** Insufficient evidence about external reality remains explicitly unknown.  
**Acceptance:** Unknown cannot silently become success, failure, certainty, or authority.

### EPIC-10 — Accountability & Audit Evidence

**STORY-28 — Capture Significant Governed Event Evidence**  
**Capability:** CAP-11  
**Specifications:** ARIA-SPEC-ASR-001  
**Outcome:** Significant governed events retain sufficient attributable evidence for later reconstruction.  
**Acceptance:** Relevant identity/domain, authority, policy, context, proposal, approval/autonomy, execution, and outcome evidence is preserved as applicable.

**STORY-29 — Preserve Accountability Evidence Integrity**  
**Capability:** CAP-11  
**Specifications:** ARIA-SPEC-ASR-001, ARIA-SPEC-ASR-002  
**Outcome:** Accountability evidence remains protected as a governance trust boundary.  
**Acceptance:** Evidence cannot be silently altered or discarded in ways that defeat required accountability.

**STORY-30 — Reconstruct Consequential Activity**  
**Capability:** CAP-11  
**Specifications:** ARIA-SPEC-ASR-001  
**Outcome:** Sufficient evidence exists to reconstruct significant consequential activity according to applicable risk.  
**Acceptance:** Reconstruction distinguishes intent, governance, execution, and external outcome.

### EPIC-11 — Observability, Assurance & Containment

**STORY-31 — Provide Governed Operational Observability**  
**Capability:** CAP-12  
**Specifications:** ARIA-SPEC-ASR-002  
**Outcome:** Relevant system conditions can be observed and investigated without creating authority.  
**Acceptance:** Observability supports assurance but cannot authorize action or rewrite accountability evidence.

**STORY-32 — Detect and Isolate Trust or Security Failure**  
**Capability:** CAP-13  
**Specifications:** ARIA-SPEC-EVOL-001, ARIA-SPEC-ASR-002  
**Outcome:** Compromised or untrusted conditions can be identified and bounded.  
**Acceptance:** Trust loss reduces capability and preserves unrelated authority boundaries.

**STORY-33 — Recover Trusted Capability Through Governance**  
**Capability:** CAP-13  
**Specifications:** ARIA-SPEC-EVOL-001  
**Outcome:** Recovery from a trust or security failure occurs through an explicit governed process.  
**Acceptance:** Recovery does not silently restore broader authority than was previously governed.

### EPIC-12 — Configuration, Governance & Evolution

**STORY-34 — Govern Changes to Governance State**  
**Capability:** CAP-14  
**Specifications:** ARIA-SPEC-EVOL-002  
**Outcome:** Changes to policies, authority relationships, domains, autonomy, restrictions, registrations, security requirements, or retention are themselves governed.  
**Acceptance:** Material governance-affecting changes are attributable, controlled, and subject to applicable approval.

**STORY-35 — Preserve Governance Across State Migration**  
**Capability:** CAP-14  
**Specifications:** ARIA-SPEC-EVOL-002, ARIA-SPEC-KNOW-001  
**Outcome:** State movement or migration preserves ownership, provenance, authority, validity, and applicable governance.  
**Acceptance:** Migration cannot silently discard governance relationships or change truth/authority status.

**STORY-36 — Prevent Architectural Drift Through Change**  
**Capability:** CAP-14  
**Specifications:** ARIA-SPEC-EVOL-002  
**Outcome:** Governance-affecting change remains traceable to approved higher-level authority.  
**Acceptance:** Ordinary implementation or configuration change cannot silently redefine constitutional, invariant, architectural, or specification-level behavior.

## 4. Cross-Story Rules

1. A Story cannot create authority that its parent Epic, Capability, or Specification does not possess.
2. A Story cannot use implementation convenience to redefine a higher-level requirement.
3. Identity remains distinct from authorization.
4. Knowledge, memory, reasoning, provider/model trust, and observability remain distinct from authority.
5. Approval remains distinct from authorization.
6. Capability availability remains distinct from permission.
7. Dispatch remains distinct from external outcome.
8. Accountability evidence remains distinct from ordinary memory and observability.
9. Trust degradation reduces capability before governance boundaries are weakened.
10. Failure, uncertainty, and unknown states remain explicit and cannot silently become authority or success.
11. Cross-domain movement remains explicitly governed.
12. Human disablement and takeover remain preserved where required.
13. Stories may expose unresolved requirements, but may not resolve deferred architectural decisions by assumption.
14. Any Story that cannot be completed without changing a higher-level artifact must stop and trigger the appropriate governance process.

## 5. Story Acceptance Boundary

A Story is ready for Task decomposition only when:

- its Epic, Capability, and primary specification sources are explicit;
- its intended outcome is bounded and independently reviewable;
- acceptance conditions are observable without prescribing implementation;
- dependencies and governance boundaries are understood;
- failure, uncertainty, security, trust, and accountability implications are addressed where applicable;
- no unstated architecture or technology decision is embedded;
- its acceptance can be traced upward and later verified downward through Tasks, Tests, and Evidence.

## 6. Traceability

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

The 36 Stories in this baseline are planning targets. Their existence does not claim implementation, tests, or evidence.

## 7. What This Baseline Does Not Decide

This Story Model intentionally does not decide:

- programming languages or frameworks;
- databases or storage technologies;
- APIs or protocols;
- deployment topology;
- vendors or providers;
- internal class/module/service boundaries;
- UI design;
- task sequencing or estimates;
- detailed policy precedence, identity assurance levels, risk tiers, or autonomy-transition mechanisms deferred to ADRs.

## Governing Principle

> **Stories define bounded, reviewable work; they do not create authority, redefine requirements, or decide implementation.**
