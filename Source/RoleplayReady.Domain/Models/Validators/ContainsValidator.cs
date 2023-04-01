namespace RoleplayReady.Domain.Models.Validators;

public record ContainsValidator<TValue> : ElementValidator {
    [SetsRequiredMembers]
    public ContainsValidator(string attributeName, TValue candidate, string message)
        : base((e, errors) => {
            var attribute = e.GetAttribute<HashSet<TValue>>(attributeName);
            if (attribute.Value == null || !attribute.Value.Contains(candidate))
                errors.Add(new ValidationError(message));

            return e;
        }) {
    }
}