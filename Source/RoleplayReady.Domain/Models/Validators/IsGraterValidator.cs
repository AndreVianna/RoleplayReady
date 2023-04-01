namespace RoleplayReady.Domain.Models.Validators;

public record IsGraterValidator<TValue> : ElementValidator
    where TValue : IComparable<TValue> {
    [SetsRequiredMembers]
    public IsGraterValidator(string attributeName, TValue minimum, string message)
        : base((e, errors) => {
            var attribute = e.GetAttribute<TValue>(attributeName);
            if (attribute.Value is not null && attribute.Value.CompareTo(minimum) > 0)
                return e;
            errors.Add(new ValidationError(message));
            return e;
        }) {
    }
}