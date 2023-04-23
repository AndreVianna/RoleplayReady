namespace RolePlayReady.Models.Abstractions;

public interface IPersisted<out TBase> : IKey {
    DateTime Timestamp { get; }
    State State { get; }
    TBase Content { get; }
}

//public interface IPersisted : IBase, IKey {
//    DateTime Timestamp { get; }
//    State State { get; }
//}