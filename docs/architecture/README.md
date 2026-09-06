# A.R.I.A. Architecture Documentation

## Purpose

This directory contains A.R.I.A.'s approved conceptual architecture and its architectural entry point.

## Current Baseline

**Conceptual Architecture:** Frozen — v1.0

The conceptual architecture is the stable architectural model from which implementation architecture and specifications are derived.

## Documents

- [A.R.I.A. Conceptual Architecture v1.0](ARIA_Conceptual_Architecture_v1.0.md)

## Architectural Authority

The conceptual architecture is subordinate to:

```text
Constitution
    ↓
Invariants
    ↓
Conceptual Architecture
```

It is authoritative within its scope and must not be silently redefined by specifications, implementation architecture, code, provider behavior, or operational convention.

## Relationship to Implementation

The conceptual architecture defines **how A.R.I.A. must fundamentally be organized**.

The implementation architecture defines **how that organization is represented as logical implementation boundaries** without prematurely selecting technology.

Architecture specifications define **what guarantees those boundaries must provide**.

```text
Conceptual Architecture
        ↓
Implementation Architecture
        ↓
Architecture Specifications
        ↓
Capabilities / Epics / Stories
        ↓
Implementation
```

## Change Rule

Changes to the conceptual architecture are architectural changes. They require explicit review and must preserve or deliberately amend the Constitution and invariants through the governed change process.
