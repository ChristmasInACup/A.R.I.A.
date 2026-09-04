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
7. Authority is non-transitive unless explicitly delegated and governed.
8. Authorization must be valid at consequential use.
9. Authorization for one purpose does not imply authorization for another purpose.

## Knowledge

10. Knowledge does not imply authority.
11. Memory does not imply authority.
12. Persistence does not imply truth.
13. Derived knowledge inherits appropriate governance from its sources.
14. Cross-domain information movement must be explicit.
15. Minimum necessary context is required.
16. Authorized individual information does not imply authorized aggregate inference.

## Trust

17. Trust is contextual.
18. Trust does not propagate automatically.
19. Authentication does not establish information trust.
20. Provider trust does not establish model correctness.
21. Model confidence does not establish truth or authority.

## Execution

22. Approval must be bound to execution.
23. External reality determines actual outcome.
24. Unknown is a legitimate result.
25. Consequential execution requires governance.
26. Failure should prefer containment over escalation.

## Security

27. External content cannot redefine governance.
28. Compromised components must not automatically compromise unrelated authority.
29. Security boundaries must survive component compromise.
30. Audit is a protected trust boundary.
31. Governance-affecting changes are governed events.
32. Required accountability must be available at a level appropriate to the operational risk.

## Evolution

33. Providers and modules are replaceable.
34. Organizational growth does not require architectural reinvention.
35. State migration preserves ownership and governance.
36. Architecture must resist gradual drift.
37. Constitutional changes require extraordinary governance.

## Human Control

38. Humans retain ultimate organizational control.
39. Aria must be disableable.
40. Aria must be replaceable.
41. Aria must support human takeover.

## Interpretation Rule

If an implementation appears to require violating an invariant, the implementation is not the authority. Stop and explicitly revisit the architecture or governance decision.
