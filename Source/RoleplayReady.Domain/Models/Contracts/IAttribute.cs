namespace RoleplayReady.Domain.Models.Contracts;

public interface IAttribute : IEntity {
    Type DataType { get; init; }
}