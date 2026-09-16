# M4 Slice 4 — Approval and Consequential Governance

**Status:** Proposed — Slice Definition
**Milestone:** M4
**Purpose:** Prove that A.R.I.A. can take a governed proposal through explicit approval/autonomy evaluation and an immediate pre-execution governance validation boundary without allowing approval, autonomy, or validation to create broader authority or perform execution.

## Governing Boundary

The locked implementation strategy defines Slice 4 as:

```text
Proposal
   ↓
Approval / Autonomy Evaluation
   ↓
Immediate Pre-Execution Governance Validation
   ↓
[execution boundary — Slice 5]
```

Slice 4 establishes the governance gate immediately before consequential execution. It does **not** invoke capabilities, perform external side effects, or implement the bounded execution behavior owned by Slice 5.

A successful Slice 4 result means only that the proposed consequential action has passed the applicable governance checks at that point in time. It is not execution and does not establish new authority.

## Story

**As A.R.I.A.,** given a governed proposal and its existing authorization context, I can determine whether explicit approval is required or bounded autonomy permits the action, preserve the resulting governance evidence, and revalidate the complete consequential-action boundary immediately before execution so that stale, revoked, changed, disabled, or otherwise invalid state cannot silently proceed.

## Vertical Path

```text
Governed Request
      ↓
 Identity / Domain
      ↓
 Authorization
      ↓
 Governed Knowledge / Context
      ↓
 Proposal
      ↓
 Proposal Decision Boundary
      ↓
 Approval / Autonomy Evaluation
      ↓
 Immediate Pre-Execution Governance Validation
      ↓
 Execution Gate Result
      ↓
 [Slice 5: Capability Invocation]
```

## Core Authority Rules

The following are hard invariants for this slice:

- Authorization remains the source of permission for the action.
- Approval does not create authority broader than the existing authorization.
- Autonomy is bounded standing authority, not model discretion.
- A model output, proposal, or capability output cannot substitute for required human approval.
- Approval is explicit where required and bound to the particular governed action.
- Approval evidence preserves the approved action scope, purpose, conditions, material inputs, and validity window.
- Material changes cannot silently inherit prior approval.
- Revoked, expired, missing, invalid, stale, or unverifiable approval is not approval.
- Autonomous permission cannot bypass authorization or a required approval requirement.
- Disablement and human takeover remain available independently of ordinary autonomous execution paths.
- The final governance validation must use current governance state rather than relying solely on an earlier evaluation.
- No Slice 4 component may broaden authority, delegate authority, select/invoke a capability, or cause an external side effect.

## Approval Evaluation

The approval boundary shall represent at least these concepts explicitly:

1. **Requirement** — whether explicit human approval is required for the governed action under the currently applicable governance state.
2. **Approval state** — required, approved, denied, invalid, expired, revoked, or otherwise restricted as applicable.
3. **Approver identity** — the identity associated with an explicit approval when approval is required.
4. **Approved action binding** — the action identity and its material governance-relevant scope.
5. **Purpose** — the purpose for which approval was granted.
6. **Resources / domain** — the approved affected resources and domain.
7. **Conditions** — material conditions that must remain satisfied.
8. **Material inputs** — governance-relevant inputs on which the approval relied.
9. **Validity window** — the period during which the approval is valid.
10. **Revision/version linkage** — the proposal/action revision to which the approval applies.
11. **Revocation/disablement state** — whether approval or human control has subsequently been withdrawn.

Approval evaluation must fail closed for missing, invalid, stale, expired, revoked, or unverifiable approval when approval is required.

## Approval Binding and Material Change

An approval is bound to the governed action it approves. The implementation must prevent approval reuse when a material governance property changes.

At minimum, material change detection covers:

- action/recommendation identity;
- proposal revision;
- domain;
- purpose;
- affected resources/scope;
- intended effect where material;
- material conditions;
- material inputs where approval depends on them;
- applicable authorization/authority basis;
- validity window;
- approval or autonomy state.

A material change requires renewed governance evaluation and renewed approval where the applicable governance state requires it. A prior approval must never silently transfer to a materially different action.

## Bounded Autonomy

Autonomy shall be represented as an explicit governance state, not inferred from model confidence, proposal content, capability availability, or technical reachability.

The autonomy boundary must remain bounded by:

- actor;
- action;
- resources;
- domain;
- purpose;
- limits;
- time;
- conditions;
- revocation/disablement state.

For this slice:

- autonomous permission may only operate within authority already established elsewhere;
- autonomy cannot broaden authorization;
- autonomy cannot satisfy a required human approval by itself;
- disabled autonomy must not permit governed autonomous action;
- loss of required human-control mechanisms must produce an appropriately restricted result rather than silently continuing;
- exact risk tiers and autonomy-level transition rules remain outside this slice unless already established by an approved specification or ADR.

## Human Disablement and Takeover

The governance model must expose a human-controlled path to disable autonomous operation and a human takeover state.

Required semantics:

- disablement is an explicit governance control;
- disablement takes precedence over ordinary autonomous continuation;
- a disabled autonomous path cannot be re-enabled by model or capability output;
- human takeover is represented explicitly rather than inferred from execution behavior;
- human control remains available independently of ordinary autonomous execution.

This slice does not require a production UI, distributed control plane, or specialized emergency-control infrastructure. It does require the domain boundary and tests needed to prove the control cannot be bypassed through the governed path.

## Immediate Pre-Execution Governance Validation

The final governance boundary must validate current state immediately before consequential execution would occur.

The validation must re-check, as applicable:

1. authorization and applicable policy;
2. proposal/action identity and revision;
3. approval requirement and current approval state;
4. autonomy permission and current disablement/revocation state;
5. actor identity;
6. domain and purpose;
7. affected resources and scope containment;
8. material conditions;
9. validity and expiration;
10. material changes since prior governance evaluation;
11. duplicate/replay/concurrency protections required by the governed action;
12. required accountability/governance evidence.

The final check must not rely on an earlier "approved" or "eligible" result when current state can have changed.

The result must explicitly indicate whether consequential execution is **permitted** or **blocked**, together with the governance reason/state required by the architecture. It must not perform the execution itself.

## TOCTOU and Revocation Semantics

Slice 4 must establish the application-level governance boundary needed to detect state changes between earlier governance evaluation and the final check.

Required behavior includes:

- authorization revoked after proposal evaluation → block;
- authorization expires before final validation → block;
- required approval revoked/expired/invalidated before final validation → block;
- approved proposal materially changes before final validation → block or require renewed governance/approval;
- autonomy disabled before final validation → block autonomous continuation;
- governed scope changes → block until revalidated;
- applicable conditions cease to hold → block;
- duplicate/replay of a prior approval for a different action/revision → block.

Where an atomic transaction or capability-level concurrency primitive is required to guarantee protection beyond the domain boundary, that mechanism belongs to the appropriate execution infrastructure and must not be invented in Slice 4. Slice 4 must nevertheless expose the validation contract and failure semantics needed by that later boundary.

## Security / Adversarial Requirements

The implementation must treat approval/autonomy inputs as governed data and must not allow untrusted content to redefine governance.

Required adversarial tests include:

- forged approval cannot produce an approved result;
- replayed approval for a different action cannot pass;
- approval scope substitution cannot pass;
- approval mutation after issuance is detected or rejected;
- proposal text cannot manufacture approval;
- model output cannot satisfy a human approval requirement;
- capability output cannot create approval or autonomy;
- unauthorized scope expansion is rejected;
- disabled autonomy cannot be bypassed through model/proposal content;
- loss of required approval/human-control infrastructure produces restriction rather than permission.

## Scope

1. Implement explicit approval requirement evaluation.
2. Represent approval state and evidence.
3. Bind approval to the governed action and material revision/scope.
4. Implement bounded autonomy state/constraint evaluation.
5. Implement human disablement and takeover state boundaries.
6. Implement immediate pre-execution governance validation.
7. Revalidate authorization, approval/autonomy, scope, purpose, conditions, validity, and material changes.
8. Establish explicit blocked/permitted governance results for the later execution boundary.
9. Preserve traceability to proposal, authorization, approval/autonomy state, and governance evidence.
10. Preserve all validated Slice 1–3 behavior through regression tests.

## Explicit Non-Goals

- Capability implementation or capability invocation.
- External side effects or claims about external reality.
- Execution outcome representation owned by Slice 5.
- Production UI/API hosting.
- Distributed approval infrastructure.
- Cryptographic identity or signature infrastructure unless already required by an approved implementation contract.
- Persistent audit storage owned by later accountability work.
- Full transaction/locking implementation for external systems.
- LLM/provider integration.
- Model selection.
- Autonomous reasoning.
- New risk-tier taxonomy.
- New autonomy-level transition rules beyond approved requirements.
- Any new authority source.

## Required Tests

### Approval

- approval not required → governed action may continue to final validation subject to all other checks;
- approval required but missing → blocked;
- valid approval → approved state is represented explicitly;
- expired approval → blocked;
- revoked approval → blocked;
- invalid/unverifiable approval → blocked;
- approval does not broaden authorization;
- approval is bound to the approved action/revision/scope.

### Material Change / Replay

- proposal revision changes → prior approval cannot silently carry forward;
- scope expands → prior approval cannot carry forward;
- domain changes → prior approval cannot carry forward;
- purpose changes → prior approval cannot carry forward;
- material condition changes → prior approval is re-evaluated;
- approval replay against another action/revision → blocked;
- duplicate/replayed governance request → handled according to the explicit governance result rather than treated as a fresh approval.

### Autonomy

- bounded autonomy permits only actions within its declared constraints;
- autonomy cannot exceed authorization;
- autonomy cannot satisfy a required human approval;
- disabled autonomy blocks autonomous continuation;
- revoked autonomy blocks autonomous continuation;
- loss of required human-control path produces restriction.

### Human Control

- human disablement is effective against autonomous continuation;
- human takeover is represented explicitly;
- model/capability output cannot cancel human disablement;
- human control remains available independently of ordinary autonomous operation.

### Immediate Final Validation

- authorization revoked between evaluation stages → blocked;
- authorization expires before final validation → blocked;
- approval revoked/expired between stages → blocked;
- material proposal change between stages → blocked or renewed governance required;
- scope/purpose/condition change between stages → blocked;
- autonomy disabled between stages → blocked;
- stale governance state is not treated as current;
- final validation result is not execution.

### Security / Adversarial

- forged approval;
- replayed approval;
- scope substitution;
- unauthorized mutation;
- malicious proposal content claiming approval/authorization;
- model output claiming approval;
- capability output claiming approval/autonomy;
- attempted authority broadening;
- governance/approval infrastructure unavailable.

### Regression

- Full Slice 1 regression suite remains green.
- Full Slice 2 regression suite remains green.
- Full Slice 3 regression suite remains green.

## Architectural Traceability

| Slice 4 responsibility | Governing source |
|---|---|
| Explicit approval | ARIA-SPEC-AUTH-003 |
| Approval cannot broaden authority | ARIA-SPEC-AUTH-003; authorization specifications |
| Approval binding/material change | ARIA-SPEC-AUTH-003; ARIA-SPEC-EXEC-001 |
| Bounded autonomy | ARIA-SPEC-AUTH-003 |
| Human disablement/takeover | ARIA-SPEC-AUTH-003 |
| Immediate final governance validation | ARIA-SPEC-EXEC-003 |
| Revocation/expiration/TOCTOU/replay | ARIA-SPEC-AUTH-003; ARIA-SPEC-EXEC-003 |
| Proposal/action linkage | ARIA-SPEC-EXEC-001 |
| No execution in this slice | Implementation Slice Strategy; ARIA-SPEC-EXEC-003 boundary |

## Mandatory Stop Conditions

Stop implementation and request human resolution if:

- an approved specification conflicts with the Constitution or architectural invariants;
- approval semantics require creating a new authority source;
- autonomy cannot be bounded without inventing unauthorized policy;
- implementation requires treating model output as approval or authorization;
- a material governance property is ambiguous and cannot be resolved from approved sources;
- exact risk tiers or autonomy transitions are required but remain an open architectural/ADR question;
- TOCTOU protection would require an unapproved durable architectural decision;
- implementation would cross into capability invocation or external side effects;
- persistent audit or distributed coordination becomes a prerequisite rather than a later integration concern;
- scope expands beyond approval/autonomy/final governance validation;
- required evidence cannot be established without changing an approved requirement.

## Definition of Done

- [ ] Approval requirement evaluation is explicit and bounded.
- [ ] Approval state/evidence is represented and bound to the governed action.
- [ ] Approval cannot broaden authorization.
- [ ] Material changes invalidate or require renewed governance/approval as applicable.
- [ ] Bounded autonomy state/constraints are explicit.
- [ ] Human disablement and takeover boundaries are represented and tested.
- [ ] Immediate pre-execution governance validation rechecks current authority and governance state.
- [ ] Revocation, expiration, replay, scope substitution, and material-change paths fail safely.
- [ ] Final governance result is distinct from execution.
- [ ] No capability is invoked.
- [ ] No external side effect is performed.
- [ ] Slice 1–3 regression tests remain green.
- [ ] Applicable invariant/specification tests pass.
- [ ] Security and adversarial tests pass.
- [ ] Unknown, invalid, unavailable, and restricted states remain explicit.
- [ ] Traceability from implementation → tests → evidence is recorded.
- [ ] CI passes.
- [ ] Pull request clearly identifies implementation scope, tests, evidence, limitations, and non-goals.
- [ ] Final human review remains available before merge.

## Completion Boundary

Slice 4 is complete when A.R.I.A. can take a governed proposal through explicit approval/autonomy evaluation and perform a current-state governance validation immediately before consequential execution, while preserving authority boundaries, human control, material-change semantics, and safe failure behavior.

Completion does **not** authorize capability invocation or external execution. Those responsibilities begin in Slice 5.
