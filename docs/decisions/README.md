# Decision Records

Architecture decisions are recorded here when a decision has durable consequences or prevents future knowledge debt.

## When to write an ADR

Create an ADR when changing or establishing:

- authority boundaries
- security boundaries
- domain isolation
- knowledge ownership or governance
- execution semantics
- provider/model governance
- autonomy or approval rules
- major architectural interfaces
- implementation choices with long-term architectural consequences

## Decision hierarchy

```text
Constitution
    ↓
Conceptual Architecture
    ↓
Architecture Specs
    ↓
Implementation Architecture
    ↓
Code
```

An ADR explains **why** a decision was made. It must not silently override the Constitution or conceptual architecture.

## Suggested ADR format

```md
# ADR-NNNN — Title

## Status

Proposed | Accepted | Superseded | Rejected

## Context

What problem or decision requires attention?

## Decision

What are we choosing?

## Alternatives Considered

What meaningful alternatives were rejected and why?

## Consequences

What becomes easier, harder, safer, riskier, or more constrained?

## Architectural Impact

Which boundaries, invariants, or documents are affected?

## Security Impact

What changes in trust, authority, information flow, or execution risk?
```

The first architectural decisions should preserve the principles already established in the Constitution and conceptual architecture.
