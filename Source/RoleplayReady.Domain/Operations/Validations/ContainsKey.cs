using RolePlayReady.Utilities;

namespace RolePlayReady.Operations.Validations;

public record ContainsKey<TKey, TValue> : EntityValidation
    where TKey : notnull {
    [SetsRequiredMembers]
    public ContainsKey(string attributeName, TKey key, string message)
        : base((e, errors) => {
            var value = e.GetValue(attributeName);
            if (value is not IDictionary dictionary || !dictionary.Contains(key))
                errors.Insert(0, new ValidationError(message));

            return errors;
        }) {
    }
}