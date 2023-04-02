namespace RoleplayReady.Domain.Operations.Validations;

public record Contains<TValue> : EntityValidation {
    [SetsRequiredMembers]
    public Contains(string attributeName, TValue candidate, string message)
        : base((e, errors) => {
            var list = e.GetList<TValue>(attributeName);
            if (!list.Contains(candidate))
                errors.Insert(0, new ValidationError(message));

            return errors;
        }) {
    }
}