namespace GeneratedDI;
public class DiagnosticEntiyBase
{
    public string ThreadId { get; set; } = string.Empty;
    public string MethodName { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime FinshedAt { get; set; } = DateTime.UtcNow;
    public int ElapsedMilliseconds { get; set; } = 0;
}
