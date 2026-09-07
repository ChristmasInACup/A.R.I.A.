# A.R.I.A. Organizational Profiles Architecture

**Status:** Architectural Baseline — Draft for Review

> **Purpose:** Support organizations of different size, structure, risk, and regulatory burden without creating separate architectures.  
> **What done looks like:** Organizational profiles change configuration and control depth while preserving the same constitutional, authority, security, trust, and human-control boundaries.

## 1. Purpose

A.R.I.A. must support organizations of different size, structure, risk, and regulatory burden without creating separate architectures for each market.

Profiles change governance configuration and operational scale. They do not change foundational authority and security boundaries.

## 2. Profiles

### Small Business

Simple organizational structure, limited delegation, relatively small capability surface, and proportionate accountability.

### Growing Business

More roles, teams, domains, delegated authority, workflows, and separation of responsibilities.

### Enterprise

Multiple organizational units, complex policy hierarchies, extensive delegation, stronger separation of duties, provider controls, and formal lifecycle management.

### High-Risk / Highly Regulated

Stricter information handling, approval, audit, retention, provider, execution, validation, and human-control requirements.

## 3. What Profiles May Change

Profiles may configure:

- organizational structure;
- roles and delegation;
- domains;
- sensitivity classifications;
- policies;
- approval requirements;
- autonomy limits;
- capabilities;
- provider restrictions;
- audit requirements;
- retention;
- separation of duties;
- risk thresholds;
- execution restrictions.

## 4. What Profiles May Not Change

A profile may not redefine constitutional or invariant boundaries.

In particular, no profile may permit:

- reasoning to create authority;
- knowledge to create authority;
- identity to imply authorization;
- technical reachability to bypass authorization;
- consequential execution without required governance;
- removal of human control where the architecture requires it;
- compromise of one domain to automatically grant unrelated authority.

## 5. Growth Model

Organizational growth should primarily increase configuration, scale, and governance sophistication rather than require architectural reinvention.

```text
Same Core Architecture
        ↓
Different Governance Configuration
        ↓
Different Operational Scale
        ↓
Different Risk Controls
```

## 6. Multi-Domain Organizations

Business units, departments, teams, clients, projects, and other domains remain explicit boundaries. Information and authority do not become globally available merely because an organization has grown.

Cross-domain operations require explicit authorization, minimum-necessary context, purpose limitation, and appropriate accountability.

## 7. What Done Looks Like

Before an organizational profile is treated as implementation-ready, verify:

- the profile maps to the same core architecture;
- roles, domains, policies, and delegation are explicit;
- risk and control depth are appropriate to the profile;
- profile configuration cannot redefine constitutional or invariant boundaries;
- cross-domain information and authority movement remains explicit;
- growth can be handled through configuration and scale rather than architectural reinvention;
- human control and accountability remain intact.

## 8. Architectural Rule

> **Scale changes configuration and control depth; it does not change the fundamental trust boundaries of A.R.I.A.**
