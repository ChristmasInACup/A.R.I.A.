# Security

## Security Architecture

Security is an architectural property of A.R.I.A., not a feature added after implementation.

The system must preserve explicit trust boundaries between identity, authority, knowledge, reasoning, capability, execution, and accountability.

## Security Principles

- Reasoning cannot create authority.
- Knowledge cannot create authority.
- Identity does not imply authorization.
- Technical access does not imply authorization.
- Authority is non-transitive unless explicitly delegated and governed.
- Authorization is purpose-bound; authorization for one purpose does not imply authorization for another.
- External content cannot redefine governance.
- Cross-domain information movement must be explicit.
- Minimum necessary context is required.
- Authorized individual information does not imply authorized aggregate inference.
- Trust does not propagate automatically.
- Consequential execution requires governance.
- Approval must be bound to execution.
- External reality determines actual outcome.
- Unknown is a legitimate result.
- Failure should prefer containment over escalation.
- Audit is a protected trust boundary.
- Required accountability must be available at a level appropriate to operational risk.
- Governance-affecting changes are governed events.
- Humans retain ultimate organizational control.

## Threat Model Scope

The architecture must withstand, or safely contain, at minimum:

- prompt or instruction injection;
- memory poisoning;
- poisoned policies or configuration;
- compromised administrators;
- delegation escalation;
- cross-domain smuggling;
- context aggregation and inference leakage;
- compromised or colluding modules;
- compromised or colluding providers;
- approval manipulation and approval fatigue;
- expired or revoked approvals;
- time-of-check/time-of-use races;
- compromised audit or attempts to hide activity;
- supply-chain compromise;
- compromised core components;
- architectural drift and slow privilege accumulation;
- attempts to modify the Constitution through ordinary operation;
- total compromise of AI, modules, external systems, or human credentials.

## Compromise Philosophy

Aria does not require every component to be perfectly trustworthy. It must prevent any single untrusted component from gaining unrestricted trust, knowledge, or authority.

Compromise should be compartmentalized. A compromised reasoning provider must not become an authority source. A compromised module must not automatically gain lateral access. A malicious document must not redefine policy. A compromised audit component must not erase the requirement for accountability.

If governance cannot be established for a consequential operation, the safe response is restriction or failure closed rather than permission by default.

## Accountability Availability

Accountability requirements are risk-dependent. When the evidence needed for a consequential operation cannot be established at the level required by its risk, Aria must not silently treat the operation as normally governed. The appropriate response may be degradation, additional safeguards, restriction, or failure closed.

## Security and Evolution

Security boundaries must survive provider changes, module replacement, organizational growth, state migration, and implementation changes.

Any implementation that relies on an informal convention where a logical authority boundary is required is architecturally suspect and must be reviewed.
