namespace RolePlayReady.Models.Attributes;

public sealed class AttributeConstraint : IAttributeConstraint {

    public AttributeConstraint(string validatorName, IEnumerable<object?> arguments) {
        ValidatorName = validatorName;
        Arguments = new List<object?>(arguments);
    }
    public string ValidatorName { get; }
    public ICollection<object?> Arguments { get; }
}