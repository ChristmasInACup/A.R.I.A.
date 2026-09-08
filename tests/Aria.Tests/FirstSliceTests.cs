using Aria.Domain;
using Aria.Governance;
using Xunit;

namespace Aria.Tests;

public sealed class FirstSliceTests
{
    [Fact]
    public void Identity_alone_does_not_authorize()
    {
        var processor = CreateProcessor(false, "No authority granted.");

        var result = processor.Process(Request(), "review information");

        Assert.False(result.IsAllowed);
        Assert.Null(result.Proposal);
        Assert.Equal("No authority granted.", result.Reason);
    }

    [Fact]
    public void Unresolved_identity_cannot_become_authorized()
    {
        var processor = CreateProcessor(true, "Would otherwise allow.", IdentityContext.Unresolved());

        var result = processor.Process(Request(), "review information");

        Assert.False(result.IsAllowed);
        Assert.Null(result.Proposal);
    }

    [Fact]
    public void Identity_mismatch_is_denied_even_when_authorization_would_allow()
    {
        var processor = CreateProcessor(
            true,
            "Allowed.",
            IdentityContext.Resolved(new SubjectId("different-subject")));

        var result = processor.Process(Request(), "review information");

        Assert.False(result.IsAllowed);
        Assert.Null(result.Proposal);
    }

    [Fact]
    public void Authorized_request_produces_proposal_with_authorized_context()
    {
        var processor = CreateProcessor(true, "Explicitly authorized.");

        var result = processor.Process(Request(), "review information");

        Assert.True(result.IsAllowed);
        Assert.NotNull(result.Proposal);
        Assert.Equal(new SubjectId("subject-1"), result.Proposal.Subject);
        Assert.Equal(new DomainId("domain-1"), result.Proposal.Domain);
        Assert.Equal("review", result.Proposal.Context.Values["purpose"]);
        Assert.Equal("review information", result.Proposal.Intent);
    }

    [Fact]
    public void Authorization_is_evaluated_before_context_is_assembled()
    {
        var processor = new GovernedRequestProcessor(
            new StubIdentityResolver(IdentityContext.Resolved(new SubjectId("subject-1"))),
            new StubAuthorizationEvaluator(false, "Denied."),
            new ThrowingContextAssembler());

        var result = processor.Process(Request(), "review information");

        Assert.False(result.IsAllowed);
        Assert.Equal("Denied.", result.Reason);
    }

    [Fact]
    public void Denied_authorization_cannot_create_an_authorization_grant()
    {
        var request = new AuthorizationRequest(
            new SubjectId("subject-1"),
            new DomainId("domain-1"),
            "review");
        var decision = AuthorizationDecision.Deny(request, "Denied.");

        Assert.False(decision.TryCreateGrant(out var grant));
        Assert.Null(grant);
    }

    [Fact]
    public void Authorized_context_contains_only_minimum_necessary_information()
    {
        var processor = new GovernedRequestProcessor(
            new StubIdentityResolver(IdentityContext.Resolved(new SubjectId("subject-1"))),
            new StubAuthorizationEvaluator(true, "Allowed."),
            new FilteringContextAssembler());

        var result = processor.Process(Request(), "review information");

        Assert.True(result.IsAllowed);
        Assert.NotNull(result.Proposal);
        Assert.Single(result.Proposal.Context.Values);
        Assert.Equal("review", result.Proposal.Context.Values["purpose"]);
        Assert.False(result.Proposal.Context.Values.ContainsKey("unnecessary-secret"));
    }

    [Fact]
    public void Authorized_context_is_not_affected_by_source_dictionary_changes()
    {
        var source = new Dictionary<string, string> { ["purpose"] = "review" };
        var processor = new GovernedRequestProcessor(
            new StubIdentityResolver(IdentityContext.Resolved(new SubjectId("subject-1"))),
            new StubAuthorizationEvaluator(true, "Allowed."),
            new DictionaryContextAssembler(source));

        var result = processor.Process(Request(), "review information");
        source["purpose"] = "tampered";

        Assert.Equal("review", result.Proposal!.Context.Values["purpose"]);
    }

    [Fact]
    public void Authorization_grant_cannot_be_reused_for_a_different_request()
    {
        var processor = new GovernedRequestProcessor(
            new StubIdentityResolver(IdentityContext.Resolved(new SubjectId("subject-1"))),
            new StubAuthorizationEvaluator(true, "Allowed."),
            new MismatchedContextAssembler());

        Assert.Throws<ArgumentException>(() => processor.Process(Request(), "review information"));
    }

    [Fact]
    public void Empty_subject_and_domain_ids_are_rejected()
    {
        Assert.Throws<ArgumentException>(() => new SubjectId(""));
        Assert.Throws<ArgumentException>(() => new DomainId("   "));
    }

    private static GovernedRequest Request() =>
        GovernedRequest.Create(
            new SubjectId("subject-1"),
            new DomainId("domain-1"),
            "review");

    private static GovernedRequestProcessor CreateProcessor(
        bool isAuthorized,
        string reason,
        IdentityContext? identity = null)
    {
        var resolvedIdentity = identity ?? IdentityContext.Resolved(new SubjectId("subject-1"));

        return new GovernedRequestProcessor(
            new StubIdentityResolver(resolvedIdentity),
            new StubAuthorizationEvaluator(isAuthorized, reason),
            new StubContextAssembler());
    }

    private sealed class StubIdentityResolver(IdentityContext identity) : IIdentityResolver
    {
        public IdentityContext Resolve(GovernedRequest request) => identity;
    }

    private sealed class StubAuthorizationEvaluator(bool isAuthorized, string reason) : IAuthorizationEvaluator
    {
        public AuthorizationDecision Evaluate(AuthorizationRequest request) =>
            isAuthorized
                ? AuthorizationDecision.Allow(request, reason)
                : AuthorizationDecision.Deny(request, reason);
    }

    private sealed class StubContextAssembler : IContextAssembler
    {
        public AuthorizedContext Assemble(GovernedRequest request, AuthorizationGrant authorization) =>
            AuthorizedContext.Create(
                request,
                authorization,
                new Dictionary<string, string>
                {
                    ["purpose"] = request.Purpose
                });
    }

    private sealed class FilteringContextAssembler : IContextAssembler
    {
        public AuthorizedContext Assemble(GovernedRequest request, AuthorizationGrant authorization) =>
            AuthorizedContext.Create(
                request,
                authorization,
                new Dictionary<string, string>
                {
                    ["purpose"] = request.Purpose
                });
    }

    private sealed class DictionaryContextAssembler(Dictionary<string, string> values) : IContextAssembler
    {
        public AuthorizedContext Assemble(GovernedRequest request, AuthorizationGrant authorization) =>
            AuthorizedContext.Create(request, authorization, values);
    }

    private sealed class MismatchedContextAssembler : IContextAssembler
    {
        public AuthorizedContext Assemble(GovernedRequest request, AuthorizationGrant authorization) =>
            AuthorizedContext.Create(
                GovernedRequest.Create(
                    new SubjectId("subject-2"),
                    request.Domain,
                    request.Purpose),
                authorization,
                new Dictionary<string, string> { ["purpose"] = request.Purpose });
    }

    private sealed class ThrowingContextAssembler : IContextAssembler
    {
        public AuthorizedContext Assemble(GovernedRequest request, AuthorizationGrant authorization) =>
            throw new InvalidOperationException("Context must not be assembled after authorization denial.");
    }
}
