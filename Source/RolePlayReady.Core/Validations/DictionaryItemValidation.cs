namespace System.Validations;

public static class DictionaryItemValidation {
    public static IFinishesValidation ForEachItemIn<TKey, TValue>(IValidation<IDictionary<TKey, TValue?>?> validation, Func<TValue?, IFinishesValidation> validateUsing, bool addIsNullError = true) {
        if (validation.Subject is null) {
            if (addIsNullError) validation.Errors.Add(new(CannotBeNull, validation.Source));
            return validation;
        }

        foreach (var key in validation.Subject.Keys) {
            var source = $"{validation.Source}[{key}]";
            foreach (var error in validateUsing(validation.Subject[key]).Result.Errors) {
                var path = ((string)error.Arguments[0]!).Split('.');
                error.Arguments[0] = $"{source}.{string.Join('.', path[1..])}";
                validation.Errors.Add(error);
            }
        }

        return validation;
    }
}