# A.R.I.A. Learning and Preference Architecture

**Status:** Architectural Baseline — Draft for Review

> **Purpose:** Define governed adaptation that improves usefulness without silently changing authority, security, policy, truth claims, or human control.  
> **What done looks like:** Learned state has explicit provenance and governance, material changes are inspectable and correctable, and learning cannot directly alter authority or security boundaries.

## 1. Purpose

A.R.I.A. should improve usefulness over time without silently changing authority, security boundaries, organizational policy, or truth claims.

Learning is therefore governed adaptation, not unrestricted self-modification.

## 2. Preference vs Authority

Preferences may influence how A.R.I.A. performs an already-authorized task.

Preferences must not silently:

- grant authority;
- remove required approval;
- expand information access;
- weaken security controls;
- change domain boundaries;
- alter retention or sharing requirements;
- redefine organizational policy;
- convert uncertain information into truth.

## 3. Learning Classes

Conceptually, learning may affect:

1. **Interaction preferences** — tone, formatting, defaults, communication style.
2. **Workflow preferences** — preferred safe sequences or recurring methods.
3. **Capability preferences** — preferred providers, tools, or methods where governance permits.
4. **Domain knowledge** — retained information subject to knowledge governance.
5. **Policy/governance knowledge** — organizational rules that remain controlled authority artifacts.

The higher the impact on authority or risk, the stronger the governance required.

## 4. Learning Sources

A.R.I.A. must distinguish among explicit user instruction, observed behavior, inferred preference, organizational policy, external information, and model-generated suggestions.

Inference does not automatically become a fact or preference.

## 5. Consent and Explainability

Where learning materially changes future behavior, the system should provide an appropriate way to inspect, correct, reject, or remove the learned state.

Material governance changes require explicit governed change rather than silent learning.

## 6. Correction and Forgetting

Learned state must be revisable. A.R.I.A. must support correction, invalidation, expiration, and forgetting according to the applicable governance and retention requirements.

Correction of a preference does not retroactively rewrite historical accountability evidence.

## 7. Safety Boundary

No learning loop may directly rewrite constitutional rules, invariants, authorization boundaries, or security controls merely because the system predicts that doing so would improve outcomes.

Learning may propose change. Governance decides change.

## 8. What Done Looks Like

Before learning or a learned preference materially affects future behavior, verify:

- the source of the learned state is distinguishable;
- its scope and purpose are explicit;
- applicable provenance, sensitivity, retention, and sharing rules remain intact;
- inference is not silently promoted to fact or authority;
- material behavior changes are inspectable and correctable;
- learned state can be invalidated, expired, or forgotten where governance permits;
- learning cannot directly alter constitutional rules, invariants, authorization, or security controls;
- historical accountability evidence is not rewritten by correction.

## 9. Architectural Rule

> **A.R.I.A. may learn how to serve better; it may not silently learn that it has more authority.**
