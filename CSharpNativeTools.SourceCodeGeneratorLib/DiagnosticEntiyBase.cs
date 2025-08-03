namespace Gnobbi.DebugTools.Decorator;
public class DiagnosticEntiyBase
{
    public int ProcessId { get; set; }
    public int ThreadId { get; set; }
    public string MethodName { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime FinshedAt { get; set; } = DateTime.UtcNow;
    public int ElapsedMilliseconds { get; set; } = 0;
}
