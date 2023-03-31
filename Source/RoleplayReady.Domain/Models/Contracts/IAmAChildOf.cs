namespace RoleplayReady.Domain.Models.Contracts;

public interface IAmAChildOf {
    IEntity? Parent { get; init; }
}