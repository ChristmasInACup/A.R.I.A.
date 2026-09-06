# A.R.I.A.

**Governed intelligence for connecting people, information, reasoning capabilities, and real-world action.**

## What is A.R.I.A.?

A.R.I.A. is a governed intelligence layer that safely connects people, information, reasoning capabilities, and external actions while separating identity, authority, knowledge, reasoning, and execution. It maintains explicit trust boundaries, bounded autonomy, provenance, uncertainty, and accountability throughout the entire lifecycle.

> **A.R.I.A. governs intelligence rather than being governed by intelligence.**

## Architectural Principle

> **Intelligence may recommend. Authority decides. Governance constrains. Execution acts. External reality determines what happened. Accountability preserves the evidence.**

## Core Architecture

A.R.I.A. is organized around six core capabilities:

1. **Identity & Domain** — who or what is involved, and where the interaction belongs.
2. **Authority & Governance** — who may do what, under which conditions.
3. **Knowledge Governance** — what information may exist, where it came from, and how it may be used.
4. **Context & Reasoning Coordination** — what authorized information should be used to solve a problem.
5. **Capability & Execution Governance** — how authorized decisions become real-world actions.
6. **Accountability & Assurance** — how A.R.I.A. determines what happened, why, and whether it behaved correctly.

The architecture is expressed through two primary planes:

- **Knowledge Plane** — information, memory, provenance, trust, and lifecycle.
- **Authority Plane** — identity, authorization, policy, delegation, autonomy, approval, and governance.

Context is the controlled bridge between them. Reasoning operates on authorized context. Execution crosses the boundary into external reality.

## Documentation

The complete documentation map is available in [Documentation](docs/README.md).

### Foundational

- [Constitution](docs/constitution/ARIA_Constitution.md)
- [Architectural Invariants](docs/constitution/ARIA_Invariants.md)
- [Conceptual Architecture v1.0](docs/architecture/ARIA_Conceptual_Architecture_v1.0.md)

### Implementation

- [Implementation Charter](docs/implementation/Implementation_Charter.md)
- [Implementation Architecture](docs/implementation/Implementation_Architecture.md)
- [System Components](docs/implementation/System_Components.md)
- [Data Architecture](docs/implementation/Data_Architecture.md)
- [Interface Architecture](docs/implementation/Interface_Architecture.md)
- [Deployment Architecture](docs/implementation/Deployment_Architecture.md)

### Specifications and Governance

- [Architecture Specifications](docs/specs/README.md)
- [Decision Records](docs/decisions/README.md)
- [Branch and Change Policy](docs/governance/Branch_and_Change_Policy.md)

### Repository Guidance

- [Security](SECURITY.md)
- [Contributing](CONTRIBUTING.md)
- [Changelog](CHANGELOG.md)

## Architectural Hierarchy

```text
CONSTITUTION
    ↓
INVARIANTS
    ↓
CONCEPTUAL ARCHITECTURE
    ↓
IMPLEMENTATION ARCHITECTURE
    ↓
ARCHITECTURE SPECIFICATIONS
    ↓
CAPABILITIES
    ↓
EPICS
    ↓
STORIES
    ↓
TASKS
    ↓
IMPLEMENTATION
    ↓
TESTS
    ↓
EVIDENCE
```

Each layer has a distinct purpose. Lower layers may implement or clarify higher layers, but may not silently redefine them.

> **Code never gets to redefine the architecture simply because the code was easier to write that way.**

## Project Status

| Area | Status |
|---|---|
| Constitution | FROZEN — v1.0 |
| Architectural Invariants | FROZEN — v1.0 |
| Conceptual Architecture | FROZEN — v1.0 |
| Security Architecture | FROZEN — v1.0 |
| Governance Model | FROZEN — v1.0 |
| Implementation Architecture | ESTABLISHED — DRAFT / IMPLEMENTATION PHASE |
| Architecture Specifications | DRAFT — APPROVAL PENDING |
| Capabilities / Epics / Stories | NOT YET BASELINED |
| Production Implementation | NOT YET STARTED |

## Development Philosophy

A.R.I.A. is designed to resist:

- tech debt
- knowledge debt
- security debt
- architectural drift
- provider lock-in
- implicit authority escalation
- uncontrolled autonomy

The implementation must preserve the conceptual boundaries rather than merely approximate them.
