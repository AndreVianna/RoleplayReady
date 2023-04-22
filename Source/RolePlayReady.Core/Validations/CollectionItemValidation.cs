namespace System.Validations;

public static class CollectionItemValidation {
    public static IFinishesValidation ForEachItemIn<TItem>(IValidation<ICollection<TItem>?> validation, Func<TItem, IFinishesValidation> validateUsing, bool addIsNullError = true) {
        if (validation.Subject is null) {
            if (addIsNullError) validation.Errors.Add(new(CannotBeNull, validation.Source));
            return validation;
        }

        var index = 0;
        foreach (var item in validation.Subject) {
            var source = $"{validation.Source}[{index++}]";
            foreach (var error in validateUsing(item).Result.Errors) {
                var path = ((string)error.Arguments[0]!).Split('.');
                error.Arguments[0] = path.Length > 1 ? $"{source}.{string.Join('.', path[1..])}" : source;
                validation.Errors.Add(error);
            }
        }

        return validation;
    }
}