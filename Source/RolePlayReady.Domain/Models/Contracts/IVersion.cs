namespace RolePlayReady.Models.Contracts;

public interface IVersion {
    public DateTime Version { get; init; }
    public State State { get; init; }
}