namespace RoleplayReady.Domain.Operations.Validations;

public record IsBetween<TValue> : EntityValidation
    where TValue : IComparable<TValue> {
    [SetsRequiredMembers]
    public IsBetween(string attributeName, TValue minimum, TValue maximum, string message)
        : base((e, errors) => {
            var value = e.GetValue<TValue>(attributeName);
            if (value is null || value.CompareTo(minimum) < 0 || value.CompareTo(maximum) > 0)
                errors.Insert(0, new ValidationError(message));

            return errors;
        }) {
    }
}