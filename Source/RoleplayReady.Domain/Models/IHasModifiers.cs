namespace RoleplayReady.Domain.Models;

public interface IHasModifiers {
    IHasFeatures Target { get; }
    IList<Modifier> Modifiers { get; }
}