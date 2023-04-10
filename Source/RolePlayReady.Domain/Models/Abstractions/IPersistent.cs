namespace RolePlayReady.Models.Abstractions;

public interface IPersistent {
    DateTime Timestamp { get; }
    State State { get; }
    string DataFileName { get; }
}