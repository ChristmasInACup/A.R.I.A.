# A.R.I.A. Specification Governance

**Status:** Foundational governance artifact — Draft
**Baseline:** Implementation documentation baseline
**Last Updated:** 2026-09-06

## Purpose

This document defines how A.R.I.A.'s architecture specifications are owned, reviewed, approved, frozen, superseded, or retired.

The registry records governance state. It does not create authority, grant authorization, or override the Constitution, Invariants, or frozen architecture.

## Specification Lifecycle

```text
DRAFT → UNDER REVIEW → APPROVED → FROZEN
                         ↓
                  SUPERSEDED / RETIRED
```

- **Draft:** Proposed normative boundary; not yet approved for implementation as an authoritative specification.
- **Under Review:** Explicit review is in progress.
- **Approved:** Explicitly accepted as the current normative specification.
- **Frozen:** Approved and protected from casual change; changes require the governed change process.
- **Superseded:** Replaced by a newer approved specification.
- **Retired:** No longer applicable and intentionally removed from the active architecture.

No implementation work may treat a Draft specification as approved authority merely because the file exists.

## Governance Registry

| ID | Specification | Owner | Reviewer | Status | Approval | Last Updated | Approval Record |
|---|---|---|---|---|---|---|---|
| ARIA-SPEC-ID-001 | [Identity & Domain](../specs/identity/identity-domain.md) | Repository Owner (`ChristmasInACup`) | Pending explicit assignment | Draft | Pending | 2026-09-06 | PR #2 |
| ARIA-SPEC-AUTH-001 | [Authorization & Policy](../specs/authority/authorization-policy.md) | Repository Owner (`ChristmasInACup`) | Pending explicit assignment | Draft | Pending | 2026-09-06 | PR #2 |
| ARIA-SPEC-AUTH-002 | [Delegation & Authority](../specs/authority/delegation-authority.md) | Repository Owner (`ChristmasInACup`) | Pending explicit assignment | Draft | Pending | 2026-09-06 | PR #2 |
| ARIA-SPEC-AUTH-003 | [Approval, Autonomy & Human Control](../specs/authority/approval-autonomy-human-control.md) | Repository Owner (`ChristmasInACup`) | Pending explicit assignment | Draft | Pending | 2026-09-06 | PR #2 |
| ARIA-SPEC-KNOW-001 | [Knowledge Lifecycle](../specs/knowledge/knowledge-lifecycle.md) | Repository Owner (`ChristmasInACup`) | Pending explicit assignment | Draft | Pending | 2026-09-06 | PR #2 |
| ARIA-SPEC-KNOW-002 | [Memory](../specs/knowledge/memory.md) | Repository Owner (`ChristmasInACup`) | Pending explicit assignment | Draft | Pending | 2026-09-06 | PR #2 |
| ARIA-SPEC-KNOW-003 | [Provenance & Information Trust](../specs/knowledge/provenance-information-trust.md) | Repository Owner (`ChristmasInACup`) | Pending explicit assignment | Draft | Pending | 2026-09-06 | PR #2 |
| ARIA-SPEC-REAS-001 | [Context Assembly](../specs/reasoning/context-assembly.md) | Repository Owner (`ChristmasInACup`) | Pending explicit assignment | Draft | Pending | 2026-09-06 | PR #2 |
| ARIA-SPEC-REAS-002 | [Reasoning Coordination](../specs/reasoning/reasoning-coordination.md) | Repository Owner (`ChristmasInACup`) | Pending explicit assignment | Draft | Pending | 2026-09-06 | PR #2 |
| ARIA-SPEC-REAS-003 | [Provider & Model Governance](../specs/reasoning/provider-model-governance.md) | Repository Owner (`ChristmasInACup`) | Pending explicit assignment | Draft | Pending | 2026-09-06 | PR #2 |
| ARIA-SPEC-EXEC-001 | [Decision & Proposal](../specs/execution/decision-proposal.md) | Repository Owner (`ChristmasInACup`) | Pending explicit assignment | Draft | Pending | 2026-09-06 | PR #2 |
| ARIA-SPEC-EXEC-002 | [Capability Contracts](../specs/execution/capability-contracts.md) | Repository Owner (`ChristmasInACup`) | Pending explicit assignment | Draft | Pending | 2026-09-06 | PR #2 |
| ARIA-SPEC-EXEC-003 | [Execution Governance](../specs/execution/execution-governance.md) | Repository Owner (`ChristmasInACup`) | Pending explicit assignment | Draft | Pending | 2026-09-06 | PR #2 |
| ARIA-SPEC-EXEC-004 | [External Outcomes](../specs/execution/external-outcomes.md) | Repository Owner (`ChristmasInACup`) | Pending explicit assignment | Draft | Pending | 2026-09-06 | PR #2 |
| ARIA-SPEC-ASR-001 | [Accountability & Audit](../specs/assurance/accountability-audit.md) | Repository Owner (`ChristmasInACup`) | Pending explicit assignment | Draft | Pending | 2026-09-06 | PR #2 |
| ARIA-SPEC-ASR-002 | [Observability & Security Assurance](../specs/assurance/observability-security-assurance.md) | Repository Owner (`ChristmasInACup`) | Pending explicit assignment | Draft | Pending | 2026-09-06 | PR #2 |
| ARIA-SPEC-EVOL-001 | [Trust, Security & Containment](../specs/evolution/trust-security-containment.md) | Repository Owner (`ChristmasInACup`) | Pending explicit assignment | Draft | Pending | 2026-09-06 | PR #2 |
| ARIA-SPEC-EVOL-002 | [Configuration, Change & Evolution](../specs/evolution/configuration-change-evolution.md) | Repository Owner (`ChristmasInACup`) | Pending explicit assignment | Draft | Pending | 2026-09-06 | PR #2 |

## Approval Record

For this baseline, approval is intentionally explicit rather than inferred from file creation, implementation activity, or review of unrelated documents.

A specification is approved only when the repository owner or explicitly delegated architectural authority records acceptance through the repository's governed review process.

The approval record should identify:

- specification ID;
- approved revision or commit;
- approving authority;
- review date;
- material conditions, if any.

## Change Rules

Changes to an approved or frozen specification must preserve the higher-level architecture and invariants. A change that would alter a constitutional, invariant, or conceptual-architecture guarantee must stop and follow the appropriate higher-level governance path rather than being hidden inside a specification edit.

Specifications may clarify implementation boundaries, but may not:

- create a competing source of authority;
- grant authority through technical reachability;
- redefine identity as authorization;
- redefine memory or knowledge as truth or authority;
- make a provider or model an authority boundary;
- convert approval into unrestricted authorization;
- silently broaden autonomy;
- erase uncertainty or external outcome semantics;
- bypass human control;
- introduce technology commitments without an approved architectural reason.

## Review Completion Criteria

Before the specification set is approved as a baseline, reviewers must confirm:

1. Every specification has a single clear primary responsibility.
2. Every specification has an explicit owner and approval state.
3. The full set preserves all architectural invariants.
4. Authority paths are explicit and non-transitive.
5. Cross-domain information movement is governed.
6. Context is minimum-necessary and does not become an authority channel.
7. Reasoning and providers remain non-authoritative.
8. Approval and autonomy remain bounded by authorization and governance.
9. Execution is distinct from external outcome.
10. Failure and uncertainty do not create authority.
11. Accountability, audit, observability, and security assurance remain distinct responsibilities.
12. Human disablement and takeover remain available.
13. Governance-affecting changes remain governed.
14. No specification silently introduces implementation technology or framework authority.

## Governing Principle

> **Documentation preserves architectural knowledge; governance determines architectural authority.**
