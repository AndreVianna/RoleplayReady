namespace RoleplayReady.Domain.Models.Contracts;

public interface IAmTracked {
    public DateTime Timestamp { get; init; }
    public State State { get; init; }
}