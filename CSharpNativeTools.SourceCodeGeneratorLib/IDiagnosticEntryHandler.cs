namespace Gnobbi.DebugTools.Decorator.Abstractions;

public interface IDiagnosticEntryHandler
{
    Task HandleDiagnosticEntryAsync(DiagnosticEntiyBase entity);
}