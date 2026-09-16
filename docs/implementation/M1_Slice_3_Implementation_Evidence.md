# M1 Slice 3 — Implementation Evidence

## Status

Implementation complete on `implementation/m1-slice-3`; awaiting CI and human review.

## Implemented

- Governed proposal now preserves stable proposal identity and revision number.
- Originating request is represented by a deterministic request fingerprint.
- Authorization basis remains linked as evidence/reasoning context rather than becoming proposal authority.
- Recommendation, intended effect, conditions, domain, purpose, and scope are represented separately.
- Proposal evidence preserves context provenance, uncertainty, and lifecycle state.
- Material revisions preserve proposal lineage and increment revision.
- Proposal containment is checked against the authorized request/context.
- A dedicated decision boundary evaluates only downstream governance eligibility.
- The decision boundary checks current authorization validity, proposal lifecycle, provenance, lifecycle health, uncertainty, and conflicting evidence.
- Boundary eligibility explicitly does not approve, authorize, broaden authority, invoke capabilities, or execute actions.

## Tests

`GovernedProposalTests` covers proposal formation, stable linkage, revision/change detection, scope/domain/purpose containment, stale authorization, unknown/stale/conflicting evidence, malicious content, and the non-authorizing boundary.

Existing Slice 1 and Slice 2 tests remain part of the repository test suite and must remain green.

## Deliberate Non-Goals

No human approval workflow, autonomous approval, capability execution, external side effects, persistence, model/provider integration, distributed coordination, or cryptographic provenance infrastructure was added.

## Review Focus

Review the proposal model and decision-boundary API specifically for accidental authority paths and confirm that proposal eligibility cannot be interpreted as approval or authorization.
