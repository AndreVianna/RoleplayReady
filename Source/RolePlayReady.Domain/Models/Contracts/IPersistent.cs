namespace RolePlayReady.Models.Contracts;

public interface IPersistent {
    DateTime Timestamp { get; }
    State State { get; }
    string DataFileName { get; }
}