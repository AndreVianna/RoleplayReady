namespace RoleplayReady.Domain.Models.Validators;

public record ContainsKeyValidator<TKey, TValue> : ElementValidator
    where TKey : notnull {
    [SetsRequiredMembers]
    public ContainsKeyValidator(string attributeName, TKey key, string message)
        : base((e, errors) => {
            var attribute = e.GetAttribute<Dictionary<TKey, TValue>>(attributeName);
            if (attribute?.Value != null && attribute.Value.ContainsKey(key))
                return e;
            errors.Add(new ValidationError(message));
            return e;
        }) {
    }
}