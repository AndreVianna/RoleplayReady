namespace RoleplayReady.Domain.Models.Contracts;

public interface IAttribute : IChild {
    Type DataType { get; init; }
}