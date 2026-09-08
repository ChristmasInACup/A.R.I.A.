# A.R.I.A. Task Model

**Status:** Proposed — Phase 5 Task Baseline  
**Authority:** Derived from the approved Story Baseline, Epics, Capabilities, and Specifications  
**Last Updated:** 2026-09-08

## 1. Purpose

This document decomposes the 36 approved Stories into bounded implementation-planning Tasks. Tasks describe concrete work required to complete an approved Story while remaining subordinate to all higher-level architecture and governance.

A Task is not permission to change architecture. If completing a Task requires changing a higher-level artifact, work stops and the change is escalated through the appropriate governance process.

## 2. Task Derivation Rules

Each Task must:

- belong to exactly one primary Story;
- preserve the Story's Epic, Capability, and specification traceability;
- have a bounded objective and completion condition;
- identify meaningful dependencies where they exist;
- identify applicable acceptance, test, or evidence obligations without claiming they already exist;
- avoid silently introducing architecture, authority, security, domain, trust, or policy decisions;
- remain technology-neutral until a later approved implementation decision authorizes technology selection;
- stop and escalate if completion requires changing a higher-level requirement or architectural decision.

Tasks may be implementation-oriented, but they do not authorize implementation choices that have not been approved elsewhere.

## 3. Task Model

The baseline uses two Tasks per Story: one establishes the bounded behavior or work product, and one verifies that the Story's acceptance and governance obligations are satisfied. This is a planning decomposition pattern, not a requirement that every future implementation contain exactly two work items.

### EPIC-01 — Identity & Domain Foundation

**STORY-01 — Establish Subject Identity Context**
- **TASK-01A — Define identity-context handling:** Establish the governed handling needed to represent subject identity and lifecycle state without authorization semantics.
- **TASK-01B — Verify identity-context boundaries:** Verify authenticated, identified, authorized, and unresolved states remain distinct and traceable to STORY-01 acceptance.

**STORY-02 — Establish Governed Domain Context**
- **TASK-02A — Define domain-context handling:** Establish explicit representation and handling of domain context where it affects governance.
- **TASK-02B — Verify domain boundaries:** Verify domain context remains distinct from permission and domain movement is explicit and attributable.

**STORY-03 — Handle Invalid or Conflicting Identity**
- **TASK-03A — Establish unresolved-identity handling:** Establish bounded handling for ambiguous, expired, revoked, unavailable, or conflicting identity states.
- **TASK-03B — Verify invalid-identity containment:** Verify consequential activity cannot silently proceed on unresolved identity.

### EPIC-02 — Authority & Delegation Governance

**STORY-04 — Evaluate Effective Authorization**
- **TASK-04A — Establish authorization evaluation:** Implement the approved evaluation behavior for authority, policy, scope, purpose, domain, and lifecycle conditions without deriving authority from identity or reachability.
- **TASK-04B — Verify authorization separation:** Verify authorization remains explicit and independent of identity, technical availability, and capability availability.

**STORY-05 — Enforce Bounded Delegation**
- **TASK-05A — Establish delegation constraints:** Implement bounded delegation behavior based on source authority and granted scope.
- **TASK-05B — Verify delegation containment:** Verify delegated authority cannot exceed its source or propagate implicitly.

**STORY-06 — Revalidate Authority at Use**
- **TASK-06A — Establish use-time revalidation:** Implement the required revalidation boundary for consequential use.
- **TASK-06B — Verify authority freshness:** Verify revocation, expiry, scope changes, and other material authority changes are not silently ignored.

### EPIC-03 — Human Control & Autonomy Governance

**STORY-07 — Determine Approval Requirement**
- **TASK-07A — Establish approval determination:** Implement the governed determination of whether human approval is required for an operation.
- **TASK-07B — Verify approval separation:** Verify approval remains distinct from authorization and is determined from applicable governance conditions.

**STORY-08 — Bind Approval to Governed Action**
- **TASK-08A — Establish approval binding:** Implement bounded association between an approval and the action, scope, and conditions approved.
- **TASK-08B — Verify approval invalidation:** Verify material changes cannot silently reuse an approval outside its governed scope.

**STORY-09 — Preserve Human Disablement and Takeover**
- **TASK-09A — Establish human control path:** Implement the governed means to restrict, disable, or supersede autonomous operation where required.
- **TASK-09B — Verify independent human control:** Verify disablement and takeover do not depend on normal autonomous reasoning and remain authoritative.

### EPIC-04 — Knowledge, Memory & Provenance

**STORY-10 — Govern Information Lifecycle**
- **TASK-10A — Establish information lifecycle handling:** Implement lifecycle handling for acquisition, classification, retention, transformation, invalidation, sharing, and retrieval within approved governance.
- **TASK-10B — Verify information governance:** Verify ownership, lifecycle, sensitivity, and domain constraints remain explicit.

**STORY-11 — Govern Deliberate Memory**
- **TASK-11A — Establish governed memory handling:** Implement deliberate retention and lifecycle handling distinct from transient task context.
- **TASK-11B — Verify memory authority separation:** Verify retained memory cannot create authority or silently establish truth.

**STORY-12 — Preserve Provenance and Information Trust**
- **TASK-12A — Establish provenance handling:** Implement preservation of source, derivation, uncertainty, validity, and applicable trust relationships.
- **TASK-12B — Verify information trust boundaries:** Verify persistence and derivation do not silently upgrade information to truth.

### EPIC-05 — Context & Reasoning Coordination

**STORY-13 — Assemble Minimum-Necessary Task Context**
- **TASK-13A — Establish governed context assembly:** Implement assembly constrained by purpose, authorization, domain, sensitivity, provenance, and aggregation requirements.
- **TASK-13B — Verify context minimization:** Verify task context contains only information permitted and necessary for its authorized purpose.

**STORY-14 — Govern Cross-Domain Context Assembly**
- **TASK-14A — Establish cross-domain context controls:** Implement explicit governance for information crossing domain boundaries into task context.
- **TASK-14B — Verify cross-domain containment:** Verify technical reachability alone cannot cause cross-domain information to enter task context.

**STORY-15 — Coordinate Reasoning with Explicit Uncertainty**
- **TASK-15A — Establish governed reasoning coordination:** Implement reasoning coordination that preserves provider/model boundaries and explicit uncertainty.
- **TASK-15B — Verify reasoning authority separation:** Verify reasoning output cannot create authority or upgrade information to truth through confidence alone.

### EPIC-06 — Decision & Proposal Governance

**STORY-16 — Represent Governed Intent**
- **TASK-16A — Establish decision/proposal representation:** Implement representation of purpose, scope, affected resources or domains, material inputs, uncertainty, and lifecycle.
- **TASK-16B — Verify intent boundaries:** Verify a decision or proposal remains an expression of intended behavior rather than authorization.

**STORY-17 — Preserve Decision Traceability**
- **TASK-17A — Establish decision traceability:** Implement linkage between consequential decisions/proposals and their material context and governance basis.
- **TASK-17B — Verify decision traceability:** Verify material uncertainty and governance basis remain reconstructable without turning traceability into authority.

**STORY-18 — Handle Changed or Superseded Proposals**
- **TASK-18A — Establish proposal validity handling:** Implement detection and representation of material proposal changes or supersession.
- **TASK-18B — Verify downstream revalidation:** Verify changed proposals trigger required revalidation rather than silent reuse.

### EPIC-07 — Consequential Action Governance

**STORY-19 — Perform Final Consequential Governance Check**
- **TASK-19A — Establish final governance boundary:** Implement the final consequential check for authority, scope, approval/autonomy, and applicable conditions.
- **TASK-19B — Verify consequential gating:** Verify actions cannot reach execution without required governance conditions being satisfied.

**STORY-20 — Reject Governance-Boundary Violations**
- **TASK-20A — Establish governance rejection behavior:** Implement bounded rejection for actions lacking required authority or approval.
- **TASK-20B — Verify bypass resistance:** Verify technical availability cannot bypass governance and failure cannot grant additional authority.

**STORY-21 — Handle Material Concurrent Change**
- **TASK-21A — Establish concurrent-change detection:** Implement recognition of material changes between planning and consequential use.
- **TASK-21B — Verify stale-condition handling:** Verify stale authority, approval, scope, or conditions cannot silently govern changed actions.

### EPIC-08 — Bounded Capability Execution

**STORY-22 — Define Governed Capability Contract**
- **TASK-22A — Establish capability contract:** Implement representation of capability requirements, access boundaries, authority constraints, and accountability obligations.
- **TASK-22B — Verify contract boundaries:** Verify the contract does not create authority beyond the approved governing layers.

**STORY-23 — Invoke Capability Within Granted Scope**
- **TASK-23A — Establish bounded capability invocation:** Implement invocation constrained by the approved capability contract and granted scope.
- **TASK-23B — Verify invocation containment:** Verify capability availability cannot broaden permission or authority.

**STORY-24 — Contain Capability Failure**
- **TASK-24A — Establish capability failure handling:** Implement explicit failure representation and containment for governed capability execution.
- **TASK-24B — Verify failure honesty and containment:** Verify failures remain attributable, cannot be represented as success, and reduce capability where required.

### EPIC-09 — External Outcome & State Reconciliation

**STORY-25 — Distinguish Dispatch from Outcome**
- **TASK-25A — Establish dispatch/outcome separation:** Implement separate handling for action dispatch and externally observed outcome.
- **TASK-25B — Verify outcome evidence:** Verify dispatch cannot be represented as external success without appropriate evidence.

**STORY-26 — Represent Partial, Failed, Delayed, or Conflicting Outcomes**
- **TASK-26A — Establish non-success outcome handling:** Implement distinct representation for partial, failed, delayed, and conflicting external results.
- **TASK-26B — Verify outcome-state integrity:** Verify materially different external states remain distinguishable and attributable.

**STORY-27 — Preserve Unknown External State**
- **TASK-27A — Establish unknown-state handling:** Implement explicit representation of insufficient evidence about external reality.
- **TASK-27B — Verify unknown-state preservation:** Verify unknown cannot silently become success, failure, certainty, or authority.

### EPIC-10 — Accountability & Audit Evidence

**STORY-28 — Capture Significant Governed Event Evidence**
- **TASK-28A — Establish governed event evidence capture:** Implement capture of applicable identity/domain, authority, policy, context, proposal, approval/autonomy, execution, and outcome evidence.
- **TASK-28B — Verify evidence completeness:** Verify significant governed events preserve sufficient attributable evidence for applicable reconstruction.

**STORY-29 — Preserve Accountability Evidence Integrity**
- **TASK-29A — Establish evidence protection:** Implement the approved protection and lifecycle behavior for accountability evidence.
- **TASK-29B — Verify evidence integrity:** Verify evidence cannot be silently altered or discarded in ways that defeat required accountability.

**STORY-30 — Reconstruct Consequential Activity**
- **TASK-30A — Establish reconstruction capability:** Implement reconstruction of significant consequential activity from preserved evidence according to applicable risk.
- **TASK-30B — Verify reconstruction distinctions:** Verify reconstruction distinguishes intent, governance, execution, and external outcome.

### EPIC-11 — Observability, Assurance & Containment

**STORY-31 — Provide Governed Operational Observability**
- **TASK-31A — Establish governed observability:** Implement observation of relevant system conditions within approved authority and privacy/security boundaries.
- **TASK-31B — Verify observability separation:** Verify observability supports assurance without authorizing action or rewriting accountability evidence.

**STORY-32 — Detect and Isolate Trust or Security Failure**
- **TASK-32A — Establish trust/security containment:** Implement bounded detection and isolation of compromised or untrusted conditions.
- **TASK-32B — Verify capability reduction:** Verify loss of trust reduces capability while preserving unrelated authority boundaries.

**STORY-33 — Recover Trusted Capability Through Governance**
- **TASK-33A — Establish governed recovery process:** Implement the approved process for restoring trusted capability after a trust or security failure.
- **TASK-33B — Verify recovery containment:** Verify recovery cannot silently restore broader authority than was previously governed.

### EPIC-12 — Configuration, Governance & Evolution

**STORY-34 — Govern Changes to Governance State**
- **TASK-34A — Establish governance-change handling:** Implement controlled handling for changes to policies, authority relationships, domains, autonomy, restrictions, registrations, security requirements, or retention.
- **TASK-34B — Verify governance-change accountability:** Verify material governance-affecting changes are attributable, controlled, and subject to applicable approval.

**STORY-35 — Preserve Governance Across State Migration**
- **TASK-35A — Establish governed state migration:** Implement movement or migration of state while preserving ownership, provenance, authority, validity, and applicable governance.
- **TASK-35B — Verify migration preservation:** Verify migration cannot silently discard governance relationships or change truth or authority status.

**STORY-36 — Prevent Architectural Drift Through Change**
- **TASK-36A — Establish change traceability:** Implement traceability from governance-affecting changes to their approved higher-level authority.
- **TASK-36B — Verify architectural drift controls:** Verify ordinary implementation or configuration changes cannot silently redefine constitutional, invariant, architectural, or specification-level behavior.

## 4. Cross-Task Rules

1. A Task cannot create authority that its parent Story, Epic, Capability, or Specification does not possess.
2. A Task cannot use implementation convenience to redefine a higher-level requirement.
3. Technology selection is not authorized merely because a Task is implementation-oriented.
4. Identity remains distinct from authorization.
5. Knowledge, memory, reasoning, provider/model trust, and observability remain distinct from authority.
6. Approval remains distinct from authorization.
7. Capability availability remains distinct from permission.
8. Dispatch remains distinct from external outcome.
9. Accountability evidence remains distinct from ordinary memory and observability.
10. Trust degradation reduces capability before governance boundaries are weakened.
11. Failure, uncertainty, and unknown states remain explicit.
12. Cross-domain movement remains explicitly governed.
13. Human disablement and takeover remain preserved where required.
14. A dependency does not transfer authority.
15. Any Task requiring a higher-level change must stop and trigger governance escalation.

## 5. Task Completion Boundary

A Task is ready for implementation when:

- its parent Story and full upward traceability are explicit;
- its objective and completion condition are bounded;
- dependencies and governance boundaries are understood;
- applicable acceptance, test, and evidence obligations are identified;
- required implementation choices are either already authorized by higher-level artifacts or explicitly deferred;
- no hidden authority, security, trust, domain, or policy decision is embedded;
- completion can be demonstrated and traced back to the parent Story.

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

The 72 Tasks in this baseline are planning targets. Their existence does not claim implementation, tests, or evidence.

## 7. What This Baseline Does Not Decide

This Task Model intentionally does not select or mandate:

- programming languages or frameworks;
- databases or storage technologies;
- APIs or protocols;
- deployment topology;
- vendors or external providers;
- UI or presentation technology;
- specific internal class/module/service structures;
- estimates, staffing, or scheduling;
- deferred ADR decisions.

Those decisions must occur only at the appropriate governed layer.

## 8. Governing Principle

> **Tasks make approved Stories actionable; they do not create authority, redefine requirements, or silently choose architecture.**
