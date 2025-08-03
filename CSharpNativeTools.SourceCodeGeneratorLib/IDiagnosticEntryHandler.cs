namespace Gnobbi.DebugTools.Decorator;

public interface IDiagnosticEntryHandler
{
    Task HandleDiagnosticEntryAsync(DiagnosticEntiyBase entity);
}