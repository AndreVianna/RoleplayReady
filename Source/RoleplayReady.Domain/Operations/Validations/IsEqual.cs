namespace RoleplayReady.Domain.Operations.Validations;

public record IsEqual<TValue> : EntityValidation
    where TValue : IEquatable<TValue> {
    [SetsRequiredMembers]
    public IsEqual(string attributeName, TValue validValue, string message)
        : base((e, errors) => {
            var value = e.GetValue<TValue>(attributeName);
            if (value is null || !value.Equals(validValue))
                errors.Insert(0, new ValidationError(message));

            return errors;
        }) {
    }
}