namespace RoleplayReady.Domain.Models;

public interface IFeature
{
    GameSystem System { get; }
    string Name { get; }
    string Description { get; }
}