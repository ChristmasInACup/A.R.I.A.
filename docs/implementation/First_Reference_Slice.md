# A.R.I.A. First Reference Slice

**Status:** Implementation in progress  
**Purpose:** Establish the smallest executable proof of the approved implementation architecture.

## Slice

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

## Traceability

Primary architectural specifications represented by this slice:

- `docs/specs/identity/identity-domain.md`
- `docs/specs/authority/authorization-policy.md`
- `docs/specs/reasoning/context-assembly.md`
- `docs/specs/execution/decision-proposal.md`

Primary governing invariants represented by this slice:

- Identity does not imply authorization.
- Technical access does not imply authorization.
- Authority cannot escalate implicitly.
- Cross-domain information movement is explicit.
- Context is minimum necessary.
- Knowledge, reasoning, and proposal cannot create authority.

The exact Task/Story lineage must be taken from the approved planning baseline when the implementation work is split into individual PRs. This document intentionally does not invent planning identifiers.

## Validation target

The xUnit suite should establish executable evidence for the permitted and denied paths above. CI builds and runs the solution on every pull request and on pushes to `main`.
