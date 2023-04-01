namespace RoleplayReady.Domain.Models.Contracts;

public interface IAmChildOf {
    IEntity? Parent { get; init; }
}