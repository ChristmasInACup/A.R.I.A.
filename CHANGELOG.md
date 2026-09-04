# Changelog

All notable changes to A.R.I.A. are documented here.

A.R.I.A. follows a documentation-first, architecture-first development model.
Changes are categorized by their impact on the Constitution, architecture,
governance, security, and implementation.

## [Unreleased]

### Planned

- Implementation architecture
- Initial architecture specifications
- Initial executable representations of architectural invariants

---

## [1.0.0] — 2026-09-04

### Added

- A.R.I.A. Constitution
- 41 foundational architectural invariants
- Conceptual Architecture v1.0
- Security architecture and threat model
- Authority and Knowledge Plane model
- Trust and accountability model
- Autonomy and human-approval model
- Provider and model governance
- Module and capability governance
- Organizational profile model
- Architecture contribution guidelines
- ADR guidance

### Architectural Baseline

A.R.I.A. v1.0 establishes the foundational governance model:

- Reasoning cannot create authority.
- Knowledge cannot create authority.
- Capability cannot create authority.
- Identity does not imply authorization.
- Cross-domain access requires explicit authorization.
- Consequential execution requires governance.
- Human beings retain ultimate organizational control.
- A.R.I.A. must remain disableable, replaceable, and recoverable.
- AI providers and models are replaceable reasoning resources, not authority boundaries.

### Security

Established the foundational security model around:

- Explicit trust boundaries
- Least privilege
- Minimum necessary context
- Authority non-transitivity
- Domain isolation
- Bounded autonomy
- Protected accountability
- Containment-first failure handling
- Human takeover and recovery

### Status

**Constitution:** Frozen  
**Conceptual Architecture:** Frozen — v1.0  
**Security Model:** Frozen — v1.0  
**Governance Model:** Frozen — v1.0  
**Implementation Architecture:** Not yet established
