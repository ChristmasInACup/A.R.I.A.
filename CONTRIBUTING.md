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

## Development Flow

```text
Issue
  ↓
Design
  ↓
ADR if architectural
  ↓
Implementation
  ↓
Tests
  ↓
Review
  ↓
Merge
```

## Architectural Rule

> **Code never gets to redefine the architecture simply because the code was easier to write that way.**

If implementation appears to require violating a constitutional principle or invariant, stop and explicitly revisit the architecture.

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
