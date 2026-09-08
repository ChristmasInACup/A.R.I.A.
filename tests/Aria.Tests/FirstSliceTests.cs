using Aria.Domain;
using Aria.Governance;
using Xunit;

namespace Aria.Tests;

public sealed class FirstSliceTests
{
    [Fact]
    public void Identity_alone_does_not_authorize()
    {
        var processor = CreateProcessor(
            new AuthorizationDecision(false, "No authority granted."));

        Assert.Throws<UnauthorizedAccessException>(() => processor.Process(Request(), "review information"));
    }

    [Fact]
    public void Unresolved_identity_cannot_become_authorized()
    {
        var processor = CreateProcessor(
            AuthorizationDecision.Allow("Would otherwise allow."),
            IdentityContext.Unresolved());

        Assert.Throws<UnauthorizedAccessException>(() => processor.Process(Request(), "review information"));
    }

    [Fact]
    public void Identity_mismatch_is_denied_even_when_authorization_would_allow()
    {
        var processor = CreateProcessor(
            AuthorizationDecision.Allow("Allowed."),
            IdentityContext.Resolved(new SubjectId("different-subject")));

        Assert.Throws<UnauthorizedAccessException>(() => processor.Process(Request(), "review information"));
    }

    [Fact]
    public void Authorized_request_produces_proposal_with_authorized_context()
    {
        var processor = CreateProcessor(
            AuthorizationDecision.Allow("Explicitly authorized."));

        var proposal = processor.Process(Request(), "review information");

        Assert.Equal(new SubjectId("subject-1"), proposal.Subject);
        Assert.Equal(new DomainId("domain-1"), proposal.Domain);
        Assert.Equal("review", proposal.Context.Values["purpose"]);
        Assert.Equal("review information", proposal.Intent);
    }

    [Fact]
    public void Authorization_is_evaluated_before_context_is_assembled()
    {
        var processor = new GovernedRequestProcessor(
            new StubIdentityResolver(IdentityContext.Resolved(new SubjectId("subject-1"))),
            new StubAuthorizationEvaluator(AuthorizationDecision.Deny("Denied.")),
            new ThrowingContextAssembler());

        Assert.Throws<UnauthorizedAccessException>(() => processor.Process(Request(), "review information"));
    }

    private static GovernedRequest Request() =>
        GovernedRequest.Create(
            new SubjectId("subject-1"),
            new DomainId("domain-1"),
            "review");

    private static GovernedRequestProcessor CreateProcessor(
        AuthorizationDecision authorization,
        IdentityContext? identity = null)
    {
        var resolvedIdentity = identity ?? IdentityContext.Resolved(new SubjectId("subject-1"));

        return new GovernedRequestProcessor(
            new StubIdentityResolver(resolvedIdentity),
            new StubAuthorizationEvaluator(authorization),
            new StubContextAssembler());
    }

    private sealed class StubIdentityResolver(IdentityContext identity) : IIdentityResolver
    {
        public IdentityContext Resolve(GovernedRequest request) => identity;
    }

    private sealed class StubAuthorizationEvaluator(AuthorizationDecision decision) : IAuthorizationEvaluator
    {
        public AuthorizationDecision Evaluate(AuthorizationRequest request) => decision;
    }

    private sealed class StubContextAssembler : IContextAssembler
    {
        public AuthorizedContext Assemble(GovernedRequest request, AuthorizationDecision authorization) =>
            AuthorizedContext.Create(
                request.Subject,
                request.Domain,
                request.Purpose,
                new Dictionary<string, string>
                {
                    ["purpose"] = request.Purpose
                });
    }

    private sealed class ThrowingContextAssembler : IContextAssembler
    {
        public AuthorizedContext Assemble(GovernedRequest request, AuthorizationDecision authorization) =>
            throw new InvalidOperationException("Context must not be assembled after authorization denial.");
    }
}
