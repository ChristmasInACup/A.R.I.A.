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

## PR #2 Documentation Baseline Decision

PR #2 establishes the repository's **implementation documentation baseline**. That repository-level baseline approval is separate from approval of the individual architecture specifications.

The 18 specifications in this baseline are **intentionally Draft**. They are documented, structurally reviewed, and traceable, but they are **not approved as implementation-authoritative specifications by PR #2**.

Therefore:

- the repository owner is the current architectural authority for this documentation baseline;
- no independent reviewer is assigned solely for ceremony where none has been explicitly designated;
- each specification remains intentionally Draft;
- specification-level approval is **Not Applicable** at this stage;
- the approval record for each specification identifies PR #2 as the documentation-baseline context, not as specification approval;
- repository-level approval of PR #2 establishes the authoritative documentation/implementation baseline for the repository, subject to the Constitution, Invariants, and frozen architecture;
- future implementation work must not treat these Draft specifications as approved normative authority until they undergo the explicit specification approval process.

This distinction prevents repository merge status from being mistaken for approval of every normative artifact contained in the repository.

## Governance Registry

| ID | Specification | Owner | Reviewer | Status | Approval | Last Updated | Approval Record |
|---|---|---|---|---|---|---|---|
| ARIA-SPEC-ID-001 | [Identity & Domain](../specs/identity/identity-domain.md) | Repository Owner (`ChristmasInACup`) | Not assigned — owner is current architectural authority | Intentionally Draft | Not applicable — intentionally draft for this documentation baseline | 2026-09-06 | PR #2 — documentation baseline only |
| ARIA-SPEC-AUTH-001 | [Authorization & Policy](../specs/authority/authorization-policy.md) | Repository Owner (`ChristmasInACup`) | Not assigned — owner is current architectural authority | Intentionally Draft | Not applicable — intentionally draft for this documentation baseline | 2026-09-06 | PR #2 — documentation baseline only |
| ARIA-SPEC-AUTH-002 | [Delegation & Authority](../specs/authority/delegation-authority.md) | Repository Owner (`ChristmasInACup`) | Not assigned — owner is current architectural authority | Intentionally Draft | Not applicable — intentionally draft for this documentation baseline | 2026-09-06 | PR #2 — documentation baseline only |
| ARIA-SPEC-AUTH-003 | [Approval, Autonomy & Human Control](../specs/authority/approval-autonomy-human-control.md) | Repository Owner (`ChristmasInACup`) | Not assigned — owner is current architectural authority | Intentionally Draft | Not applicable — intentionally draft for this documentation baseline | 2026-09-06 | PR #2 — documentation baseline only |
| ARIA-SPEC-KNOW-001 | [Knowledge Lifecycle](../specs/knowledge/knowledge-lifecycle.md) | Repository Owner (`ChristmasInACup`) | Not assigned — owner is current architectural authority | Intentionally Draft | Not applicable — intentionally draft for this documentation baseline | 2026-09-06 | PR #2 — documentation baseline only |
| ARIA-SPEC-KNOW-002 | [Memory](../specs/knowledge/memory.md) | Repository Owner (`ChristmasInACup`) | Not assigned — owner is current architectural authority | Intentionally Draft | Not applicable — intentionally draft for this documentation baseline | 2026-09-06 | PR #2 — documentation baseline only |
| ARIA-SPEC-KNOW-003 | [Provenance & Information Trust](../specs/knowledge/provenance-information-trust.md) | Repository Owner (`ChristmasInACup`) | Not assigned — owner is current architectural authority | Intentionally Draft | Not applicable — intentionally draft for this documentation baseline | 2026-09-06 | PR #2 — documentation baseline only |
| ARIA-SPEC-REAS-001 | [Context Assembly](../specs/reasoning/context-assembly.md) | Repository Owner (`ChristmasInACup`) | Not assigned — owner is current architectural authority | Intentionally Draft | Not applicable — intentionally draft for this documentation baseline | 2026-09-06 | PR #2 — documentation baseline only |
| ARIA-SPEC-REAS-002 | [Reasoning Coordination](../specs/reasoning/reasoning-coordination.md) | Repository Owner (`ChristmasInACup`) | Not assigned — owner is current architectural authority | Intentionally Draft | Not applicable — intentionally draft for this documentation baseline | 2026-09-06 | PR #2 — documentation baseline only |
| ARIA-SPEC-REAS-003 | [Provider & Model Governance](../specs/reasoning/provider-model-governance.md) | Repository Owner (`ChristmasInACup`) | Not assigned — owner is current architectural authority | Intentionally Draft | Not applicable — intentionally draft for this documentation baseline | 2026-09-06 | PR #2 — documentation baseline only |
| ARIA-SPEC-EXEC-001 | [Decision & Proposal](../specs/execution/decision-proposal.md) | Repository Owner (`ChristmasInACup`) | Not assigned — owner is current architectural authority | Intentionally Draft | Not applicable — intentionally draft for this documentation baseline | 2026-09-06 | PR #2 — documentation baseline only |
| ARIA-SPEC-EXEC-002 | [Capability Contracts](../specs/execution/capability-contracts.md) | Repository Owner (`ChristmasInACup`) | Not assigned — owner is current architectural authority | Intentionally Draft | Not applicable — intentionally draft for this documentation baseline | 2026-09-06 | PR #2 — documentation baseline only |
| ARIA-SPEC-EXEC-003 | [Execution Governance](../specs/execution/execution-governance.md) | Repository Owner (`ChristmasInACup`) | Not assigned — owner is current architectural authority | Intentionally Draft | Not applicable — intentionally draft for this documentation baseline | 2026-09-06 | PR #2 — documentation baseline only |
| ARIA-SPEC-EXEC-004 | [External Outcomes](../specs/execution/external-outcomes.md) | Repository Owner (`ChristmasInACup`) | Not assigned — owner is current architectural authority | Intentionally Draft | Not applicable — intentionally draft for this documentation baseline | 2026-09-06 | PR #2 — documentation baseline only |
| ARIA-SPEC-ASR-001 | [Accountability & Audit](../specs/assurance/accountability-audit.md) | Repository Owner (`ChristmasInACup`) | Not assigned — owner is current architectural authority | Intentionally Draft | Not applicable — intentionally draft for this documentation baseline | 2026-09-06 | PR #2 — documentation baseline only |
| ARIA-SPEC-ASR-002 | [Observability & Security Assurance](../specs/assurance/observability-security-assurance.md) | Repository Owner (`ChristmasInACup`) | Not assigned — owner is current architectural authority | Intentionally Draft | Not applicable — intentionally draft for this documentation baseline | 2026-09-06 | PR #2 — documentation baseline only |
| ARIA-SPEC-EVOL-001 | [Trust, Security & Containment](../specs/evolution/trust-security-containment.md) | Repository Owner (`ChristmasInACup`) | Not assigned — owner is current architectural authority | Intentionally Draft | Not applicable — intentionally draft for this documentation baseline | 2026-09-06 | PR #2 — documentation baseline only |
| ARIA-SPEC-EVOL-002 | [Configuration, Change & Evolution](../specs/evolution/configuration-change-evolution.md) | Repository Owner (`ChristmasInACup`) | Not assigned — owner is current architectural authority | Intentionally Draft | Not applicable — intentionally draft for this documentation baseline | 2026-09-06 | PR #2 — documentation baseline only |

## Approval Record

For this baseline, approval is intentionally explicit rather than inferred from file creation, implementation activity, or review of unrelated documents.

A specification is approved only when the repository owner or explicitly delegated architectural authority records acceptance through the repository's governed review process.

The approval record should identify:

- specification ID;
- approved revision or commit;
- approving authority;
- review date;
- material conditions, if any.

**Important:** Approval of PR #2 is a repository-level approval of the documentation/implementation baseline. It is not an approval record for any of the 18 Draft specifications.

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
