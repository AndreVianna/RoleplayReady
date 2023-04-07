namespace RolePlayReady.Models.Contracts;

public interface ITrackable {
    public DateTime Timestamp { get; init; }
    public State State { get; init; }
}