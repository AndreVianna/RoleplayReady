namespace RolePlayReady.Models.Contracts;

public interface IAttribute : IIdentification {
    IRuleSet RuleSet { get; }
    Type DataType { get; }
}
