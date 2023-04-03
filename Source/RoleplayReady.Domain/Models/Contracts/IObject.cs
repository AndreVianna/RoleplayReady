namespace RolePlayReady.Models.Contracts;

public interface IObject : IComponent {
    string Unit { get; init; }
}