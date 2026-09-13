# M1 Slice 3 — Governed Proposal → Decision Boundary

**Status:** Authorized — M1 Slice 3
**Milestone:** M1 / M2 boundary
**Purpose:** Prove that A.R.I.A. can produce a governed, traceable recommendation from authorized context and expose it to a decision boundary without allowing the proposal to become an authorization, approval, or execution instruction.

## Story

**As A.R.I.A.,** given an authorized request and authorized context, I can produce a governed proposal whose recommendation, evidence, provenance, uncertainty, scope, and intended effect remain explicit, while the proposal remains non-authoritative and cannot approve, authorize, broaden authority, or execute anything.

## Vertical Path

```text
Governed Request
      ↓
   Identity
      ↓
    Domain
      ↓
 Authorization
      ↓
 Knowledge Governance
      ↓
 Minimum-Necessary Context
      ↓
     Proposal
      ↓
 Decision Boundary
      ↓
 Decision / Approval (future slice)
      ↓
 Execution (future slice)
```

This slice establishes the boundary; it does not implement autonomous decision, approval, or execution.

## Core Boundary

A.R.I.A. must preserve three distinct concepts:

```text
Recommendation ≠ Decision ≠ Execution
```

- **Recommendation:** what the proposal suggests should happen and why.
- **Decision:** an authorized governance determination about whether a proposed action may proceed. A decision is not created merely because a proposal exists.
- **Execution:** carrying out an authorized and approved action against an external capability/system. Execution is outside this slice.

A proposal may cross the decision boundary for governance evaluation, but crossing that boundary must not itself constitute approval or authorization.

## Proposal Definition

A governed proposal is a non-authoritative, immutable-at-the-boundary representation of a recommendation derived from an authorized request and authorized context.

At minimum, a proposal must preserve:

1. **Stable proposal identity** — unique identity for the proposal.
2. **Revision identity/version** — material changes must be distinguishable from the prior proposal.
3. **Request linkage** — the originating subject, domain, purpose, and requested scope.
4. **Authorization basis** — the authorization that permitted the proposal's source context; this is evidence/basis, not new authority.
5. **Recommendation** — the proposed course of action, expressed as recommendation rather than command or authorization.
6. **Affected domain/resources** — explicit proposed domain and resources.
7. **Intended effect** — what the recommendation is intended to accomplish.
8. **Relevant evidence/material** — the governed context/material on which the recommendation relies.
9. **Provenance** — origin and source linkage sufficient to trace the proposal back to its governing context.
10. **Uncertainty** — known, uncertain, or unknown state must remain explicit.
11. **Conditions/constraints** — material conditions that affect whether the recommendation is appropriate.
12. **Lifecycle state** — proposal state such as Draft, Superseded, Expired, Rejected, or Unresolved where applicable to this slice.
13. **Creation/origin metadata** — who or what produced the proposal, without treating that producer as an authority source.

The proposal must not contain a field whose presence can itself confer authority.

## Proposal Authority Rules

The following are hard invariants for this slice:

- A proposal **cannot grant authority**.
- A proposal **cannot broaden authority**.
- A proposal **cannot delegate authority**.
- A proposal **cannot approve itself**.
- A proposal **cannot convert recommendation into authorization**.
- A proposal **cannot bypass the authorization boundary**.
- A proposal **cannot cause execution**.
- Proposal content, including model-generated or externally supplied text, is untrusted with respect to authority.
- The proposal's affected domain and resources cannot exceed the authorized request/context boundary.
- Adding information to a proposal cannot broaden the authorization from which its context was obtained.
- A proposal must not be treated as an authority-bearing substitute for `AuthorizationGrant`.

## Provenance Boundary

Proposal provenance must remain traceable through the complete chain:

```text
Request
  → Authorization
  → Governed Context
  → Proposal
```

The implementation must preserve enough linkage to establish:

- which request produced the proposal;
- which authorization basis permitted the source context;
- which context/material contributed to the proposal;
- which provenance was inherited from that material;
- which proposal revision is being evaluated.

M1 does not require persistent audit storage, cryptographic provenance verification, or distributed provenance infrastructure.

## Uncertainty and Conflict Rules

- Unknown information remains unknown.
- Uncertain information remains uncertain.
- Conflicting source/context information remains conflicting or unresolved.
- A proposal must not silently convert uncertainty or conflict into certainty.
- Missing required governance information produces an explicit invalid/unresolved result rather than a best-effort approval.
- Stale, expired, revoked, or otherwise invalid authorization/context must not produce a normally governed proposal.
- If material proposal inputs change, the existing proposal must not silently represent the changed state; a new revision/identity or explicit invalidation/supersession is required.

## Decision Boundary

This slice may expose a boundary evaluator whose result is limited to governance status. It may determine whether a proposal is structurally eligible for downstream decision evaluation, or that evaluation is denied/unresolved/rejected.

The boundary evaluator must not:

- approve a proposal;
- create authorization;
- broaden authorization;
- select an execution capability;
- invoke a capability;
- execute an action;
- establish autonomous authority;
- treat proposal text as policy.

A successful boundary evaluation means only that the proposal is a valid candidate for a later governance decision. It is **not** an approval.

## Scope

1. Define and enforce the governed proposal boundary.
2. Preserve request and authorization linkage.
3. Preserve proposal provenance through authorized context.
4. Preserve uncertainty and unresolved/conflicting state.
5. Represent recommendation, affected scope, intended effect, evidence, and conditions distinctly from authorization.
6. Prevent proposal scope/domain/purpose from exceeding the authorized request/context.
7. Detect material proposal revision/change.
8. Reject or mark unresolved proposals with missing or contradictory governance state.
9. Establish a non-authorizing decision-boundary result.
10. Preserve all prior M1 Slice 1 and Slice 2 behavior through regression tests.

## Acceptance Criteria

- [ ] A proposal can be created only from authorized context.
- [ ] A proposal has stable identity and distinguishable revision/change semantics.
- [ ] The originating request and authorization basis remain traceable.
- [ ] Proposal recommendation is distinct from authorization.
- [ ] Proposed domain exactly matches the governed request/context domain.
- [ ] Proposed purpose does not exceed the governed request purpose.
- [ ] Proposed resources are contained within the authorized request/context scope.
- [ ] A proposal cannot broaden the authority represented by its authorization basis.
- [ ] A proposal cannot create, grant, or delegate authority.
- [ ] A proposal cannot approve itself.
- [ ] A proposal cannot bypass authorization.
- [ ] A proposal cannot trigger execution.
- [ ] Provenance from contributing context remains preserved.
- [ ] Uncertainty remains explicit.
- [ ] Conflicting material remains explicit or produces an unresolved result.
- [ ] Missing required governance information fails safely.
- [ ] Stale/invalid/revoked authorization or context cannot yield a normally governed proposal.
- [ ] Material proposal changes are detectable and cannot silently reuse prior governance state.
- [ ] Malicious or instruction-like proposal content cannot redefine governance.
- [ ] Decision-boundary evaluation does not constitute approval or authorization.
- [ ] No autonomous approval path exists.
- [ ] No execution path exists.
- [ ] All M1 Slice 1 and Slice 2 regression tests remain green.

## Required Tests

### Proposal Formation

- Authorized context produces a valid proposal.
- Unauthorized/missing authorization cannot produce a valid proposal.
- Request linkage is preserved.
- Authorization basis is preserved as evidence, not authority created by the proposal.
- Proposal identity and revision/change semantics are explicit.

### Scope and Authority Containment

- Proposed scope equal to authorized scope is accepted.
- Proposed scope narrower than authorized scope is accepted.
- Proposed scope broader than authorized scope is rejected/unresolved.
- Wrong-domain proposal is rejected.
- Wrong-purpose proposal is rejected.
- Proposal cannot create or broaden authority.
- Proposal cannot delegate authority.

### Provenance and Uncertainty

- Context provenance survives into the proposal.
- Multiple contributing sources retain traceability.
- Known information remains known.
- Uncertain information remains uncertain.
- Unknown information remains unknown.
- Conflicting source material remains explicit/unresolved.

### Lifecycle / Change

- Invalid or expired source authorization cannot produce a valid proposal.
- Invalid/stale context cannot silently produce a valid proposal.
- Material proposal changes create a distinguishable revision/identity or equivalent traceable invalidation.
- A superseded/stale proposal cannot be treated as the current proposal without explicit validation.

### Decision Boundary

- Valid proposal reaches the boundary as a candidate for downstream governance.
- Invalid proposal is rejected/unresolved at the boundary.
- Boundary evaluation never returns an approval merely because a proposal is valid.
- Boundary evaluation never creates or broadens authorization.
- Boundary evaluation never invokes execution.
- No autonomous approval path exists.

### Security / Adversarial

- Proposal text containing instructions such as "ignore authorization" is treated as untrusted content.
- Proposal text cannot redefine domain, purpose, scope, policy, or authority.
- Injected content cannot cause execution or approval.
- Adding unauthorized context cannot improve proposal authority.

### Regression

- Full M1 Slice 1 regression suite remains green.
- Full M1 Slice 2 regression suite remains green.

## Architectural References

- A.R.I.A. Constitution
- A.R.I.A. Architectural Invariants
- Conceptual Architecture v1.0
- Implementation Architecture — proposal, governance, security, and invariant boundaries
- ARIA-SPEC-EXEC-001 — Decision & Proposal Specification
- ARIA-SPEC-AUTH-001 — Authorization & Policy
- ARIA-SPEC-AUTH-002 — Delegation & Authority
- ARIA-SPEC-AUTH-003 — Approval, Autonomy & Human Control
- ARIA-SPEC-KNOW-003 — Provenance / Information Trust
- ARIA-SPEC-KNOW-001 — Knowledge Lifecycle
- Implementation Charter
- Autonomous Implementation Protocol
- Implementation Backlog — applicable proposal/governance stories and tasks
- ADR-0002 — M1 Delegation Model

## Security Requirements

- Authorization remains the sole source of permission for the current request path.
- Proposal content is not an authority source.
- Proposal provenance is security-relevant governance state and must not be discarded.
- Technical validity of a proposal must not be confused with permission to execute it.
- A proposal must remain bounded by the authorization that enabled its context.
- Failure or ambiguity reduces capability; it never increases authority.
- Malicious content must remain contained as data.
- The decision boundary must not become an implicit approval or execution boundary.

## Dependencies

- M1 Slice 1 identity/domain/authorization behavior.
- M1 Slice 2 governed knowledge/context behavior.
- Approved decision/proposal, authorization, delegation, knowledge, and human-control specifications.
- Existing `GovernedProposal` and authorized-context domain model.

## Explicit Non-Goals

- LLM or reasoning-provider integration.
- Provider/model selection.
- Persistent proposal storage.
- Distributed proposal coordination.
- Human approval workflow.
- Autonomous approval.
- Autonomy-level transitions.
- Capability selection/execution.
- External side effects.
- Production UI/API hosting.
- Cryptographic provenance verification.
- Full audit persistence.
- Risk-tier implementation unless already required by an approved specification for this slice.

## Mandatory Stop Conditions

Stop implementation and request human resolution if:

- an approved specification conflicts with the Constitution or architectural invariants;
- defining the proposal requires a new authority source;
- the implementation would require treating a proposal as authorization;
- the decision boundary cannot be established without implementing approval/autonomy;
- material proposal semantics are ambiguous;
- provenance or uncertainty requirements cannot be preserved without changing an approved requirement;
- implementation requires a durable architectural decision not already authorized;
- scope would materially expand beyond this slice;
- required evidence cannot be established without changing an approved requirement.

## Definition of Done

- [ ] Proposal semantics are implemented within the locked boundary.
- [ ] Decision-boundary semantics are implemented without approval/autonomy/execution.
- [ ] Acceptance criteria are satisfied.
- [ ] Applicable invariants are preserved and tested.
- [ ] Proposal provenance and uncertainty are preserved.
- [ ] Scope/domain/purpose containment is enforced.
- [ ] Material change/revision behavior is explicit and tested.
- [ ] Adversarial proposal content is contained as data.
- [ ] Slice 1 and Slice 2 regression suites pass.
- [ ] Security implications are validated.
- [ ] Unknown, conflict, invalid, and failure behavior is explicit.
- [ ] Relevant evidence is recorded.
- [ ] CI passes.
- [ ] Pull request clearly identifies scope, tests, evidence, limitations, and non-goals.
- [ ] Final human review remains available at the applicable governance boundary.

## Completion Boundary

This slice is complete only when A.R.I.A. can reliably distinguish recommendation from decision and execution, preserve proposal governance state, and expose a proposal to a non-authorizing decision boundary without creating an authority path. Completion does not authorize autonomous approval, capability execution, or external side effects.
