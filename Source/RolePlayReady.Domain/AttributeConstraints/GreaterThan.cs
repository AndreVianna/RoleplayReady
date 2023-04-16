namespace RolePlayReady.AttributeConstraints;

public class GreaterThan : IAttributeConstraint {
    private readonly int _minimum;

    public GreaterThan(int minimum) {
        _minimum = minimum;
    }

    public ValidationResult Validate(object? value) => new();
}