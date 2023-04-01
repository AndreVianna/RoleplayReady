namespace RoleplayReady.Domain.Models.Validators;

public record IsEqualValidator<TValue> : ElementValidator
    where TValue : IEquatable<TValue> {
    [SetsRequiredMembers]
    public IsEqualValidator(string attributeName, TValue validValue, string message)
        : base((e, errors) => {
            var attribute = e.GetAttribute<TValue>(attributeName);
            if (attribute.Value is not null && attribute.Value.Equals(validValue))
                return e;
            errors.Add(new ValidationError(message));
            return e;
        }) {
    }
}