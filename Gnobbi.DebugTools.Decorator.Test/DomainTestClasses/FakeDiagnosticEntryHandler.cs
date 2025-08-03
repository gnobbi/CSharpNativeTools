namespace Gnobbi.DebugTools.Decorator.Decorator.Test.DomainTestClasses;

public class FakeDiagnosticEntryHandler : IDiagnosticEntryHandler
{
    public List<DiagnosticEntiyBase> Entries { get; } = [];

    public Task HandleDiagnosticEntryAsync(DiagnosticEntiyBase entity)
    {
        Entries.Add(entity);
        return Task.CompletedTask;
    }
}
