using System.Reflection;
using System.Security.Cryptography;
using Aria.Domain;

namespace Aria.Tests;

public class GovernedProposalTests
{
    // existing tests are below; runtime diagnostic added temporarily
    [Fact]
    public void Runtime_assembly_diagnostic()
    {
        var assembly = typeof(GovernedProposalDecisionBoundary).Assembly;
        var path = assembly.Location;
        var hash = Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(path)));
        var boundary = new GovernedProposalDecisionBoundary(() => DateTimeOffset.UtcNow);

        Console.WriteLine($"RUNTIME ARIA ASSEMBLY: {assembly.FullName}");
        Console.WriteLine($"RUNTIME ARIA LOCATION: {path}");
        Console.WriteLine($"RUNTIME ARIA SHA256: {hash}");
        Console.WriteLine($"RUNTIME ARIA TYPE: {typeof(GovernedProposalDecisionBoundary).AssemblyQualifiedName}");

        foreach (var loaded in AppDomain.CurrentDomain.GetAssemblies().Where(a => string.Equals(a.GetName().Name, "Aria", StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine($"LOADED ARIA: {loaded.FullName} @ {loaded.Location}");
        }

        Assert.Contains("eligibility is not approval", ReadAllUtf8OrUtf16Strings(path), StringComparison.Ordinal);
    }

    private static string ReadAllUtf8OrUtf16Strings(string path)
    {
        var bytes = File.ReadAllBytes(path);
        return System.Text.Encoding.UTF8.GetString(bytes) + "\n" + System.Text.Encoding.Unicode.GetString(bytes);
    }

    // ORIGINAL_TESTS_PLACEHOLDER
}
