# A.R.I.A. Branch and Change Policy

**Status:** Foundational — Draft

## Purpose

This policy defines how repository branches and changes communicate intent without becoming a substitute for architectural authority.

The purpose is to prevent implementation convenience, branch structure, or local decisions from silently changing A.R.I.A.'s architecture or governance model.

## Governing Principle

> **Branch names communicate intent; they do not grant authority.**

A change is governed by its architectural impact and review requirements, not by the branch name chosen for it.

## Branch Model

```text
main
  │
  ├── architecture/<purpose>
  ├── spec/<purpose>
  ├── feature/<purpose>
  ├── fix/<purpose>
  └── experiment/<purpose>
```

### `main`

The approved project baseline. Changes reaching `main` must have completed the review appropriate to their scope.

### `architecture/*`

Changes to conceptual or implementation architecture, architectural boundaries, invariants, or other durable structural decisions. These require explicit architecture review and an ADR when appropriate.

### `spec/*`

Specification work derived from approved architecture. Specifications may clarify or formalize architectural behavior but must not silently redefine higher-level architecture.

### `feature/*`

Implementation of approved architecture and specifications. Feature branches do not have authority to redefine architecture.

### `fix/*`

Corrections to existing behavior. If a fix requires changing an invariant, architectural boundary, or approved specification, the change must be escalated rather than hidden inside the fix.

### `experiment/*`

Exploration and learning. Experiments may inform future decisions but do not establish architectural authority or approved behavior.

## Change Hierarchy

Changes should respect this hierarchy:

```text
Constitution
    ↓
Invariants
    ↓
Conceptual Architecture
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

A lower layer may implement a higher layer, but may not silently redefine it.

If implementation appears to require violating a higher-level rule, stop and revisit the higher-level decision.

## Pull Request Expectations

A pull request should make its intended scope and architectural impact clear. Where applicable, the PR should identify:

- the capability being changed;
- the epic and story being implemented;
- affected specifications;
- affected invariants;
- tests and evidence;
- any ADRs required by the change;
- security, authority, knowledge, trust, execution, or human-control implications.

Architectural changes should not be disguised as implementation changes merely because the resulting code is small.

## Review and Approval

The required reviewer(s) depend on the level of change. At minimum:

- implementation changes require normal code review;
- specification changes require specification/architecture review;
- architectural changes require explicit architecture review;
- constitutional or invariant changes require extraordinary governance and explicit approval.

Passing tests do not substitute for architectural approval.

## Prohibited Authority Escalation

No branch, contributor, component, model, provider, capability, technical path, or implementation convenience may create authority merely because it can technically perform an action.

In particular:

- reasoning cannot create authority;
- knowledge cannot create authority;
- capability cannot create authority;
- technical access cannot create authorization;
- approval cannot become unbounded authorization;
- implementation cannot redefine governance by convention.

## Traceability

Durable decisions should preserve a traceable path from architectural intent to implementation evidence:

```text
Decision
  ↓
Architecture / Invariant
  ↓
Specification
  ↓
Capability / Epic / Story
  ↓
Implementation
  ↓
Test
  ↓
Evidence
```

This traceability is part of A.R.I.A.'s defense against architectural drift, security debt, and knowledge debt.

## Policy Evolution

This policy itself is subordinate to the A.R.I.A. Constitution and invariants. Changes to this policy that materially affect architectural authority, security, or governance must follow the same governed change process they establish.
