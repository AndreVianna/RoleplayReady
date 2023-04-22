namespace System.Validations;

public static class DictionaryItemValidation {
    public static IFinishesValidation ForEachItemIn<TKey, TValue>(IValidation<IDictionary<TKey, TValue>?> validation, Func<TValue, IFinishesValidation> validateUsing, bool addIsNullError = true) {
        if (validation.Subject is null) {
            if (addIsNullError) validation.Errors.Add(new(CannotBeNull, validation.Source));
            return validation;
        }

        foreach (var key in validation.Subject.Keys) {
            var source = $"{validation.Source}[{key}]";
            foreach (var error in validateUsing(validation.Subject[key]).Result.Errors) {
                var path = ((string)error.Arguments[0]!).Split('.');
                var skip = path.Length > 1 && path[1] == "Value" ? 2 : 1;
                error.Arguments[0] = path.Length > skip ? $"{source}.{string.Join('.', path[skip..])}" : source;
                validation.Errors.Add(error);
            }
        }

        return validation;
    }
}