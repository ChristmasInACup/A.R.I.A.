# ADR-0002 — M1 Delegation Model

**Status:** Proposed
**Scope:** M1 governed request authorization
**Authority:** ARIA-SPEC-AUTH-002 — Delegation & Authority

## Context

M1 must evaluate delegated authority without allowing authority to propagate implicitly. The approved delegation specification requires every delegation to be explicitly created and attributable and requires delegation state to preserve issuer, recipient, scope, purpose, domain, conditions, validity, provenance, and revocation state.

The initial M1 implementation represented delegation only through an `AuthorityRecord.DelegatorAuthority` reference. That proves ancestry and inherited constraints but does not explicitly prove that the delegator granted the authority to the recipient represented by the delegated authority's holder.

## Decision

M1 will model **delegation as an explicit relationship between an issuer and a recipient**, while keeping the underlying authority distinct from the delegation relationship.

### AuthorityRecord

An authority record represents authority held by a subject. It contains the holder, domain, purpose, scope, validity, and revocation state. An authority record may reference the authority from which it was derived for bounded-chain evaluation, but that reference alone is not evidence that a delegation occurred.

### Delegation

A delegation represents an explicit grant of bounded authority from an issuer to a recipient. For M1, a delegation must contain:

- a stable delegation identifier;
- issuer/authorizing subject;
- recipient/delegate subject;
- source/delegator authority;
- delegated domain;
- delegated purpose;
- delegated scope;
- validity window;
- revocation state;
- explicit permission for further delegation;
- provenance/attribution sufficient to establish who created the delegation.

M1 may keep conditions minimal where no condition model is yet required, but the model must not silently discard the specification's condition boundary. Unsupported conditions must fail closed rather than being treated as satisfied.

### Effective delegation

A delegated authority is effective only when all of the following hold at evaluation time:

1. The delegation is explicitly associated with the requested recipient.
2. The issuer/source authority is effective and active.
3. The issuer is permitted to delegate.
4. The delegation's domain exactly matches the required domain.
5. The delegation's purpose exactly matches the required purpose.
6. The delegation's scope is contained by the issuer's effective scope.
7. The delegation's validity window is active and it is not revoked.
8. Every ancestor delegation satisfies the same bounded constraints.
9. No cycle or repeated authority/delegation identity is accepted.
10. Any unverifiable or ambiguous delegation causes denial/unresolved behavior rather than authorization.

### Transitive delegation

Transitive delegation is permitted only when each delegating authority explicitly permits further delegation. Each new delegation must identify its own issuer and recipient and must be independently bounded by the effective authority available to that issuer.

No property of an authority record, by itself, grants permission to an unrelated actor.

### M1 boundary

M1 will not implement persistence, distributed concurrency control, emergency delegation, complex condition evaluation, or a full delegation-management workflow. Those remain future concerns unless required by the governing specifications for the current slice.

M1 will nevertheless represent enough delegation state to prevent implicit recipient propagation and to evaluate the authorization boundary safely.

## Consequences

- Authorization can prove recipient identity rather than trusting object topology.
- Delegation becomes auditable as a distinct governance event.
- Future revocation, versioning, persistence, and delegation-history features have a natural domain boundary.
- M1 requires explicit delegation fixtures and tests for recipient mismatch, transitive delegation, revocation/expiration, scope, domain, purpose, and cycles.
- The implementation should prefer small domain types and focused evaluation methods over a larger authorization object.

## Rejected alternative

Treating `DelegatorAuthority` as sufficient evidence of delegation is rejected because it does not explicitly establish the required issuer-to-recipient relationship and could permit an unrelated holder to inherit authority merely because an authority object references a valid delegator.
