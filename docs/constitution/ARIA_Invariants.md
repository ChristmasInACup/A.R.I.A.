# A.R.I.A. Architectural Invariants

**Status:** FROZEN  
**Version:** 1.0

These invariants are the non-negotiable rules identified during the architecture and red-team phases. Implementation, configuration, and organizational growth must preserve them.

## Authority

1. Reasoning cannot create authority.
2. Capability cannot create authority.
3. Identity does not imply authorization.
4. Technical access does not imply authorization.
5. Authority cannot escalate implicitly.
6. Delegation cannot exceed delegator authority.
7. Authorization must be valid at consequential use.

## Knowledge

8. Knowledge does not imply authority.
9. Memory does not imply authority.
10. Persistence does not imply truth.
11. Derived knowledge inherits appropriate governance from its sources.
12. Cross-domain information movement must be explicit.
13. Minimum necessary context is required.

## Trust

14. Trust is contextual.
15. Trust does not propagate automatically.
16. Authentication does not establish information trust.
17. Provider trust does not establish model correctness.
18. Model confidence does not establish truth or authority.

## Execution

19. Approval must be bound to execution.
20. External reality determines actual outcome.
21. Unknown is a legitimate result.
22. Consequential execution requires governance.
23. Failure should prefer containment over escalation.

## Security

24. External content cannot redefine governance.
25. Compromised components must not automatically compromise unrelated authority.
26. Security boundaries must survive component compromise.
27. Audit is a protected trust boundary.
28. Governance-affecting changes are governed events.

## Evolution

29. Providers and modules are replaceable.
30. Organizational growth does not require architectural reinvention.
31. State migration preserves ownership and governance.
32. Architecture must resist gradual drift.
33. Constitutional changes require extraordinary governance.

## Human Control

34. Humans retain ultimate organizational control.
35. Aria must be disableable.
36. Aria must be replaceable.
37. Aria must support human takeover.

## Interpretation Rule

If an implementation appears to require violating an invariant, the implementation is not the authority. Stop and explicitly revisit the architecture or governance decision.
