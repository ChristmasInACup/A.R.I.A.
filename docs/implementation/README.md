# A.R.I.A. Implementation Documentation

## Purpose

This directory defines the implementation-phase architecture and engineering rules needed to realize the approved conceptual architecture without prematurely binding A.R.I.A. to unnecessary frameworks, infrastructure, or AI providers.

## Current Status

**Implementation Architecture:** Established — implementation-phase baseline

**Implementation Technology:** C# / xUnit — accepted by ADR-0001 for the initial reference implementation

The implementation layer defines logical responsibilities, boundaries, data ownership, interfaces, deployment concerns, and engineering discipline that concrete implementations must preserve.

## Documents

### Implementation Governance

- [Implementation Charter](Implementation_Charter.md) — engineering principles, AI engineering contract, planning hierarchy, workflow, testing, evidence, and definition of done.
- [Autonomous Implementation Protocol](Autonomous_Implementation_Protocol.md) — controlled autonomous build → test → fix → continue operating mode, stop conditions, milestone gates, testing, evidence, and final human release authority.

### Planning

- [Autonomous Implementation Backlog](../planning/Implementation_Backlog.md) — dependency-ordered Epics → Stories → Tasks, milestone exit criteria, acceptance criteria, tests, traceability, and autonomous execution rules.
- [Implementation Slice Strategy](../planning/Implementation_Slice_Strategy.md) — sequence of coherent vertical slices and rules for progressing between them.

### Reference Slice

- [First Reference Slice](First_Reference_Slice.md) — the first executable vertical slice and its approved Story/Task lineage.

### Logical Architecture

- [Implementation Architecture](Implementation_Architecture.md) — logical component organization and architectural boundaries.
- [System Components](System_Components.md) — component responsibilities and ownership boundaries.

### Boundary Architecture

- [Data Architecture](Data_Architecture.md) — logical data domains, ownership, provenance, lifecycle, and governance semantics.
- [Interface Architecture](Interface_Architecture.md) — controlled interaction boundaries, directionality, validation, failure, and evolution semantics.
- [Deployment Architecture](Deployment_Architecture.md) — physical/deployment concerns and preservation of logical trust and authority boundaries.

## Architectural Position

Implementation documentation sits below the approved conceptual architecture and above concrete code:

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
Autonomous Implementation Loop
    ↓
Tests
    ↓
Evidence
    ↓
Human Release Decision
```

## Autonomous Implementation Position

A.R.I.A. may operate in an autonomous implementation mode once the relevant backlog and governance are approved. In this mode, AI coding agents may continuously execute eligible tasks within an authorized slice and self-correct ordinary implementation failures without requiring human approval after every task.

Human authority remains concentrated at architectural decisions, material scope changes, governance conflicts, and the final release decision.

The autonomous protocol does **not** grant the coding agent authority to redefine architecture, weaken security, invent requirements, bypass governance, or merge the final consequential implementation.

## Technology Boundary

ADR-0001 establishes **C# with xUnit** as the initial reference-implementation technology. This is a scoped implementation decision, not a commitment that every future A.R.I.A. component must use C#.

Future Python use remains possible where a concrete responsibility demonstrates a material advantage, particularly for AI/reasoning, experimentation, evaluation, or tooling. Introducing Python or a cross-language boundary requires an independently justified implementation or architectural decision when its consequences are durable.

The accepted technology decision does not authorize an application framework, database, cloud provider, AI provider, model vendor, or deployment topology unless separately required and governed.

## Change Rule

Implementation architecture may evolve, but changes that affect authority, security, knowledge ownership, trust, execution, accountability, human control, or other durable architectural boundaries must be reviewed at the appropriate architectural level.

> **Implementation is allowed to evolve. It is not allowed to silently erode the architecture.**
