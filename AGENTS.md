# A.R.I.A. Codex / AI Engineering Instructions

## Authority

A.R.I.A. implementation is governed by this hierarchy:

Constitution → Invariants → Conceptual Architecture → Specifications → Capabilities → Epics → Stories → Tasks → Implementation → Tests → Evidence

Implementation may realize approved architecture; it may not redefine it.

Read the relevant governing documents before changing behavior:

- `docs/constitution/ARIA_Constitution.md`
- `docs/constitution/ARIA_Invariants.md`
- `docs/architecture/ARIA_Conceptual_Architecture_v1.0.md`
- `docs/implementation/Implementation_Architecture.md`
- `docs/implementation/Implementation_Charter.md`
- `docs/implementation/Implementation_Readiness.md`
- `docs/governance/Architecture_Traceability_Matrix.md`
- `docs/decisions/ADR-0001-initial-implementation-technology.md`

## Current implementation target

The initial reference slice is deliberately bounded:

`Governed Request → Identity → Domain → Authorization → Minimum-Necessary Context → Governed Proposal`

Do not add AI providers, LLM integration, execution, external side effects, databases, production authentication, deployment infrastructure, UI, autonomous operation, or distributed architecture unless a separately authorized task requires them.

## Non-negotiable boundaries

- Identity is not authority.
- Knowledge is not authority.
- Context is not authority.
- Reasoning is not authority.
- Proposal is not authorization.
- Capability is not authority.
- Provider/model output is not authority.
- Technical reachability is not authorization.
- Cross-domain access requires authorization.
- Context must be minimum necessary.
- Unknown outcomes must remain unknown.
- Failure must not increase authority.

## Coding rules

- C# is the initial implementation language; xUnit is the initial test framework.
- Keep the reference implementation single-language unless a concrete requirement justifies another boundary.
- Prefer small, focused, explicit components.
- Prefer immutable domain values where practical.
- Keep authorization decisions separate from identity resolution, context assembly, and proposal construction.
- Do not pass raw collections or ambient/global state when a narrower domain type can express the boundary.
- Do not let proposal objects expose authority-changing behavior.
- Default to deny when authorization is absent or indeterminate.
- Do not use exceptions as a substitute for normal authorization outcomes.
- Do not introduce frameworks or infrastructure merely for convenience.

## Testing rules

Every security or authority boundary introduced by implementation should have a corresponding test. Include both permitted and denied paths where applicable.

Important invariant tests should demonstrate that:

1. identity alone does not authorize;
2. authorization is evaluated explicitly;
3. unauthorized information cannot enter authorized context;
4. a proposal cannot grant itself authority;
5. changing authorization conditions changes the result.

## Agent behavior

Codex may inspect, implement, test, document, and prepare a PR for explicitly authorized work. It must not:

- change constitutional or architectural authority silently;
- weaken security or tests to make code pass;
- invent requirements;
- expand story scope;
- treat model output as authoritative;
- bypass required review or merge controls.

If implementation conflicts with a higher-level artifact, stop at the conflict and report it rather than redesigning the architecture unilaterally.

## Change discipline

Use a branch and pull request for implementation work. Keep changes small and reviewable. Preserve traceability in code/test documentation and PR descriptions. Run the complete test suite before claiming a change is validated.
