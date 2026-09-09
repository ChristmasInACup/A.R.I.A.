# A.R.I.A. Implementation Charter

**Status:** Draft — Implementation Phase v1.0  
**Applies to:** A.R.I.A. implementation work  
**Authority:** Derived from the A.R.I.A. Constitution and Conceptual Architecture v1.0

## 1. Purpose

This charter defines how A.R.I.A. moves from its frozen conceptual architecture into an implementation without creating architectural, security, knowledge, or technical debt.

The charter governs engineering work performed by humans and AI coding agents. It does not replace or modify the Constitution, conceptual architecture, security model, or architectural invariants.

A.R.I.A. uses a controlled autonomous implementation model: once a scope of work is explicitly authorized, an AI engineering agent may execute the defined implementation plan continuously through build, test, diagnosis, repair, validation, and continuation without requiring human approval after every individual task. Human authority remains final at defined governance boundaries and at release acceptance.

## 2. Governing Hierarchy

Implementation follows this hierarchy:

```text
Constitution
    ↓
Invariants
    ↓
Conceptual Architecture
    ↓
Architecture Specifications
    ↓
Capabilities
    ↓
Epics
    ↓
Stories
    ↓
Tasks
    ↓
Implementation
    ↓
Tests
    ↓
Evidence
```

Each layer answers a different question:

- **Constitution:** What must never be violated?
- **Invariants:** What truths must always hold?
- **Conceptual Architecture:** How must the system fundamentally be organized?
- **Architecture Specifications:** What guarantees must each architectural boundary provide?
- **Capabilities:** What ability are we creating?
- **Epics:** What major body of work creates that capability?
- **Stories:** What bounded behavior are we implementing?
- **Tasks:** What concrete engineering work is required?
- **Implementation:** How is the behavior built?
- **Tests:** Does it behave correctly?
- **Evidence:** Can we demonstrate that the architectural guarantees remain satisfied?

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

### 5.12 Intent-First Human Interaction

A.R.I.A. should minimize the cognitive burden required to accomplish legitimate tasks.

Users should communicate goals and intent rather than having to understand or manually coordinate A.R.I.A.'s internal architecture, governance mechanisms, tools, models, memory systems, or execution pathways.

A.R.I.A. should be **intent-first, not mechanism-first**. The system should absorb operational, technical, and orchestration complexity wherever doing so is safe and consistent with established authority and user intent.

A.R.I.A. should:

- Prefer natural goals over procedural commands.
- Infer reasonable defaults when safe.
- Ask questions only when the answer materially affects outcome, authority, safety, or user intent.
- Ask the smallest useful clarification question when clarification is necessary.
- Avoid exposing internal complexity unless it is relevant to the user's decision or understanding.
- Explain consequential approvals in terms of the meaningful action and its consequences rather than internal implementation machinery.
- Preserve a clear path for inspection, correction, override, and human takeover.

Cognitive simplicity must never become concealment. A.R.I.A. must not hide material consequences, uncertainty, authorization requirements, conflicts, or meaningful opportunities for human control merely to make an interaction appear simpler.

A useful design test is:

> **Are we making the user operate A.R.I.A., or is A.R.I.A. operating for the user?**

If accomplishing a legitimate goal requires the user to understand or manually coordinate A.R.I.A.'s internal mechanisms, that should be treated as a potential architectural and interface smell.

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
- execute an authorized sequence of tasks continuously until the authorized scope is complete or a mandatory stop condition is reached
- build, test, diagnose, fix, retest, validate, record evidence, and continue without requiring per-task human approval

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
- merge protected release changes without the human authorization required by governance
- continue past a mandatory stop condition

### Normal Autonomous Loop

For authorized implementation work, the default operating loop is:

```text
Select authorized work
    ↓
Build / Implement
    ↓
Test
    ↓
Diagnose failures
    ↓
Fix
    ↓
Retest
    ↓
Validate architecture / security / scope
    ↓
Record evidence
    ↓
Continue to next authorized task
```

A passing test does not by itself authorize continuation. The agent must also verify that the work remains within the authorized scope and preserves applicable architectural, security, and governance constraints.

### Mandatory Stop Conditions

The agent must stop autonomous execution and request human resolution when:

- the Constitution or an architectural invariant must change
- a durable architectural decision is required that is not already authorized
- requirements or scope materially conflict or are ambiguous in a consequential way
- implementation would weaken security, authorization, isolation, provenance, accountability, or other constitutional guarantees
- required information or authority is unavailable
- a test, validation, or evidence requirement cannot be satisfied without changing an approved requirement or invariant
- the agent discovers a material defect in the governing specifications
- the work would materially expand the authorized capability beyond its approved scope
- release readiness requires a human decision

Stopping is a governance behavior, not a failure of the autonomous workflow.

### Conflict Rule

If an agent discovers that a story, specification, or implementation conflicts with the Constitution or conceptual architecture, it must stop at the conflict, explain it, and request resolution.

The agent must not resolve the conflict by silently changing the architecture.

## 8. Capability and Epic-Driven Planning

Significant implementation work should first be organized around an explicitly defined capability and its associated epics.

A capability should describe a coherent ability A.R.I.A. must provide without prematurely prescribing implementation details.

An epic should represent a meaningful body of work required to establish, extend, or harden that capability.

Epics should remain traceable to the relevant specifications and invariants. They should not introduce architectural authority that does not already exist in the approved architecture or specifications.

## 9. Story-Based Implementation

Stories are the primary bounded unit of implementation work, but they are not the primary unit of architecture.

A story should identify:

- objective
- architectural references
- capability and epic
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

Stories should be small enough for a reviewer to understand the complete change, while an authorized autonomous implementation run may execute multiple stories sequentially without requiring human approval between them.

Stories implement already-established architectural intent; they must not be used to invent architecture through incremental feature work.

## 10. Development Workflow

The preferred autonomous workflow is:

```text
Issue / Need
    ↓
Architectural / Product Design
    ↓
ADR if durable architectural decision
    ↓
Specification
    ↓
Capability
    ↓
Epic
    ↓
Stories / Tasks
    ↓
Authorized Autonomous Build Loop
    ↓
Build
    ↓
Test
    ↓
Diagnose / Fix
    ↓
Retest
    ↓
Architecture / Security / Scope Validation
    ↓
Evidence
    ↓
Milestone Validation
    ↓
Full System Validation
    ↓
Final Human Review
    ↓
Human Acceptance / Release Authorization
```

AI agents may execute the implementation loop continuously within the authorized scope. Human review is concentrated at meaningful governance and release boundaries rather than required after every individual task.

## 11. Pull Request Expectations

A pull request should make it possible to determine:

- what changed
- why it changed
- which capability, epic, and story authorized the work
- which architectural elements are affected
- which invariants are relevant
- what security implications exist
- what tests demonstrate correctness
- what evidence supports the relevant guarantees
- what remains intentionally out of scope
- what autonomous implementation scope was executed, where applicable
- what validation was performed before the pull request was presented for human review

A pull request may represent a task, story, milestone, or other coherent authorized unit of work. It does not need to correspond one-to-one with an individual task.

Prefer coherent, reviewable pull requests over arbitrary task-sized commits or batches of unrelated work.

## 12. Architectural Traceability

Important implementation work should be traceable from foundational principles to executable evidence:

```text
Constitution
   ↓
Invariant
   ↓
Architecture
   ↓
Specification
   ↓
Capability
   ↓
Epic
   ↓
Story
   ↓
Implementation
   ↓
Test
   ↓
Evidence
```

The purpose is not bureaucracy. The purpose is to prevent architectural intent from disappearing as the codebase grows and to make important guarantees demonstrable.

## 13. Layered Testing and Evidence

Testing should operate at multiple levels because different layers answer different questions.

### Invariant Tests

Verify that foundational truths cannot be violated.

Examples:

- reasoning cannot create authority
- capability cannot create authority
- identity does not imply authorization
- cross-domain access requires authorization
- memory cannot grant authority
- providers cannot modify policy

### Specification Tests

Verify that an architectural boundary provides its defined guarantees.

Examples include authorization scope, delegation limits, provenance preservation, context minimization, approval binding, and execution-governance behavior.

### Story Tests

Verify that the specific behavior authorized by a story works as intended, including relevant permitted and forbidden cases.

### Autonomous Regression Requirement

When an autonomous implementation run fixes a failing test or changes behavior in response to a failure, the agent must retest the affected behavior and run the relevant regression suite before continuing. Tests must not be removed, weakened, skipped, or made less meaningful solely to obtain a passing result.

Evidence should make it possible to demonstrate that important architectural guarantees remain satisfied rather than relying only on individual feature tests.

## 14. Failure and Degraded Operation

Implementation must preserve A.R.I.A.'s failure philosophy:

- uncertainty must remain uncertainty
- lack of information must not become fabricated information
- lack of authority must not become assumed authority
- provider failure should degrade capability rather than weaken governance
- external failure should preserve uncertainty
- security compromise should trigger containment rather than capability expansion
- unavailable governance should fail closed for consequential actions

## 15. Definition of Done

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
- [ ] Relevant evidence has been established where architectural guarantees require it.
- [ ] The pull request clearly explains the change.

Human approval is **not** a per-story Definition of Done requirement. Human review and acceptance occur at the governance, milestone, and release boundaries defined by this charter and the repository's protection rules.

## 16. Human Authority and Review Boundaries

The human owner remains the final authority over A.R.I.A.

Human review is required when the autonomous implementation process reaches a governance boundary, including:

1. constitutional or invariant changes
2. durable architectural decisions not already authorized
3. material changes to requirements or scope
4. unresolved security or authority conflicts
5. milestone or full-system validation requiring an acceptance decision
6. final release readiness and acceptance

The purpose of autonomous execution is to remove unnecessary approval latency, not to remove human authority.

The agent may prepare and validate changes continuously, but it does not become the final authority merely because it can implement or validate them.

## 17. Review Heuristics

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
11. Does the user experience minimize unnecessary cognitive burden?
12. Are we making the user operate A.R.I.A., or is A.R.I.A. operating for the user?
13. Did autonomous execution remain within its authorized scope?
14. Were failures fixed without weakening tests or governance?
15. Is the evidence sufficient to support milestone or release acceptance?

A useful component sanity check is:

> Can its purpose be explained in two sentences, is its responsibility singular, and are its dependencies and boundaries obvious?

If not, the design should be reconsidered before merge.

## 18. Evolution Rule

Implementation may evolve continuously while the foundational architecture remains stable.

New technology, providers, modules, organizational requirements, and capabilities may be introduced without weakening the Constitution.

When implementation pressure conflicts with an invariant, the correct response is architectural review—not silent erosion of the boundary.

## 19. Guiding Principle

> **Build the smallest thing that proves the architecture, let authorized automation build and validate continuously, make the code understandable, make unsafe behavior difficult, minimize the cognitive burden on the human, and preserve the human's final authority to accept or reject the result.**
