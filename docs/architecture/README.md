# A.R.I.A. Architecture Documentation

## Purpose

This directory contains A.R.I.A.'s conceptual architecture and the architectural completion documents that establish its stable first-principles boundaries.

## Quick Navigation

| Document | Purpose | What it closes |
|---|---|---|
| [Conceptual Architecture v1.0](ARIA_Conceptual_Architecture_v1.0.md) | Foundational architecture | Identity, authority, knowledge, reasoning, capability, execution, accountability, and human control |
| [Capability Architecture](Capability_Architecture.md) | Capability boundaries | Capability contracts, composition, risk, and separation from authority |
| [Organizational Profiles Architecture](Organizational_Profiles_Architecture.md) | Organizational scale | Profiles, configuration, risk depth, multi-domain boundaries, and growth |
| [Autonomy Architecture](Autonomy_Architecture.md) | Bounded autonomy | Autonomy levels, standing authority, escalation, revocation, and human control |
| [Agent Architecture](Agent_Architecture.md) | Governed agents | Agent scope, delegation, context, capabilities, lifecycle, and failure |
| [Learning and Preference Architecture](Learning_and_Preference_Architecture.md) | Governed adaptation | Learning sources, preference boundaries, correction, forgetting, and authority safety |
| [Architectural Closure and Red-Team Baseline](Architectural_Closure_and_Red_Team.md) | Architecture closure | End-to-end completeness, red-team questions, debt prevention, and implementation readiness |
| [Architecture Change Process](Architecture_Change_Process.md) | Change governance | How architectural changes are proposed, reviewed, approved, and recorded |
| [Architecture Review Checklist](Architecture_Review_Checklist.md) | Review aid | Repeatable architectural review and approval checks |

## Governance and Traceability

- [Architecture Traceability Matrix](../governance/Architecture_Traceability_Matrix.md) — maps the 41 architectural invariants to specifications and future verification/evidence targets.
- [ADR-0001: Establish the Architectural Completion Baseline](../decisions/ADR-0001-architectural-completion-baseline.md) — records why the completion documents were added and the approval/merge sequence.
- [Architecture Change Process](Architecture_Change_Process.md) — defines how future architectural changes are proposed, reviewed, approved, and recorded.
- [Architecture Review Checklist](Architecture_Review_Checklist.md) — provides the repeatable review and approval checks.

## Current Baseline

**Conceptual Architecture:** Frozen — v1.0

**Architectural Baseline:** The foundational conceptual architecture and the architectural completion documents together form the current conceptual architecture baseline once this branch is approved.

The conceptual architecture is the stable architectural model from which implementation architecture and specifications are derived.

## Architectural Structure

```text
Constitution
    ↓
Invariants
    ↓
Conceptual Architecture v1.0
    ↓
Architectural Completion / Current Conceptual Baseline
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

## Architectural Authority

The conceptual architecture and its approved completion documents are subordinate to:

```text
Constitution
    ↓
Invariants
    ↓
Conceptual Architecture
```

Architectural documents are authoritative within their defined scope. They must not be silently redefined by specifications, implementation, code, provider behavior, or operational convention.

## Relationship to Lower Layers

The architecture defines **what must fundamentally remain true**. Lower layers determine how those requirements are represented and delivered without violating higher-level authority.

## Architectural Completion Rule

The architecture is considered closed for implementation when a reasonable implementation problem can be placed into an existing boundary without inventing a new source of authority, trust, knowledge, execution permission, or human-control exception.

If implementation reveals a genuine architectural gap, the architecture must be deliberately reopened through governance. Implementation may refine architecture; it may not silently create architecture.

## Change Rule

Changes to the conceptual architecture are architectural changes. They require explicit review and must preserve or deliberately amend the Constitution and invariants through the governed change process.

See the [Architecture Change Process](Architecture_Change_Process.md) for the required process.
