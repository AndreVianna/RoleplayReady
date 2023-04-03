using RolePlayReady.Utilities;

namespace RolePlayReady.Operations.Validations;

public record IsLess<TValue> : EntityValidation
    where TValue : IComparable<TValue> {
    [SetsRequiredMembers]
    public IsLess(string attributeName, TValue maximum, string message)
        : base((e, errors) => {
            var value = e.GetValue<TValue>(attributeName);
            if (value is null || value.CompareTo(maximum) >= 0)
                errors.Insert(0, new ValidationError(message));

            return errors;
        }) {
    }
}