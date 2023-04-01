namespace RoleplayReady.Domain.Models.Validators;

public record IsLessValidator<TValue> : ElementValidator
    where TValue : IComparable<TValue> {
    [SetsRequiredMembers]
    public IsLessValidator(string attributeName, TValue maximum, string message)
        : base((e, errors) => {
            var attribute = e.GetAttribute<TValue>(attributeName);
            if (attribute.Value is not null && attribute.Value.CompareTo(maximum) < 0)
                return e;
            errors.Add(new ValidationError(message));
            return e;
        }) {
    }
}