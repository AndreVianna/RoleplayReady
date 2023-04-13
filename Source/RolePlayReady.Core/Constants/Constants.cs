namespace System.Constants;

public static class Constants {
    public static class ErrorMessages {
        public const string IsNotOfType = "'{0}' is not of type '{1}'.";
        public const string IsRequired = "'{0}' is required.";
        public const string CannotBeNull = "'{0}' cannot be null.";
        public const string CannotBeEmpty = "'{0}' cannot be empty.";
        public const string CannotBeWhitespace = "'{0}' cannot be whitespace.";
        public const string CannotBeEmptyOrWhitespace = "'{0}' cannot be empty or whitespace.";

        public const string CannotBeLongerThan = "'{0}' length cannot be greater than {1}.";
        public const string CannotBeShorterThan = "'{0}' length cannot be less than {1}.";

        public const string CannotContainNull = "'{0}' cannot contain null items.";
        public const string CannotContainNullOrEmpty = "'{0}' cannot contain null or empty items.";
        public const string CannotContainNullOrWhitespace = "'{0}' cannot contain null or whitespace items.";

        public const string LengthOutOfRange = "'{0}' length cannot be less than {1} or greater than {2}.";
        public const string LengthAboveMaximum = "'{0}' cannot have more than {1} items.";
        public const string LengthBelowMinimum = "'{0}' cannot have less than {1} items.";

        public const string CountOutOfRange = "'{0}' cannot have less than {1} or more than {2} items.";

        public const string CannotAssignToResult = "The value cannot be assined to a result of type '{0}'.";
        public const string ResultIsNull = "The result is null.";
        public const string ResultContainErrors = "The result contains {0} errors.";
        public const string ResultIsNotNull = "The result is not null.";
    }
}
