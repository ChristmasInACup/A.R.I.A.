namespace Aria.Domain;

public enum ProposalLifecycleState { Draft }

/// <summary>A non-authoritative recommendation derived only from authorized context.</summary>
public sealed record GovernedProposal
{
    private GovernedProposal(AuthorizedContext context, string intent)
    {
        Id = Guid.NewGuid(); Context = context; Intent = intent; Subject = context.Request.Subject; Domain = context.Request.Domain; Purpose = context.Request.Purpose; Scope = context.Request.Scope; Lifecycle = ProposalLifecycleState.Draft; Uncertainty = context.Uncertainty; Material = context.Material;
    }
    public Guid Id { get; }
    public SubjectId Subject { get; }
    public DomainId Domain { get; }
    public string Purpose { get; }
    public ResourceScope Scope { get; }
    public string Intent { get; }
    public ProposalLifecycleState Lifecycle { get; }
    public Uncertainty Uncertainty { get; }
    public IReadOnlyList<ContextMaterial> Material { get; }
    public AuthorizedContext Context { get; }
    public static GovernedProposal Create(AuthorizedContext context, string intent) { ArgumentNullException.ThrowIfNull(context); ArgumentException.ThrowIfNullOrWhiteSpace(intent); return new(context, intent); }
}
