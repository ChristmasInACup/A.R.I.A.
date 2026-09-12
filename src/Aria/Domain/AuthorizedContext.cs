namespace Aria.Domain;

public enum Sensitivity { Public, Internal, Confidential }
public enum Uncertainty { Known, Uncertain, Unknown }

/// <summary>Governed material is data, never an authority source.</summary>
public sealed record ContextMaterial(string Key, string Value, string Resource, DomainId Domain, string Purpose, Sensitivity Sensitivity, string Provenance, Uncertainty Uncertainty);

public sealed record AuthorizedContext
{
    internal AuthorizedContext(GovernedRequest request, AuthorizationGrant grant, IReadOnlyList<ContextMaterial> material) => (Request, Grant, Material) = (request, grant, material);
    public GovernedRequest Request { get; }
    public AuthorizationGrant Grant { get; }
    public IReadOnlyList<ContextMaterial> Material { get; }
    public Uncertainty Uncertainty => Material.Any(item => item.Uncertainty == Uncertainty.Unknown) ? Uncertainty.Unknown : Material.Any(item => item.Uncertainty == Uncertainty.Uncertain) ? Uncertainty.Uncertain : Uncertainty.Known;
}

public sealed record ContextAssemblyResult(bool IsAssembled, string Reason, AuthorizedContext? Context)
{
    public static ContextAssemblyResult Assembled(AuthorizedContext context) => new(true, "Authorized minimum-necessary context assembled.", context);
    public static ContextAssemblyResult Denied(string reason) => new(false, reason, null);
}

public interface IContextAssembler { ContextAssemblyResult Assemble(GovernedRequest request, AuthorizationGrant authorization); }

public sealed class MinimumNecessaryContextAssembler : IContextAssembler
{
    private readonly IReadOnlyList<ContextMaterial> _availableMaterial;
    public MinimumNecessaryContextAssembler(IEnumerable<ContextMaterial> availableMaterial) => _availableMaterial = availableMaterial?.ToArray() ?? throw new ArgumentNullException(nameof(availableMaterial));
    public ContextAssemblyResult Assemble(GovernedRequest request, AuthorizationGrant authorization)
    {
        if (!authorization.AppliesTo(request)) return ContextAssemblyResult.Denied("Authorization does not apply to the requested context.");
        var selected = _availableMaterial.Where(item => item.Domain == request.Domain && item.Purpose == request.Purpose && request.Scope.Contains(item.Resource)).ToArray();
        return selected.Length == 0 ? ContextAssemblyResult.Denied("No authorized context material is available for the request.") : ContextAssemblyResult.Assembled(new AuthorizedContext(request, authorization, selected));
    }
}
