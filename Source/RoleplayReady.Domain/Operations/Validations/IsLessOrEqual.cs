namespace RoleplayReady.Domain.Operations.Validations;

public record IsLessOrEqual<TValue> : EntityValidation
    where TValue : IComparable<TValue> {
    [SetsRequiredMembers]
    public IsLessOrEqual(string attributeName, TValue maximum, string message)
        : base((e, errors) => {
            var value = e.GetValue<TValue>(attributeName);
            if (value is null || value.CompareTo(maximum) > 0)
                errors.Insert(0, new ValidationError(message));

            return errors;
        }) {
    }
}