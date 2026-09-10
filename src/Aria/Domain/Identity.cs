namespace Aria.Domain;

public enum IdentityResolutionStatus { Resolved, Unresolved, Invalid, Mismatched }

/// <summary>Identity evidence only; it carries no authority.</summary>
public sealed record IdentityContext
{
    private IdentityContext(SubjectId? subject, IdentityResolutionStatus status, string reason) => (Subject, Status, Reason) = (subject, status, reason);
    public SubjectId? Subject { get; }
    public IdentityResolutionStatus Status { get; }
    public string Reason { get; }
    public bool IsResolved => Status == IdentityResolutionStatus.Resolved;
    public static IdentityContext Resolved(SubjectId subject, string reason = "Identity resolved.") => new(subject, IdentityResolutionStatus.Resolved, reason);
    public static IdentityContext Unresolved(string reason = "Identity is unresolved.") => new(null, IdentityResolutionStatus.Unresolved, reason);
    public static IdentityContext Invalid(string reason = "Identity is invalid.") => new(null, IdentityResolutionStatus.Invalid, reason);
    public static IdentityContext Mismatched(SubjectId subject, string reason = "Identity does not match the request.") => new(subject, IdentityResolutionStatus.Mismatched, reason);
}

public interface IIdentityResolver { IdentityContext Resolve(GovernedRequest request); }
