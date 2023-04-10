namespace RolePlayReady.Models.Contracts;

public interface IAttribute : IDescribed {
    Type DataType { get; }
}
