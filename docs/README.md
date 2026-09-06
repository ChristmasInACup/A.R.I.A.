# A.R.I.A. Documentation

This directory contains the durable architectural and engineering knowledge of A.R.I.A.

The documentation system is intentionally layered. Each layer has a distinct purpose and authority. Lower layers may implement or clarify higher layers, but they may not silently redefine them.

## Documentation Hierarchy

```text
Constitution
    ↓
Invariants
    ↓
Conceptual Architecture
    ↓
Implementation Architecture
    ↓
Architecture Specifications
    ↓
Capabilities
    ↓
Epics
    ↓
Stories
    ↓
Tasks
    ↓
Implementation
    ↓
Tests
    ↓
Evidence
```

## Documentation Areas

### Constitution

Foundational rules that define what A.R.I.A. must never violate.

- [A.R.I.A. Constitution](constitution/ARIA_Constitution.md)
- [Architectural Invariants](constitution/ARIA_Invariants.md)

**Status:** Frozen — v1.0

### Conceptual Architecture

Defines the fundamental organization and boundaries of A.R.I.A. without committing to implementation technology.

- [Conceptual Architecture v1.0](architecture/ARIA_Conceptual_Architecture_v1.0.md)
- [Architecture Documentation](architecture/README.md)

**Status:** Frozen — v1.0

### Implementation Architecture

Defines the logical structure required to realize the approved conceptual architecture while remaining technology-agnostic.

- [Implementation Charter](implementation/Implementation_Charter.md)
- [Implementation Architecture](implementation/Implementation_Architecture.md)
- [System Components](implementation/System_Components.md)
- [Data Architecture](implementation/Data_Architecture.md)
- [Interface Architecture](implementation/Interface_Architecture.md)
- [Deployment Architecture](implementation/Deployment_Architecture.md)
- [Implementation Documentation](implementation/README.md)

**Status:** Established — Draft / implementation-phase baseline

### Architecture Specifications

Translate architectural intent into precise, testable boundary contracts.

- [Specification Index](specs/README.md)

**Status:** Draft specifications — approval pending

### Decision Records

Record durable decisions, alternatives, consequences, and architectural rationale.

- [Decision Records](decisions/README.md)

**Status:** Active

### Governance

Defines repository-level change and review discipline that supports architectural integrity.

- [Branch and Change Policy](governance/Branch_and_Change_Policy.md)

**Status:** Foundational — Draft

## Root-Level Project Documents

These documents govern repository participation and provide project-level context.

- [Project README](../README.md)
- [Security](../SECURITY.md)
- [Contributing](../CONTRIBUTING.md)
- [Changelog](../CHANGELOG.md)

## How to Use the Documentation

### When designing

Start with the Constitution and invariants, then read the conceptual architecture. Use implementation architecture only after the conceptual boundary is understood.

### When specifying behavior

Start from the relevant architectural boundary and create or update an architecture specification. Specifications must remain subordinate to higher-level architecture.

### When planning implementation

Use approved specifications to derive capabilities, epics, and stories. Do not use implementation work to discover or silently redefine foundational architecture.

### When making a durable decision

Record the decision in an ADR when it has lasting architectural, security, authority, governance, or implementation consequences.

### When implementing

Follow the Implementation Charter and preserve traceability from architecture through specifications, stories, tests, and evidence.

## Status Vocabulary

Use these terms consistently:

- **Draft** — work exists but is not yet approved as a baseline.
- **Under Review** — explicitly being reviewed for approval.
- **Approved** — accepted as the current authoritative baseline within its scope.
- **Frozen** — approved and protected from ordinary change; changes require the defined governance process.
- **Superseded** — replaced by a newer approved version.
- **Retired** — no longer active and retained for historical traceability.

## Governing Rule

> **Documentation preserves architectural knowledge; governance determines architectural authority.**

If documentation conflicts with a higher-authority artifact, the conflict must be resolved explicitly. The easier implementation is never the authority by default.
