# A.R.I.A. Implementation Slice Strategy

**Status:** Baseline for implementation planning  
**Authority:** Implementation Charter, Autonomous Implementation Protocol, Implementation Backlog, approved architecture and specifications

## Purpose

A.R.I.A. should be implemented as a sequence of integrated vertical slices rather than as one large build or as isolated layers completed independently.

Each slice should prove a meaningful end-to-end architectural boundary, produce executable tests and evidence, and leave the repository in a coherent state. A slice may contain multiple related Stories and Tasks when they are required to prove the boundary together.

The agent may work continuously through eligible Tasks within an authorized slice. Human review remains concentrated at meaningful governance boundaries and the final release gate.

## Slice Model

```text
Architecture / Specifications
            ↓
       Slice Definition
            ↓
    Stories + Tasks
            ↓
 Build → Test → Fix → Retest
            ↓
       Validate + Evidence
            ↓
       Slice Completion
            ↓
        Next Slice
```

Slices are sequential by default. Later slices may not depend on behavior that has not been validated in an earlier slice.

Parallel implementation is permitted only when work is genuinely independent, has no conflicting ownership of the same architectural boundary, and the applicable governance and branch rules allow it. Parallel work must not become a substitute for integrating and validating each slice.

## Planned Slices

### Slice 1 — Governed Request to Proposal

**Milestone:** M1  
**Status:** First implementation target

```text
Governed Request → Identity → Domain → Authorization
→ Minimum-Necessary Context → Proposal
```

Proves the foundational authority boundaries. See `docs/implementation/First_Reference_Slice.md`.

### Slice 2 — Governed Knowledge and Memory

**Milestone:** M2

```text
Authorized Task Context → Governed Information/Memory
→ Provenance → Sensitivity → Lifecycle → Controlled Sharing
```

Proves that information and memory remain governed knowledge rather than authority, and that cross-domain movement is explicit and minimum necessary.

### Slice 3 — Governed Reasoning and Decision Formation

**Milestone:** M3

```text
Authorized Context → Provider-Independent Reasoning
→ Uncertainty/Provenance → Governed Decision/Proposal
```

Proves that reasoning consumes authorized context but cannot authorize or execute an action.

### Slice 4 — Approval and Consequential Governance

**Milestone:** M4

```text
Proposal → Approval/Autonomy Evaluation
→ Immediate Pre-Execution Governance Validation
```

Proves that approval and autonomy are bounded, explicit, disableable, and revalidated before consequential action.

### Slice 5 — Bounded Execution and External Outcomes

**Milestone:** M5

```text
Validated Decision → Declared Capability
→ Bounded Invocation → External Outcome
```

Proves that capability invocation is constrained and that dispatch is distinct from what actually happened in external reality.

### Slice 6 — Accountability, Assurance, and Containment

**Milestone:** M6

```text
Governed Event → Evidence → Assurance
→ Trust/Fault Detection → Containment
```

Proves reconstructable accountability and the ability to contain trust failures without weakening authority boundaries.

### Slice 7 — Configuration, Evolution, and Release Hardening

**Milestone:** M7

```text
Governance-Affecting Change → Validation → Traceability
→ Full-System Assurance → Release Readiness
```

Proves that evolution itself remains governed and prepares the integrated system for final human release review.

## Slice Completion Rules

A slice is complete only when:

1. all Tasks required by the slice are complete or explicitly escalated;
2. applicable acceptance criteria pass;
3. invariant and specification tests pass;
4. regression tests preserve previously validated behavior;
5. traceability can be followed from implementation through tests to evidence;
6. non-goals have not been silently introduced;
7. no mandatory stop condition remains unresolved;
8. the repository remains buildable and testable.

## Autonomous Execution Rule

The existence of multiple planned slices does **not** mean the agent should implement everything at once.

The recommended operating unit is:

**One coherent vertical slice authorized at a time, with continuous autonomous execution inside that slice.**

This gives us the benefit of autonomous development without allowing a large, unvalidated chain of assumptions to accumulate across the entire system.

## Expansion Rule

When a slice is completed, the next slice may be authorized from the approved backlog if its prerequisites are satisfied and no governance conflict exists. The agent should not invent new slices, alter slice boundaries materially, or skip required validation merely to maintain momentum.
