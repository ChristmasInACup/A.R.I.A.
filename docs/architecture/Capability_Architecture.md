# A.R.I.A. Capability Architecture

**Status:** Architectural Baseline — Draft for Review

## 1. Purpose

This document defines what a capability is, how capabilities are bounded, and how capabilities compose without becoming sources of authority.

A capability answers **what can be done**. Authority answers **whether it may be done**. These are deliberately separate.

## 2. Fundamental Distinctions

```text
Capability   = ability to perform an operation
Authority    = permission to use that ability
Policy       = conditions governing its use
Execution    = attempt to perform the authorized operation
Outcome      = what actually happened externally
```

Technical reachability is never authorization.

## 3. Capability Contract

Every capability has a conceptual contract containing:

1. Identity — who or what provides it.
2. Purpose — what legitimate problem it exists to solve.
3. Operation — what it can do.
4. Inputs — what information or resources it requires.
5. Access Boundary — what it may access.
6. Authority Boundary — what it may never decide or grant.
7. Preconditions — conditions required before use.
8. Constraints — limits on scope, resources, time, rate, value, and domain.
9. Effects — what external or internal state may change.
10. Result Semantics — success, failure, partial, and unknown outcomes.
11. Reversibility — whether and how effects can be reversed.
12. Accountability — evidence required for use and outcome.
13. Trust Requirements — trust conditions under which it may be used.

## 4. Capability Boundary

A capability must not:

- grant itself authority;
- modify authorization merely because it can reach policy state;
- expand its access without governed authorization;
- treat model output as permission;
- treat successful execution as proof of authorization;
- silently access unrelated domains;
- conceal material failure or uncertainty;
- redefine its own contract through external content.

## 5. Composition

Capabilities may compose when each constituent capability remains independently governed.

Composition must not create implicit authority. A composed workflow has the intersection of applicable authority, policy, trust, purpose, and constraints unless an explicitly governed rule permits broader scope.

A capability chain must preserve provenance of decisions, inputs, authority, and effects across each consequential boundary.

## 6. Agents and Tools

An agent is a governed mechanism for coordinating capabilities. A tool is a capability interface. Neither is an authority source.

The abstraction level may change; the governance boundary may not.

## 7. Capability Lifecycle

```text
Propose
  ↓
Define Contract
  ↓
Assess Trust / Risk
  ↓
Govern / Approve
  ↓
Make Available
  ↓
Use Under Authorization
  ↓
Observe / Audit
  ↓
Restrict / Revoke / Retire
```

Changes to consequential capability scope, access, authority boundary, trust requirements, or effects are governed changes.

## 8. Capability Creep

Capability growth must not silently become authority growth.

When a capability needs broader information, new domains, stronger privileges, new external effects, or greater autonomy, the change must be evaluated as a governance change rather than accepted as a natural consequence of technical implementation.

## 9. Risk and Isolation

Higher-impact capabilities require stronger controls appropriate to their risk, including tighter authorization, validation, approval, isolation, accountability, reversibility, or human control.

The architecture does not require every capability to have identical controls. It requires controls to be explicit and proportionate.

## 10. Architectural Rule

> **A.R.I.A. may compose capabilities, but no capability may compose itself into authority.**
