# ADR-0001 — Initial Implementation Technology

## Status

Accepted

## Date

2026-09-08

## Context

A.R.I.A. has completed its architectural and planning baseline and is entering implementation planning. The implementation architecture intentionally deferred programming-language and framework selection so that technology would not redefine the approved architecture.

The first implementation target is a bounded reference slice intended to prove the architecture rather than implement the complete A.R.I.A. vision:

```text
Governed Request
      ↓
   Identity
      ↓
    Domain
      ↓
 Authorization
      ↓
Minimum-Necessary Context
      ↓
   Proposal
```

The technology decision therefore needs to optimize for explicit authority boundaries, domain clarity, testability, maintainability, provider independence, and the ability to make unsafe behavior difficult without introducing unnecessary architectural complexity.

Python and C# were evaluated as primary implementation choices. A deliberately polyglot architecture was also evaluated, including the possibility of using C# for governance/domain responsibilities and Python for reasoning/AI responsibilities.

## Decision

**C# is selected as the initial implementation language for A.R.I.A.'s reference implementation, with xUnit as the initial test framework.**

This is a scoped implementation decision, not a commitment that all future A.R.I.A. components must use C#.

The initial implementation should remain a single-language reference implementation unless a concrete architectural or responsibility-level need justifies introducing another language or a cross-language boundary.

**Python remains an approved candidate for future responsibilities where its ecosystem provides a material advantage**, particularly AI/reasoning, experimentation, evaluation, or tooling workloads. Introducing Python is not authorized merely because a responsibility involves AI; the responsibility and boundary must justify the choice.

No Python/C# process, service, serialization, or deployment boundary is introduced by this ADR. Such a boundary is a separate architectural/implementation decision and must be evaluated when there is a demonstrated need.

## Alternatives Considered

### Python-first

Python is an excellent fit for rapid experimentation, AI/LLM ecosystems, context processing, and automated testing with pytest. It can represent A.R.I.A.'s architectural boundaries correctly when supported by explicit types, conventions, and tests.

It was not selected for the initial reference implementation because C# provides stronger compile-time assistance for expressing and preserving the distinctions central to A.R.I.A.'s first slice, including identity, authorization, authorized context, proposal, and other domain concepts. The additional language structure is considered beneficial for making the architecture visible and making accidental boundary violations harder.

### C#-first

C# provides strong domain modeling, explicit interfaces and contracts, immutable record types, compiler-assisted refactoring, and clear separation between domain concepts. These characteristics align particularly well with A.R.I.A.'s emphasis on authority boundaries, explicit behavior, modularity, testability, and change isolation.

The primary cost is greater ceremony than Python. For the first reference slice, that cost is acceptable because the purpose is to prove and communicate architectural boundaries rather than maximize experimentation speed.

### Polyglot from the beginning

A polyglot design could eventually place governance/domain responsibilities in C# and reasoning/AI responsibilities in Python. This remains a viable future direction.

It is rejected for the initial slice because it would introduce cross-language contracts, serialization, process/deployment boundaries, failure modes, version compatibility, and additional testing obligations before those concerns are necessary to prove the architecture. That complexity would not materially improve the first architectural proof.

## Consequences

### Positive

- The initial reference implementation has a strongly typed foundation for domain and authority concepts.
- Architectural boundaries can be made visible through explicit domain types and interfaces.
- The compiler can prevent some classes of accidental misuse before runtime tests execute.
- xUnit supports layered invariant, specification, and story tests required by the implementation charter.
- The implementation remains independent of any AI provider or model vendor.
- Future Python use remains possible without making Python a foundational dependency.
- A future cross-language boundary can be introduced because it is needed, rather than because the architecture was prematurely split.

### Negative / Trade-offs

- The initial implementation will have more ceremony than an equivalent Python prototype.
- AI/LLM experimentation may eventually be more convenient in Python, which could create a later language boundary if that advantage becomes material.
- C# does not itself enforce A.R.I.A.'s security or authority model; architecture, explicit design, validation, tests, and governance remain the actual controls.
- Selecting C# does not justify introducing an application framework, database, cloud platform, provider, or deployment topology before those choices are independently required.

## Architectural Impact

This ADR implements the technology-selection boundary established by the Implementation Architecture, Implementation Charter, and Implementation Readiness baseline. It does not change A.R.I.A.'s conceptual architecture or constitutional invariants.

The following separations remain mandatory regardless of implementation language:

- identity ≠ authority;
- knowledge ≠ authority;
- context ≠ authority;
- reasoning ≠ authority;
- proposal ≠ authorization;
- capability ≠ authority;
- provider/model ≠ authority.

Implementation may realize these boundaries but may not redefine them.

## Security / Trust Impact

No new trust boundary is introduced by selecting C#.

The decision is intended to make domain and authority boundaries more explicit in the reference implementation, but the language itself is not a security boundary and must not be treated as one.

Provider/model output remains untrusted reasoning output until it passes the applicable governance and validation boundaries.

Introducing another language, process, service, or external provider later may create additional trust or failure boundaries and must therefore be evaluated separately.

## Required Follow-Up

1. Create the initial C# reference-implementation structure only after the first bounded implementation slice and its exact Task/Story lineage are confirmed.
2. Establish the xUnit test structure alongside implementation so architectural invariants are executable from the beginning.
3. Keep the first slice free of AI-provider integration, execution, external side effects, production deployment infrastructure, and other concerns not required to prove the slice.
4. Record any later durable technology decision in an ADR when it has architectural, security, trust, governance, or long-term implementation consequences.
5. Evaluate Python separately when a concrete responsibility demonstrates a material benefit that justifies its introduction.
6. If implementation reveals that this decision conflicts with a higher-level architectural artifact, stop implementation at that boundary and resolve the conflict through the appropriate governance process.
