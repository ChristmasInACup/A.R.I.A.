# A.R.I.A. Architecture Review Checklist

**Status:** Supporting Governance Artifact

> **Purpose:** Provide a repeatable review checklist for architectural changes and baseline approval.  
> **What done looks like:** Reviewers can determine the affected layer, verify higher-level consistency, confirm acceptance criteria, and record an explicit approval decision.

## 1. Identify the Affected Layer

- [ ] Constitution
- [ ] Invariants
- [ ] Conceptual Architecture
- [ ] Implementation Architecture
- [ ] Architecture Specifications
- [ ] Capabilities
- [ ] Epics / Stories / Tasks
- [ ] Implementation / Tests / Evidence

If a change affects a higher layer, lower-layer implementation must not be used to bypass review of that higher layer.

## 2. Architectural Consistency

- [ ] No new source of authority is introduced.
- [ ] Identity remains distinct from authorization.
- [ ] Knowledge and memory remain distinct from authority and truth.
- [ ] Reasoning and providers remain subordinate to governance.
- [ ] Capability remains distinct from authority.
- [ ] Execution remains distinct from external outcome.
- [ ] Trust remains contextual and non-transitive.
- [ ] Cross-domain movement remains explicit and governed.
- [ ] Human control remains preserved.
- [ ] Failure behavior reduces capability before weakening governance.

## 3. Acceptance and Traceability

- [ ] Purpose and non-goals are explicit.
- [ ] "What done looks like" criteria are present where appropriate.
- [ ] Affected invariants are identified.
- [ ] Related specifications and ADRs are identified.
- [ ] Security, trust, accountability, and uncertainty implications are addressed.
- [ ] Required tests and evidence are identified for lower-layer implementation.

## 4. Approval

Record:

- Change / document reviewed:
- Reviewer:
- Date:
- Decision: Approved / Changes Requested / Rejected
- Conditions or follow-up:

Approval applies only to the stated scope. It does not grant authority to lower layers to redefine higher-level decisions.
