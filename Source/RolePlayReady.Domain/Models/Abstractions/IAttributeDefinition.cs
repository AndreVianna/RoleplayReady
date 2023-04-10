namespace RolePlayReady.Models.Abstractions;

public interface IAttributeDefinition : IDescribed {
    Type DataType { get; }
}
