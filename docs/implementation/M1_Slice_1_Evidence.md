# M1 Slice 1 Evidence — Governed Request to Proposal

**Scope:** CAP-01, CAP-02, CAP-05, CAP-07; EPIC-02 through EPIC-05; tasks T-02.1.1–T-05.2.6.

## Implemented boundary

`GovernedRequestProcessor` admits a proposal only after resolved, matching identity; an explicit policy-and-effective-authority decision; and successful minimum-necessary context assembly. Identity, policy, authority, context material, and proposal are distinct domain values. No proposal, context material, or identity value can grant authority.

`EffectiveAuthorityEvaluator` fails closed for missing, denying, unavailable, expired, revoked, out-of-scope, or invalidly delegated authority. `MinimumNecessaryContextAssembler` selects material only when its domain, purpose, and resource match the authorized request, retaining sensitivity, provenance, and uncertainty. `GovernedProposal` is a draft recommendation with explicit scope and uncertainty.

## Executable evidence

`tests/Aria.Tests/FirstSliceTests.cs` covers the permitted end-to-end path and denied paths for identity state, policy availability/denial, lifecycle, delegation, purpose/domain/scope mismatch, authorization ordering, context minimization, provenance/sensitivity/uncertainty retention, proposal non-authority, and changed authorization conditions.

On 2026-09-09, `dotnet test Aria.sln --configuration Release` passed **15/15** under the repository's native `net10.0` target. The test command restored, built, and executed the complete solution suite.

## Non-goals and limitations

This in-memory reference slice introduces no persistence, production authentication, provider/reasoning integration, approval/autonomy, execution, API/UI, or external side effects. Policy precedence is intentionally limited to deny-overrides-allow; broader policy precedence remains an open specification question. This evidence records validation facts and does not itself authorize release.
