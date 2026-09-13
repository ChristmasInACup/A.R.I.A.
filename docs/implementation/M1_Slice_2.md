# M1 Slice 2 — Governed Knowledge → Authorized Context

**Status:** Authorized — M1 Slice 2
**Milestone:** M1 / M2 boundary
**Purpose:** Prove that A.R.I.A. can assemble minimum-necessary, authorized context from governed information without allowing knowledge to create authority.

## Story

**As A.R.I.A.,** given an authorized request and governed information, I can assemble only the information necessary for that request while preserving governance metadata and without allowing information or context to create authority.

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
 Knowledge Selection
      ↓
 Knowledge Governance
      ↓
 Minimum-Necessary Context
      ↓
   Proposal
```

Reasoning providers, LLMs, persistence, and execution remain outside this slice.

## Scope

1. Represent governed information with the metadata required by applicable knowledge specifications.
2. Establish an explicit knowledge-governance boundary for use of information.
3. Assemble minimum-necessary context only after authorization succeeds.
4. Enforce domain, purpose, scope, lifecycle, and other applicable constraints.
5. Preserve provenance through context assembly.
6. Preserve uncertainty and unknown states through context assembly.
7. Prevent cross-domain information from entering context without authorization.
8. Prove that knowledge and context cannot create authority.
9. Preserve all M1 Slice 1 behavior through regression tests.

## Acceptance Criteria

- [ ] Governed information can be represented with required governance metadata.
- [ ] Authorized information can enter context when domain, purpose, scope, lifecycle, and authority requirements are satisfied.
- [ ] Unauthorized information cannot enter context.
- [ ] Wrong-domain information cannot enter context.
- [ ] Wrong-purpose information cannot enter context.
- [ ] Invalid or expired information is excluded where required by the applicable specification.
- [ ] Context contains only information necessary for the task.
- [ ] Provenance required by the applicable specification survives context assembly.
- [ ] Uncertainty and unknown states survive context assembly without being converted into facts.
- [ ] Context cannot be produced when authorization has failed.
- [ ] Knowledge cannot grant or broaden authority.
- [ ] Adding unauthorized or unrelated knowledge cannot change an authorization result.
- [ ] Aggregating individually available information cannot silently broaden authorization.
- [ ] Cross-domain aggregation fails closed when authorization is absent.
- [ ] All existing M1 Slice 1 tests continue to pass.

## Required Tests

### Permitted

- Authorized information is included.
- Multiple authorized items can be assembled when each is permitted and necessary.
- Required provenance is retained.
- Uncertainty is retained.

### Denied

- Unauthorized information is excluded.
- Wrong-domain information is excluded.
- Wrong-purpose information is excluded.
- Expired or otherwise invalid information is excluded where required.
- Context assembly does not occur after authorization failure.

### Security and Invariants

- Knowledge does not create authority.
- Adding unauthorized knowledge cannot change authorization.
- Information aggregation cannot silently broaden authority.
- Cross-domain information movement fails closed without authorization.

### Regression

- Full M1 Slice 1 regression suite remains green.

## Architectural References

- A.R.I.A. Constitution
- A.R.I.A. Architectural Invariants
- Implementation Architecture — Knowledge Governance, Context Assembly, Cross-Cutting Security Model, and Architectural Invariants as Tests
- Applicable knowledge and reasoning specifications
- Applicable identity and authorization specifications
- Implementation Charter
- Autonomous Implementation Protocol
- Implementation Backlog — EPIC-06 and supporting M1 context requirements

## Security Requirements

- Knowledge availability must never imply authority.
- Technical accessibility must never substitute for authorization.
- Cross-domain movement must be explicit and governed.
- Context must not become an authority-bearing object merely by containing authorized information.
- Failure or ambiguity in a required governance check must fail closed for protected information.
- Provenance and uncertainty must not be discarded merely to simplify context.
- The implementation must not introduce a provider, model, storage system, or framework as a security boundary.

## Dependencies

- M1 Slice 1 identity and authorization behavior.
- Approved knowledge specifications.
- Existing proposal/context path established by Slice 1.

## Explicit Non-Goals

- LLM or reasoning-provider integration.
- Provider/model selection.
- Embeddings or vector databases.
- Persistent memory implementation.
- Production database/storage infrastructure.
- UI or API hosting.
- External execution.
- Approval/autonomy systems.
- Distributed knowledge services.
- Complex information-sharing workflows.

## Mandatory Stop Conditions

Stop implementation and request human resolution if:

- an applicable specification conflicts with the Constitution or architectural invariants;
- the required knowledge semantics are materially ambiguous;
- implementation requires a new durable architectural decision not already authorized;
- a security or authority boundary would need to be weakened;
- the scope would materially expand beyond this slice;
- required evidence cannot be established without changing an approved requirement;
- an implementation choice would make knowledge an implicit authority source.

## Definition of Done

- [ ] Authorized behavior is implemented.
- [ ] Acceptance criteria are satisfied.
- [ ] Applicable invariants are preserved and tested.
- [ ] Story, specification, and regression tests pass.
- [ ] Security implications are validated.
- [ ] Unknown and failure behavior is explicit where applicable.
- [ ] Durable knowledge changes are documented.
- [ ] Required architectural decisions are recorded through ADRs.
- [ ] Implementation remains within slice scope.
- [ ] Code is readable, focused, and modular.
- [ ] Relevant evidence is recorded.
- [ ] CI passes.
- [ ] Pull request clearly identifies scope, tests, evidence, limitations, and non-goals.
- [ ] Final human review is available at the applicable governance boundary.

## Completion Boundary

This slice is complete only when the permitted and denied knowledge/context paths, invariant tests, specification-aligned behavior, regression suite, security validation, CI, and evidence are all satisfied. Completion does not authorize later reasoning-provider or execution work.
