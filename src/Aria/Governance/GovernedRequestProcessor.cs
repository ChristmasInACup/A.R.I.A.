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

    public GovernedProposal Process(GovernedRequest request, string intent)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrWhiteSpace(intent);

        var identity = _identityResolver.Resolve(request);
        if (!identity.IsResolved || identity.Subject != request.Subject)
        {
            throw new UnauthorizedAccessException("The request identity could not be resolved and verified.");
        }

        var authorization = _authorizationEvaluator.Evaluate(
            new AuthorizationRequest(identity.Subject, request.Domain, request.Purpose));

        if (!authorization.IsAuthorized)
        {
            throw new UnauthorizedAccessException(authorization.Reason);
        }

        var context = _contextAssembler.Assemble(request, authorization);
        return GovernedProposal.Create(context, intent);
    }
}
