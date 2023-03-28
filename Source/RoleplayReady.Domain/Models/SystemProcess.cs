namespace RoleplayReady.Domain.Models;

public record SystemProcess
{
    // System and Process must be unique.
    public required System System { get; init; }
    public required Process Process { get; init; }
    public IList<SystemProcessStep> Steps { get; init; } = new List<SystemProcessStep>();
}