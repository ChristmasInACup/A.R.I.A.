# A.R.I.A.

**Governed intelligence for connecting people, information, reasoning capabilities, and real-world action.**

## What is Aria?

Aria is a governed intelligence layer that safely connects people, information, reasoning capabilities, and external actions while separating identity, authority, knowledge, reasoning, and execution. It maintains explicit trust boundaries, bounded autonomy, provenance, uncertainty, and accountability throughout the entire lifecycle.

> **Aria governs intelligence rather than being governed by intelligence.**

## Architectural Principle

> **Intelligence may recommend. Authority decides. Governance constrains. Execution acts. External reality determines what happened. Accountability preserves the evidence.**

## Core Architecture

Aria is organized around six Core capabilities:

1. **Identity & Domain** — who or what is involved, and where the interaction belongs.
2. **Authority & Governance** — who may do what, under which conditions.
3. **Knowledge Governance** — what information may exist, where it came from, and how it may be used.
4. **Context & Reasoning Coordination** — what authorized information should be used to solve a problem.
5. **Capability & Execution Governance** — how authorized decisions become real-world actions.
6. **Accountability & Assurance** — how Aria determines what happened, why, and whether it behaved correctly.

The architecture is expressed through two primary planes:

- **Knowledge Plane** — information, memory, provenance, trust, and lifecycle.
- **Authority Plane** — identity, authorization, policy, delegation, autonomy, approval, and governance.

Context is the controlled bridge between them. Reasoning operates on authorized context. Execution crosses the boundary into external reality.

## Documentation

- [Constitution](docs/constitution/ARIA_Constitution.md)
- [Architectural Invariants](docs/constitution/ARIA_Invariants.md)
- [Conceptual Architecture v1.0](docs/architecture/ARIA_Conceptual_Architecture_v1.0.md)
- [Decision Records](docs/decisions/README.md)
- [Security Architecture](SECURITY.md)
- [Contributing](CONTRIBUTING.md)

## Architectural Hierarchy

```text
CONSTITUTION
    ↓
CONCEPTUAL ARCHITECTURE
    ↓
ARCHITECTURE SPECS
    ↓
IMPLEMENTATION ARCHITECTURE
    ↓
CODE
    ↓
TESTS
```

Architecture decisions and security constraints apply across these layers.

> **Code never gets to redefine the architecture simply because the code was easier to write that way.**

## Project Status

| Area | Status |
|---|---|
| Constitution | FROZEN |
| Conceptual Architecture | FROZEN — v1.0 |
| Security Model | FROZEN — v1.0 |
| Governance Model | FROZEN — v1.0 |
| Implementation | BEGINNING |

## Development Philosophy

Aria is designed to resist:

- tech debt
- knowledge debt
- security debt
- architectural drift
- provider lock-in
- implicit authority escalation
- uncontrolled autonomy

The implementation must preserve the conceptual boundaries rather than merely approximate them.
