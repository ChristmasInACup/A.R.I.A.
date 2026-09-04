# A.R.I.A. Implementation Charter

**Status:** Draft — Implementation Phase v1.0  
**Applies to:** A.R.I.A. implementation work  
**Authority:** Derived from the A.R.I.A. Constitution and Conceptual Architecture v1.0

## 1. Purpose

This charter defines how A.R.I.A. moves from its frozen conceptual architecture into an implementation without creating architectural, security, knowledge, or technical debt.

The charter governs engineering work performed by humans and AI coding agents. It does not replace or modify the Constitution, conceptual architecture, security model, or architectural invariants.

## 2. Governing Hierarchy

Implementation follows this hierarchy:

```text
Constitution
    ↓
Conceptual Architecture
    ↓
Implementation Architecture
    ↓
Specifications
    ↓
Epics
    ↓
Stories
    ↓
Tasks
    ↓
Code + Tests
```

Code is evidence of the architecture, not the authority that defines it.

If implementation appears to require violating an architectural invariant, implementation must stop and the architectural question must be explicitly reviewed.

## 3. Implementation Goals

A.R.I.A. implementation should:

- Preserve all constitutional invariants.
- Preserve logical security and authority boundaries.
- Make architectural intent visible in the structure of the system.
- Prefer simple, understandable solutions over unnecessary sophistication.
- Make important behavior explicit rather than implicit.
- Keep components small, focused, replaceable, and testable.
- Make future change easier rather than harder.
- Establish executable evidence for important architectural guarantees.
- Remain independent of any particular AI provider or model.
- Support controlled evolution without architectural reinvention.

## 4. Non-Goals

The implementation phase does not initially attempt to:

- Build every capability A.R.I.A. may eventually support.
- Optimize prematurely for scale, performance, or infrastructure complexity.
- Select a programming language before the implementation architecture justifies that decision.
- Bind foundational architecture to a specific AI provider.
- Treat an AI model as an authority or security boundary.
- Build autonomous behavior merely because it is technically possible.
- Replace architectural reasoning with framework conventions.

Technology and language choices will be evaluated separately after the language-agnostic implementation architecture is established.

## 5. Engineering Principles

### 5.1 Correctness Over Convenience

Correct behavior and preservation of architectural boundaries take precedence over implementation convenience.

### 5.2 Clarity Over Cleverness

Prefer code that another engineer can understand quickly over code that demonstrates technical cleverness.

If a straightforward solution is slightly longer but materially easier to understand, prefer the straightforward solution.

### 5.3 Simplicity Over Complexity

Use the smallest design that correctly satisfies the requirement.

Complexity must have a reason. Complexity introduced without a demonstrated requirement is technical debt.

### 5.4 Explicit Over Implicit

Important behavior should be visible in code and interfaces rather than depending on hidden conventions, surprising side effects, or implicit authority.

### 5.5 Readability Is a Requirement

Code must be readable enough that its purpose, responsibilities, dependencies, and important security properties can be understood without reconstructing hidden assumptions.

Prefer:

- descriptive names
- focused functions and methods
- clear control flow
- small modules
- explicit dependencies
- obvious boundaries
- limited nesting
- minimal global state
- useful comments where intent is not obvious

Comments should explain **why**, not restate obvious code.

### 5.6 Small, Focused Components

A component should have a clear responsibility and a reason to change.

Avoid:

- god classes
- god modules
- oversized functions or methods
- unrelated responsibilities grouped together
- hidden coupling
- excessive inheritance hierarchies
- global mutable state

Prefer composition and focused interfaces where they improve clarity and replaceability.

### 5.7 Design for Change

Boundaries should make change local whenever practical.

A change to one provider, capability, policy, or module should not require unnecessary changes throughout the system.

A useful review question is:

> If this part needs to change six months from now, how much of the system will have to change with it?

### 5.8 Make the Right Thing Easy and the Wrong Thing Difficult

System boundaries should naturally encourage correct behavior and make unsafe behavior difficult to express accidentally.

Security should not depend solely on developers remembering a rule.

### 5.9 Testability Is a Design Requirement

Important behavior must be testable independently of unrelated behavior.

Architectural invariants should become executable tests wherever practical.

### 5.10 Security by Default

The default behavior should be the safer behavior.

Do not require callers, modules, providers, or agents to opt into basic security boundaries accidentally.

### 5.11 No Premature Optimization

Optimize when evidence demonstrates a meaningful requirement.

Performance must not be used as a justification for weakening authorization, provenance, accountability, isolation, or correctness.

## 6. Documentation and Code Comments

Documentation should preserve knowledge that would otherwise be lost from the codebase.

Use documentation for:

- architectural intent
- non-obvious security constraints
- important tradeoffs
- operational assumptions
- durable decisions
- reasons behind unusual implementation choices

Use ADRs for durable architectural decisions.

Use code comments sparingly and intentionally. Comments must remain accurate as the implementation changes.

## 7. AI Engineering Contract

AI coding agents, including Codex, are engineering assistants operating under A.R.I.A.'s governance model.

They may:

- inspect the repository
- analyze existing implementation
- implement explicitly authorized stories
- create or update tests
- identify defects and inconsistencies
- propose design improvements
- create documentation
- prepare branches and pull requests when authorized

They may not:

- modify the Constitution without explicit human authorization
- silently change architectural invariants
- weaken security to make an implementation or test pass
- invent requirements
- create authority through implementation convenience
- bypass authorization or approval boundaries
- remove or weaken tests merely because they are inconvenient
- silently expand a story's scope
- treat model output as authoritative
- make a provider a security or authorization boundary
- merge consequential changes without the required human review

### Conflict Rule

If an agent discovers that a story, specification, or implementation conflicts with the Constitution or conceptual architecture, it must stop at the conflict, explain it, and request resolution.

The agent must not resolve the conflict by silently changing the architecture.

## 8. Story-Driven Development

Implementation work should be expressed as bounded stories before significant coding begins.

A story should identify:

- objective
- architectural references
- scope
- preconditions
- expected behavior
- acceptance criteria
- security requirements
- failure behavior
- required tests
- dependencies
- explicit non-goals
- definition of done

Stories should be small enough for a reviewer to understand the complete change.

## 9. Development Workflow

The preferred workflow is:

```text
Issue / Need
    ↓
Design
    ↓
ADR if durable architectural decision
    ↓
Specification
    ↓
Story
    ↓
Implementation
    ↓
Tests
    ↓
Security / Architecture Review
    ↓
Pull Request
    ↓
Human Review
    ↓
Merge
```

AI agents may accelerate the workflow, but they do not remove the governance steps.

## 10. Pull Request Expectations

A pull request should make it possible to determine:

- what changed
- why it changed
- which story authorized the work
- which architectural elements are affected
- which invariants are relevant
- what security implications exist
- what tests demonstrate correctness
- what remains intentionally out of scope

Prefer small, reviewable pull requests over large batches of unrelated work.

## 11. Architectural Traceability

Important implementation work should be traceable from architecture to executable evidence:

```text
Invariant
   ↓
Architecture
   ↓
Specification
   ↓
Story
   ↓
Implementation
   ↓
Test
```

The purpose is not bureaucracy. The purpose is to prevent architectural intent from disappearing as the codebase grows.

## 12. Invariant Testing

Where practical, constitutional and architectural invariants should be represented by automated tests.

Examples include:

- reasoning cannot create authority
- capability cannot create authority
- identity does not imply authorization
- cross-domain access requires authorization
- memory cannot grant authority
- providers cannot modify policy
- approval is bound to execution
- unauthorized execution is rejected
- unknown execution outcomes remain unknown
- compromised components cannot implicitly gain unrelated authority

Tests should verify both permitted and forbidden behavior where practical.

## 13. Failure and Degraded Operation

Implementation must preserve A.R.I.A.'s failure philosophy:

- uncertainty must remain uncertainty
- lack of information must not become fabricated information
- lack of authority must not become assumed authority
- provider failure should degrade capability rather than weaken governance
- external failure should preserve uncertainty
- security compromise should trigger containment rather than capability expansion
- unavailable governance should fail closed for consequential actions

## 14. Definition of Done

A story is complete only when:

- [ ] The authorized behavior is implemented.
- [ ] Acceptance criteria are satisfied.
- [ ] Relevant architectural invariants are preserved.
- [ ] Appropriate tests exist and pass.
- [ ] Security implications have been considered.
- [ ] Error and unknown-result behavior is defined and tested where applicable.
- [ ] Documentation is updated when durable knowledge changed.
- [ ] Architectural decisions are captured in an ADR when required.
- [ ] The implementation remains within story scope.
- [ ] The code is readable and appropriately modular.
- [ ] The pull request clearly explains the change.
- [ ] Required human review is complete.

## 15. Review Heuristics

Reviewers should ask:

1. Is this the simplest correct design?
2. Is the code easy to read?
3. Are responsibilities clearly separated?
4. Are boundaries explicit?
5. Can this component be changed without unnecessary collateral changes?
6. Does the implementation preserve authority boundaries?
7. Could an untrusted input, provider, module, or model influence governance improperly?
8. Is important behavior testable?
9. Are failure and unknown states represented honestly?
10. Did the implementation introduce architectural knowledge that needs documentation?

A useful component sanity check is:

> Can its purpose be explained in two sentences, is its responsibility singular, and are its dependencies and boundaries obvious?

If not, the design should be reconsidered before merge.

## 16. Evolution Rule

Implementation may evolve continuously while the foundational architecture remains stable.

New technology, providers, modules, organizational requirements, and capabilities may be introduced without weakening the Constitution.

When implementation pressure conflicts with an invariant, the correct response is architectural review—not silent erosion of the boundary.

## 17. Guiding Principle

> **Build the smallest thing that proves the architecture, make the code understandable, make unsafe behavior difficult, and preserve the ability to change what we have built.**
