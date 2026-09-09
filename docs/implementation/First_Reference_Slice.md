# A.R.I.A. First Reference Slice

**Status:** Ready — M1 Reference Slice 1  
**Purpose:** Establish the smallest executable proof of the approved implementation architecture.

## Slice 1 — Governed Request to Proposal

```text
Governed Request
      ↓
   Identity
      ↓
    Domain
      ↓
 Authorization
      ↓
Minimum-Necessary Context
      ↓
   Proposal
```

This is the first executable vertical slice. It is intentionally narrow and should be completed end-to-end before the implementation expands into the next slice.

## Story and Task Lineage

The slice is composed from the approved implementation backlog:

- **EPIC-02 — Identity and Domain Context**
  - STORY-02.1 — Represent identity state
  - STORY-02.2 — Prevent identity-to-authority escalation
- **EPIC-03 — Authorization and Effective Authority**
  - STORY-03.1 — Evaluate effective authority
  - STORY-03.2 — Enforce lifecycle and delegation limits
- **EPIC-04 — Minimum-Necessary Context**
  - STORY-04.1 — Assemble authorized context
  - STORY-04.2 — Preserve provenance and uncertainty
- **EPIC-05 — Proposal Formation**
  - STORY-05.1 — Form governed proposal
  - STORY-05.2 — Complete first vertical slice

The individual tasks under these Stories are authoritative in `docs/planning/Implementation_Backlog.md`. An implementation task must not be expanded beyond its approved scope merely to complete this slice.

## Architectural intent

This slice proves separation of identity, authority, context, and proposal without introducing a reasoning provider or execution system.

The implementation must demonstrate:

- an identified subject does not become authorized merely by being identified;
- unresolved or mismatched identity is denied;
- authorization is evaluated explicitly before authorized context is assembled;
- context is created only after authorization succeeds;
- proposal construction consumes authorized context and does not grant authority;
- changing the authorization decision changes whether a proposal can be produced.

## Explicit non-goals

- AI/LLM providers
- reasoning-provider integration
- external execution
- approvals and autonomy
- databases or persistence
- production authentication
- UI/API hosting
- deployment infrastructure
- distributed services
- cross-language boundaries

## Validation target

The implementation language and test framework for this reference implementation are governed by ADR-0001: **C# with xUnit**. The repository CI builds and tests the solution on pull requests and pushes to `main`.

Validation must include both permitted and denied paths and must preserve executable evidence for the applicable invariants and specifications.

## Completion boundary

Slice 1 is complete only when its end-to-end permitted path, denied paths, invariant tests, specification-aligned behavior, and evidence are validated. Completion of Slice 1 does not authorize work from later milestones unless that work is already within the agent's authorized scope.

If implementation reveals an architectural conflict, material ambiguity, security conflict, scope expansion, or another mandatory stop condition, work stops at that boundary rather than inventing a solution.
