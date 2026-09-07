# A.R.I.A. Autonomy Architecture

**Status:** Architectural Baseline — Draft for Review

> **Purpose:** Define how A.R.I.A. may act without obtaining a new human decision at every step while remaining bounded by existing authority and governance.  
> **What done looks like:** Every autonomous action has a bounded authority envelope, explicit conditions, revocation/expiration semantics, escalation rules, and human control.

## 1. Purpose

Autonomy determines how much execution A.R.I.A. may perform without obtaining a new human decision at each step. Autonomy never creates authority; it operates within authority already established by governance.

## 2. Autonomy Levels

```text
0  Observe
1  Recommend
2  Prepare
3  Approval Required
4  Autonomous Execution
```

A higher level is not inherently better. The appropriate level depends on risk, authority, reversibility, trust, purpose, and organizational policy.

## 3. Bounded Standing Authority

Standing autonomy must be bounded by, at minimum:

- authorized actor or principal;
- action or capability;
- resources;
- domain;
- purpose;
- maximum scope or value;
- time window;
- conditions;
- risk limits;
- required evidence;
- revocation state.

A standing permission for one action, purpose, or domain does not imply permission for another.

## 4. Escalation

A.R.I.A. should escalate when a requested operation exceeds the established autonomy envelope, encounters material uncertainty, loses required trust, reaches a risk threshold, or encounters a consequential change in circumstances.

Escalation should preserve the user's intent while surfacing the smallest meaningful decision required.

## 5. Approval

Approval is authorization for a defined consequential operation under defined conditions. Approval is not unrestricted authority and does not automatically authorize materially different actions.

If the proposed execution changes materially after approval, governance must determine whether approval remains valid or a new approval is required.

## 6. Revocation and Expiration

Autonomy must support explicit revocation and bounded validity. Expired, revoked, invalidated, or materially changed authority must not be treated as active merely because a workflow was previously permitted.

## 7. Safe Failure

When required authorization, governance, trust, or accountability cannot be established, A.R.I.A. must reduce capability before weakening governance.

Provider failure may reduce reasoning capability. It must not silently expand autonomy.

## 8. Human Control

Humans retain the ability to inspect, interrupt, override, disable, and take control of consequential system behavior according to organizational governance.

Autonomy must never become a one-way transfer of organizational control.

## 9. What Done Looks Like

Before autonomous execution is enabled, verify:

- the applicable autonomy level is explicit;
- the principal, action, resources, domain, purpose, scope/value, time, conditions, and risk limits are bounded;
- required evidence is defined;
- escalation triggers are defined;
- approval requirements are explicit where applicable;
- expiration, revocation, and material-change behavior are defined;
- consequential failure reduces capability rather than weakening governance;
- human interruption, override, disablement, and takeover remain available.

## 10. Architectural Rule

> **Autonomy is bounded execution under authority; it is never authority created by execution.**
