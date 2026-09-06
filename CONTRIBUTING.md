# Contributing to A.R.I.A.

A.R.I.A. is architecture-first. Contributions must preserve the separation between intelligence, authority, governance, execution, and accountability.

## Before Changing the Repository

First determine the highest architectural layer affected by the change:

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

If a change affects authority, security, domains, knowledge or memory, execution, trust, audit, autonomy, providers/model governance, human control, or constitutional/architectural boundaries, document the design impact before implementation. Create an ADR when the decision has durable consequences.

## Branch and Change Policy

The detailed repository policy is defined in [Branch and Change Policy](docs/governance/Branch_and_Change_Policy.md).

Branch names communicate intent; they do not grant architectural authority.

```text
main
  │
  ├── architecture/<purpose>
  ├── spec/<purpose>
  ├── feature/<purpose>
  ├── fix/<purpose>
  └── experiment/<purpose>
```

- `main` — approved project baseline.
- `architecture/*` — architectural changes requiring explicit architecture review.
- `spec/*` — specification work within approved architecture.
- `feature/*` — implementation of approved architecture and specifications.
- `fix/*` — corrections that do not redefine architecture; architectural changes must be escalated.
- `experiment/*` — exploration only; experiments do not establish architectural authority.

No branch type, implementation convenience, technical reachability, or passing test may silently create or redefine authority.

## Development Flow

```text
Need / Issue
    ↓
Design
    ↓
ADR if durable architectural decision
    ↓
Specification
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
Tests
    ↓
Evidence
    ↓
Review
    ↓
Merge
```

Not every change requires every layer. The highest affected layer determines the appropriate review and documentation requirements.

## Architectural Rule

> **Code never gets to redefine the architecture simply because the code was easier to write that way.**

If implementation appears to require violating a constitutional principle, invariant, specification, or architectural boundary, stop and explicitly revisit the higher-level decision.

## Testing Expectations

As implementation begins, architectural invariants should become executable tests where practical. Examples include:

- reasoning cannot authorize;
- modules cannot escalate authority;
- cross-domain access requires authorization;
- providers cannot modify policy;
- memory cannot grant authority;
- approval is bound to execution;
- unknown execution results remain unknown;
- loss of trust reduces capability rather than expanding authority.

## Documentation Expectations

Update documentation when durable knowledge changes.

Use:

- architecture documents for stable structural intent;
- specifications for testable behavioral and boundary guarantees;
- ADRs for durable decisions and rationale;
- README/index documents for navigation and current status;
- the changelog for meaningful project-level evolution.

Do not duplicate a rule across documents merely for convenience. When duplication is necessary for discoverability, identify the higher-authority source and keep the repeated statement consistent.

## AI Engineering

AI coding agents are engineering assistants operating under A.R.I.A.'s governance model. They may accelerate approved work, but they do not have authority to redefine requirements, architecture, governance, or security boundaries.

If an agent discovers a conflict between implementation work and a higher-level artifact, it must stop at the conflict and request resolution rather than silently choosing an implementation-driven interpretation.

## Pull Request Expectations

A pull request should make its scope and architectural impact clear. Where applicable, identify:

- the capability being changed;
- the epic and story authorizing the work;
- affected specifications;
- affected invariants;
- tests and evidence;
- relevant ADRs;
- security, authority, knowledge, trust, execution, or human-control implications;
- explicit non-goals.

Passing tests do not substitute for architectural approval.

## Change Discipline

Prefer small, reviewable changes. Preserve provenance for architectural decisions. Avoid provider-specific, implementation-specific, or convenience-driven assumptions in foundational architecture documents.

When a lower-level change appears to require changing a higher-level rule, escalate the decision rather than allowing architectural drift to enter through implementation.
