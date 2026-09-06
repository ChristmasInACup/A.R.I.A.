# Contributing to A.R.I.A.

A.R.I.A. is architecture-first. Contributions must preserve the separation between intelligence, authority, governance, execution, and accountability.

## Before changing code

Determine whether the change affects:

- authority
- security
- domains
- knowledge or memory
- execution
- trust
- audit
- autonomy
- providers or model governance
- constitutional or architectural boundaries

If it does, document the design impact before implementation. Create an ADR when the decision has durable architectural consequences.

## Branch and Change Policy

Branch names communicate intent; they do not grant architectural authority.

The repository should use branches according to the kind of change being made:

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

When a change crosses its branch's intended responsibility, stop and move the decision to the appropriate higher-level artifact and review process.

## Development Flow

```text
Issue
  ↓
Design
  ↓
ADR if architectural
  ↓
Specification / Acceptance Criteria
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
- unknown execution results remain unknown.

## Change Discipline

Prefer small, reviewable changes. Preserve provenance for architectural decisions. Avoid introducing provider-specific, implementation-specific, or convenience-driven assumptions into foundational architecture documents.
