using Aria.Domain;

namespace Aria.Governance;

public sealed class GovernedRequestProcessor
{
    private readonly IIdentityResolver _identityResolver;
    private readonly IAuthorizationEvaluator _authorizationEvaluator;
    private readonly IContextAssembler _contextAssembler;

    public GovernedRequestProcessor(
        IIdentityResolver identityResolver,
        IAuthorizationEvaluator authorizationEvaluator,
        IContextAssembler contextAssembler)
    {
        _identityResolver = identityResolver;
        _authorizationEvaluator = authorizationEvaluator;
        _contextAssembler = contextAssembler;
    }

    public GovernedRequestResult Process(GovernedRequest request, string intent)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrWhiteSpace(intent);

        var identity = _identityResolver.Resolve(request);
        if (!identity.IsResolved || identity.Subject != request.Subject)
        {
            return GovernedRequestResult.Deny("The request identity could not be resolved and verified.");
        }

        var authorizationRequest = new AuthorizationRequest(
            identity.Subject,
            request.Domain,
            request.Purpose);
        var authorization = _authorizationEvaluator.Evaluate(authorizationRequest);

        if (!authorization.Request.Equals(authorizationRequest))
        {
            return GovernedRequestResult.Deny("The authorization decision was not bound to the evaluated request.");
        }

        if (!authorization.TryCreateGrant(out var grant))
        {
            return GovernedRequestResult.Deny(authorization.Reason);
        }

        var context = _contextAssembler.Assemble(request, grant!);
        return GovernedRequestResult.Allow(GovernedProposal.Create(context, intent));
    }
}

public sealed record GovernedRequestResult(
    bool IsAllowed,
    string Reason,
    GovernedProposal? Proposal)
{
    public static GovernedRequestResult Allow(GovernedProposal proposal) =>
        new(true, "Request authorized.", proposal);

    public static GovernedRequestResult Deny(string reason) =>
        new(false, reason, null);
}
