namespace RoleplayReady.Domain.Models.Contracts;

public interface IAmTrackable {
    public DateTime Timestamp { get; init; }
    public State State { get; init; }
}