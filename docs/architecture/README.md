# A.R.I.A. Architecture Documentation

## Purpose

This directory contains A.R.I.A.'s conceptual architecture and the architectural closure documents that establish its stable first-principles boundaries.

## Current Baseline

**Conceptual Architecture:** Frozen — v1.0

**Architecture Completion:** Baseline established on `architecture/capability-baseline`; pending explicit review before being treated as the next approved architectural baseline.

The conceptual architecture is the stable architectural model from which implementation architecture and specifications are derived.

## Documents

### Foundational Architecture

- [A.R.I.A. Conceptual Architecture v1.0](ARIA_Conceptual_Architecture_v1.0.md)

### Architectural Completion

- [Capability Architecture](Capability_Architecture.md)
- [Organizational Profiles Architecture](Organizational_Profiles_Architecture.md)
- [Autonomy Architecture](Autonomy_Architecture.md)
- [Agent Architecture](Agent_Architecture.md)
- [Learning and Preference Architecture](Learning_and_Preference_Architecture.md)
- [Architectural Closure and Red-Team Baseline](Architectural_Closure_and_Red_Team.md)

## Architectural Authority

The conceptual architecture and its approved extensions are subordinate to:

```text
Constitution
    ↓
Invariants
    ↓
Conceptual Architecture
```

Architectural documents are authoritative within their defined scope. They must not be silently redefined by specifications, implementation, code, provider behavior, or operational convention.

## Relationship to Lower Layers

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
Capabilities / Epics / Stories
    ↓
Implementation
    ↓
Tests / Evidence
```

The architecture defines **what must fundamentally remain true**. Lower layers determine how those requirements are represented and delivered without violating higher-level authority.

## Architectural Completion Rule

The architecture is considered closed when a reasonable implementation problem can be placed into an existing boundary without inventing a new source of authority, trust, knowledge, execution permission, or human-control exception.

If implementation reveals a genuine architectural gap, the architecture must be deliberately reopened through governance. Implementation may refine architecture; it may not silently create architecture.

## Change Rule

Changes to the conceptual architecture are architectural changes. They require explicit review and must preserve or deliberately amend the Constitution and invariants through the governed change process.
