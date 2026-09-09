# A.R.I.A. Autonomous Implementation Protocol

**Status:** Proposed — Implementation Operating Mode v1.0  
**Applies to:** Autonomous implementation of the approved A.R.I.A. backlog  
**Authority:** Derived from the A.R.I.A. Constitution, architectural invariants, implementation architecture, specifications, and Implementation Charter

## 1. Purpose

This protocol changes the **operating mode** of implementation work from task-by-task human orchestration to controlled autonomous execution.

The objective is simple:

> **Build → Test → Fix → Continue until the authorized implementation scope is complete or a governance boundary requires human intervention.**

The protocol does not grant an AI coding agent architectural authority. It changes how already-authorized implementation work is executed.

The human owner remains the final authority and final release gate.

## 2. Operating Model

The implementation backlog remains the source of authorized work.

The autonomous agent may progress through eligible work without waiting for human approval after every task.

```text
Approved Architecture
        ↓
Approved Specifications
        ↓
Capabilities
        ↓
Epics
        ↓
Stories
        ↓
Tasks
        ↓
Autonomous Execution Loop
        ↓
Build
        ↓
Test
        ↓
Fix
        ↓
Retest
        ↓
Validate Acceptance Criteria
        ↓
Continue to Next Eligible Task
        ↓
Milestone Validation
        ↓
Full System Validation
        ↓
Final Review
        ↓
Human Release Decision
```

The agent is expected to keep moving when the next action is already authorized and objectively defined.

## 3. Human Control Model

Human control is concentrated at meaningful governance boundaries rather than every implementation step.

### Human owner controls

The human owner retains exclusive authority to:

- approve or change foundational architecture
- approve changes to architectural invariants
- approve changes to requirements or scope at the capability/epic level
- resolve architectural conflicts
- authorize exceptions to this protocol
- approve final release or production readiness
- merge the consequential final implementation into the protected release branch

### Autonomous agent controls

Within an approved scope, the agent may:

- select the next eligible task
- create implementation branches
- implement code
- create and modify tests
- refactor implementation when required for correctness or maintainability
- run builds and test suites
- diagnose failures
- make corrective changes
- repeat the build/test/fix cycle
- update implementation documentation
- create intermediate commits and pull requests where useful
- maintain traceability from implementation to backlog items
- continue to the next eligible task

The agent must stop when it reaches a governance boundary, not merely when it encounters a difficult technical problem.

## 4. The Autonomous Build Loop

For every eligible task, the agent follows this loop:

```text
1. Read task
2. Read parent story/epic
3. Read linked specifications and invariants
4. Inspect existing implementation
5. Plan the smallest correct change
6. Implement
7. Add or update tests
8. Build
9. Run relevant tests
10. Run broader regression tests
11. Run static analysis / quality checks when configured
12. If failure → diagnose → fix → return to step 8
13. Validate acceptance criteria
14. Validate architectural constraints
15. Record evidence
16. Mark task complete
17. Select next eligible task
```

A successful test run is necessary but is not sufficient for completion.

## 5. Autonomous Progress Rules

The agent may continue automatically when all of the following are true:

- the work is within an approved capability, epic, story, or task
- acceptance criteria are explicit enough to evaluate
- dependencies are satisfied
- no architectural conflict has been discovered
- no new authority is being created
- security boundaries are preserved
- tests can provide meaningful evidence
- the next action does not require a product or architectural decision

The agent should prefer the smallest correct implementation and should not expand scope merely because additional improvements are technically possible.

## 6. Self-Correction Rules

The agent is expected to fix ordinary implementation failures autonomously.

Examples include:

- compilation errors
- failing unit tests
- failing integration tests
- incorrect test expectations caused by the implementation
- ordinary defects discovered during validation
- formatting or static-analysis failures
- missing test coverage required by the story
- straightforward compatibility issues

The agent may iterate repeatedly until the failure is resolved or a stop condition is reached.

The agent must not resolve a failure by weakening governance, deleting meaningful tests, hiding errors, falsifying evidence, or changing requirements.

## 7. Mandatory Stop Conditions

The agent must stop and request human resolution when it encounters any of the following:

### Architecture conflict

The implementation appears to require violating an invariant, changing a foundational boundary, or contradicting approved architecture.

### Requirement ambiguity with material consequences

Multiple interpretations would materially change behavior, authority, security, user outcome, or scope.

### New architectural decision

The implementation requires a durable architectural choice not already covered by approved architecture or an existing ADR.

### Security boundary conflict

The easiest implementation weakens authorization, isolation, provenance, containment, accountability, approval, or another security property.

### Scope expansion

Completing the work requires functionality outside the authorized backlog scope.

### Evidence failure

The agent cannot establish credible evidence that a required architectural guarantee is preserved.

### Irreconcilable dependency

A required dependency is unavailable, contradictory, or blocked by an unresolved governance decision.

### Unsafe uncertainty

The agent cannot determine whether an action is safe, authorized, or consistent with requirements.

A stop should include:

1. what was being attempted
2. what was discovered
3. why autonomous resolution is not appropriate
4. the relevant architectural/specification references
5. the smallest decision required from the human owner

## 8. Backlog Execution Strategy

The backlog should be ordered by dependency and architectural proof value rather than by convenience.

The agent should generally prefer:

1. foundational infrastructure required by multiple stories
2. boundaries that establish security or authority guarantees
3. thin vertical slices that prove end-to-end architecture
4. core behavior
5. broader capabilities
6. hardening, resilience, observability, and operational improvements
7. final integration and release validation

A task is not blocked merely because a later task has not been designed yet. The agent should continue whenever the current task has sufficient authorization and acceptance criteria.

## 9. Vertical Slice Progression

A.R.I.A. should not be implemented as disconnected layers for an extended period.

Where practical, autonomous execution should produce increasingly complete vertical slices.

The reference implementation begins with the existing governed request path:

```text
Request
  → Identity
  → Domain
  → Authorization
  → Minimum-Necessary Context
  → Proposal
```

Later milestones extend this toward:

```text
Request
  → Identity
  → Domain
  → Authorization
  → Context
  → Reasoning
  → Proposal
  → Governance
  → Approval / Autonomy
  → Execution
  → External Result
  → Accountability
```

Each milestone should leave the system in a buildable and testable state.

## 10. Testing Strategy

Testing is continuous, not a final-stage activity.

The agent should progressively maintain evidence at several levels:

### Unit tests

Verify focused component behavior.

### Story tests

Verify the acceptance criteria of the current story, including relevant permitted and forbidden paths.

### Specification tests

Verify architectural boundary guarantees.

### Invariant tests

Verify foundational truths such as:

- identity does not imply authorization
- reasoning cannot create authority
- capability cannot create authority
- knowledge cannot create authority
- memory cannot create authority
- cross-domain access requires authorization
- minimum-necessary context is enforced
- approval is bound to consequential execution
- revoked or expired authority cannot authorize consequential action
- provider output cannot modify governance
- unknown external outcomes remain unknown

### Regression tests

Previously passing behavior must remain passing as implementation expands.

### System validation

At milestone and final stages, the entire implemented system is tested together.

## 11. Definition of Done — Autonomous Mode

A task is complete when:

- [ ] Authorized behavior is implemented.
- [ ] Acceptance criteria are satisfied.
- [ ] Relevant invariants remain satisfied.
- [ ] Required tests exist and pass.
- [ ] Relevant regression tests pass.
- [ ] Security implications have been considered.
- [ ] Failure and unknown-result behavior is handled correctly.
- [ ] Required documentation is updated.
- [ ] Required ADRs exist.
- [ ] Work remains within scope.
- [ ] Code is readable and appropriately modular.
- [ ] Evidence is recorded where required.
- [ ] The implementation is buildable.

**Human review is not required to advance from one ordinary task to the next.**

Human review is required at the defined milestone and final governance gates.

## 12. Milestones

The implementation plan should group work into meaningful milestones rather than requiring human approval after every task.

A milestone should have:

- a clear objective
- a bounded set of epics/stories
- explicit exit criteria
- required invariant/specification tests
- system-level validation where applicable
- evidence summary
- known limitations

The agent may execute all eligible work within a milestone continuously.

At milestone completion, the agent should produce a concise implementation report before proceeding if the milestone contains a governance boundary or explicitly requires human review.

## 13. Pull Request Strategy

Pull requests are implementation artifacts, not mandatory human synchronization points for every task.

The agent may use:

- one PR per meaningful milestone
- a small set of PRs within a milestone when repository mechanics require it
- intermediate PRs for recoverability or CI purposes

PRs should preserve traceability to the relevant backlog and provide evidence of validation.

The protected release branch remains subject to repository governance and human ownership.

The agent must not merge the final consequential implementation without the required human release decision.

## 14. Recovery and Checkpointing

Autonomous execution must remain recoverable.

The agent should commit coherent progress at useful checkpoints so that:

- work can be resumed after interruption
- failures can be isolated
- regressions can be identified
- completed milestones can be reconstructed
- the human owner can inspect history if necessary

A checkpoint does not imply that the human must review it immediately.

## 15. Traceability and Evidence

Autonomous execution must preserve the existing traceability chain:

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
Task
   ↓
Implementation
   ↓
Test
   ↓
Evidence
```

The agent should never mark work complete solely because code exists or tests happen to pass.

## 16. Final Completion Gate

The implementation is not considered complete merely because the backlog is exhausted.

Before presenting the system for final human review, the agent must perform a final validation pass covering, as applicable:

- complete build
- complete automated test suite
- invariant tests
- specification tests
- integration tests
- system/end-to-end tests
- security validation
- failure/degraded-mode validation
- traceability validation
- documentation consistency
- architecture drift review
- dependency and configuration review
- known limitations and unresolved risks

The final report should state clearly:

- what was implemented
- what was tested
- what passed
- what remains uncertain
- known limitations
- deferred work
- architectural decisions made under existing authority
- any conditions that prevent release

The human owner then makes the final release decision.

## 17. Final Principle

> **The agent owns the implementation loop. The human owns the authority to define, change, approve, and ultimately release the system.**
