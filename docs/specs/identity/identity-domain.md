# Identity & Domain Specification

**Specification ID:** ARIA-SPEC-ID-001
**Status:** Draft
**Authority:** Constitution; Invariants 3, 4, 9, 17–19, 34; Conceptual Architecture §3; Implementation Architecture §4

## Scope
Defines how A.R.I.A. establishes actor/service identity and domain context without granting authority.

## Non-Goals
Does not decide authorization, policy, delegation, approval, or capability execution.

## Actors / Components
Human actors, services, modules, providers, organizational units, domains, Identity & Domain, Authorization & Policy.

## Preconditions
An interaction or state change has entered a governed A.R.I.A. boundary.

## Inputs
Identity claims, authentication evidence, actor/service metadata, organizational and domain context, lifecycle state.

## Required Behavior
1. A.R.I.A. shall distinguish identity from authorization.
2. A.R.I.A. shall distinguish domain from permission.
3. Identity records shall identify the relevant actor or system subject and its lifecycle state.
4. Domain context shall be explicit where it affects governance.
5. Unknown, ambiguous, expired, revoked, or conflicting identity information shall not be silently treated as authoritative.
6. Identity or domain information shall not by itself grant authority or permit cross-domain access.
7. Identity interpretation shall preserve the distinction between authenticated, identified, authorized, and trusted states.
8. Intent interpretation shall not infer authority from identity alone.

## Authority / Governance Rules
Authorization is evaluated by the Authority Plane. Technical reachability, organizational membership, authentication, or identity confidence shall not substitute for authorization.

## Data / Knowledge Rules
Identity and domain state shall have an owner, lifecycle, provenance where applicable, and appropriate protection. Domain movement shall be explicit.

## Trust / Security Requirements
Identity evidence shall be validated according to risk. Compromise of an identity component shall not automatically grant unrelated authority or cross-domain access.

## Failure / Uncertainty Semantics
Ambiguous or unavailable identity shall produce an explicit unresolved state. Consequential actions requiring unresolved identity shall not proceed.

## Temporal / Concurrency Semantics
Identity validity, revocation, lifecycle changes, and domain membership changes shall have effective-time semantics where relevant.

## Outputs
Established identity and domain context, or an explicit unresolved/denied result.

## Accountability / Audit Requirements
Significant governed events shall record the identity and domain basis used for subsequent governance decisions.

## Invariants Covered
3, 4, 9, 17, 18, 19, 34.

## Acceptance Criteria
- Identity never directly authorizes an action.
- Domain crossing requires explicit governance.
- Invalid or unresolved identity cannot silently become trusted identity.
- Identity changes are attributable and auditable.

## Test Obligations
Test authenticated-but-unauthorized actors, expired/revoked identities, ambiguous identity, domain crossing, compromised identity components, and organizational growth scenarios.

## Open Questions / ADRs
Define detailed identity assurance levels and organizational domain taxonomy through later ADRs if implementation requires choices not established by architecture.
