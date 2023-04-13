namespace System.Constants;

public static class Constants {
    public static class Singletons {
        public static readonly ReadOnlyCollection<ValidationError> NoErrors = new(Array.Empty<ValidationError>());
    }

    public static class ErrorMessages {
        public const string Null = "'{0}' is required.";
        public const string Empty = "'{0}' cannot be empty.";
        public const string Whitespace = "'{0}' cannot be whitespace.";
        public const string EmptyOrWhitespace = "'{0}' cannot be empty or whitespace.";

        public const string LongerThan = "'{0}' length cannot be greater than {1}.";
        public const string ShorterThan = "'{0}' length cannot be less than {1}.";

        public const string HasNull = "'{0}' cannot contain null items.";
        public const string HasNullOrEmpty = "'{0}' cannot contain null or empty items.";
        public const string HasNullOrWhitespace = "'{0}' cannot contain null or whitespace items.";

        public const string InvalidLength = "'{0}' length cannot be less than {1} or greater than {2}.";
        public const string LargerThan = "'{0}' cannot have more than {1} items.";
        public const string SmallerThan = "'{0}' cannot have less than {1} items.";

        public const string InvalidCount = "'{0}' cannot have less than {1} or more than {2} items.";

        public const string ResultInvalidType = "The value cannot be assined to result.";
        public const string ResultIsNotValid = "The result is not valid.";
        public const string ResultHasNoValue = "The result is null or failure.";
        public const string ResultIsNotNull = "The result is not null.";
        public const string ResultIsNotFailure = "The result is not failure.";
        public const string ResultHasNoExceptions = "The result has no exceptions.";
    }
}
