using Aria.Domain;

namespace Aria.Governance;

/// <summary>Coordinates the M1 boundary; context is unavailable until authority permits it.</summary>
public sealed class GovernedRequestProcessor
{
    private readonly IIdentityResolver _identityResolver;
    private readonly IAuthorizationEvaluator _authorizationEvaluator;
    private readonly IContextAssembler _contextAssembler;
    public GovernedRequestProcessor(IIdentityResolver identityResolver, IAuthorizationEvaluator authorizationEvaluator, IContextAssembler contextAssembler) => (_identityResolver, _authorizationEvaluator, _contextAssembler) = (identityResolver ?? throw new ArgumentNullException(nameof(identityResolver)), authorizationEvaluator ?? throw new ArgumentNullException(nameof(authorizationEvaluator)), contextAssembler ?? throw new ArgumentNullException(nameof(contextAssembler)));

    public GovernedRequestResult Process(GovernedRequest request, string intent)
    {
        ArgumentNullException.ThrowIfNull(request); ArgumentException.ThrowIfNullOrWhiteSpace(intent);
        var identity = _identityResolver.Resolve(request);
        if (!identity.IsResolved || identity.Subject != request.Subject) return GovernedRequestResult.Deny(identity.Reason);
        var evaluation = new AuthorizationRequest(identity.Subject.Value, request.Domain, request.Purpose, request.Scope);
        var authorization = _authorizationEvaluator.Evaluate(evaluation);
        if (authorization.Request != evaluation) return GovernedRequestResult.Deny("The authorization decision was not bound to the evaluated request.");
        if (!authorization.IsAuthorized) return GovernedRequestResult.Deny(authorization.Reason);
        var context = _contextAssembler.Assemble(request, authorization.Grant!);
        return !context.IsAssembled ? GovernedRequestResult.Deny(context.Reason) : GovernedRequestResult.Allow(GovernedProposal.Create(context.Context!, intent));
    }
}

public sealed record GovernedRequestResult(bool IsAllowed, string Reason, GovernedProposal? Proposal)
{
    public static GovernedRequestResult Allow(GovernedProposal proposal) => new(true, "Request authorized.", proposal);
    public static GovernedRequestResult Deny(string reason) => new(false, reason, null);
}
