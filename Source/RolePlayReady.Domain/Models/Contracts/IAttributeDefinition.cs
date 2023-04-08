namespace RolePlayReady.Models.Contracts;

public interface IAttributeDefinition : IIdentification {
    Type DataType { get; }
}
