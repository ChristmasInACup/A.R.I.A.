# Decision Records

Architecture Decision Records (ADRs) capture durable decisions that would otherwise become lost knowledge or be rediscovered later.

## Purpose

Use an ADR when a decision has meaningful architectural, security, authority, governance, trust, execution, or long-term implementation consequences.

An ADR records **why** a decision was made. It does not become a new source of authority and cannot silently override a higher-level artifact.

## When to Write an ADR

Create an ADR when establishing or changing:

- authority boundaries;
- security or trust boundaries;
- domain isolation;
- knowledge ownership or governance;
- execution semantics;
- provider/model governance;
- autonomy or approval rules;
- major architectural interfaces;
- recovery or containment behavior;
- implementation choices with durable architectural consequences;
- migration or state-transition rules that affect governance;
- other decisions whose rationale should remain discoverable after the original discussion is gone.

If a question changes the Constitution or a foundational invariant, treat it as an extraordinary architectural change rather than an ordinary implementation ADR.

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
Implementation
    ↓
Tests
    ↓
Evidence
```

ADRs record decisions **within or about this hierarchy**. They preserve rationale and consequences; they do not create a parallel hierarchy.

## Authority Rules

An ADR:

- must identify the higher-level artifacts it affects;
- must not silently redefine the Constitution;
- must not weaken an invariant without the required extraordinary governance;
- must identify when an associated specification, architecture document, or implementation artifact must change;
- should preserve the alternatives and reasoning that led to the decision.

If an ADR conflicts with a higher-authority artifact, the conflict must be resolved explicitly before implementation proceeds.

## Suggested ADR Format

```md
# ADR-NNNN — Title

## Status

Proposed | Accepted | Superseded | Rejected

## Date

YYYY-MM-DD

## Context

What problem or decision requires attention?

## Decision

What are we choosing?

## Alternatives Considered

What meaningful alternatives were considered and why were they rejected?

## Consequences

What becomes easier, harder, safer, riskier, or more constrained?

## Architectural Impact

Which boundaries, invariants, specifications, or documents are affected?

## Security / Trust Impact

What changes in authority, trust, information flow, isolation, or execution risk?

## Required Follow-Up

Which specifications, capabilities, implementation documents, tests, or evidence must change?
```

## Decision Lifecycle

```text
Question / Need
      ↓
Context and alternatives
      ↓
Decision
      ↓
ADR
      ↓
Update affected architecture/specification
      ↓
Implementation
      ↓
Tests / Evidence
```

An ADR should be created early enough that implementation does not become the de facto decision mechanism.

## Current State

No individual ADRs are required merely to restate the already-approved foundational architecture. The existing Constitution, invariants, conceptual architecture, implementation architecture, and specification framework remain the primary sources of architectural authority.
