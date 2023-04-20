namespace System.Constants;

public static class Constants {
    public static class ErrorMessages {
        public const string CannotAssignNull = "Cannot assign null to '{0}'.";
        public const string CannotAssign = "Cannot assign '{1}' to '{0}'.";
        public const string IsNotOfType = "'{0}' is not of type '{1}'. Found: '{2}'.";
        public const string CannotBeNull = "'{0}' cannot be null.";
        public const string CannotBeEmpty = "'{0}' cannot be empty.";
        public const string CannotBeWhitespace = "'{0}' cannot be whitespace.";
        public const string CannotBeEmptyOrWhitespace = "'{0}' cannot be empty or whitespace.";

        public const string IsNotEqual = "'{0}' is not equal to {1}. Found: {2}.";

        public const string MinimumLengthIs = "'{0}' minimum length is {1} character(s). Found: {2}.";
        public const string MaximumLengthIs = "'{0}' maximum length is {1} character(s). Found: {2}.";
        public const string LengthMustBe = "'{0}' length must be exactly {1} character(s). Found: {2}.";
        public const string CannotHaveLessThan = "'{0}' cannot have less than {1} item(s). Found: {2}.";
        public const string CannotHaveMoreThan = "'{0}' cannot have more than {1} item(s). Found: {2}.";
        public const string MustHave = "'{0}' must have exactly {1} item(s). Found: {2}.";

        public const string CannotBeLessThan = "'{0}' cannot be less then {1}. Found: {2}.";
        public const string MustBeLessThan = "'{0}' must be less than {1}. Found: {2}.";
        public const string CannotBeGreaterThan = "'{0}' cannot be greater then {1}. Found: {2}.";
        public const string MustBeGraterThan = "'{0}' must be grather than {1}. Found: {2}.";
        public const string MustBeEqualTo = "'{0}' must be equal to {1}. Found: {2}.";
        public const string CannotBeBefore = "'{0}' cannot be before {1}. Found: {2}.";
        public const string MustBeBefore = "'{0}' must be befor {1}. Found: {2}.";
        public const string CannotBeAfter = "'{0}' cannot be after {1}. Found: {2}.";
        public const string MustBeAfter = "'{0}' must be after {1}. Found: {2}.";

        public const string CannotContainNull = "'{0}' cannot contain null items.";
        public const string CannotContainNullOrEmpty = "'{0}' cannot contain null or empty items.";
        public const string CannotContainNullOrWhitespace = "'{0}' cannot contain null or whitespace items.";

        public const string CountOutOfRange = "'{0}' cannot have less than {1} or more than {2} items. Found: {3}.";

        public const string CannotAssignToResult = "The value cannot be assined to a result of type '{0}'.";
        public const string ResultIsNull = "The result is null.";
        public const string ResultContainErrors = "The result contains {0} errors.";
        public const string ResultIsNotNull = "The result is not null.";
    }
}
