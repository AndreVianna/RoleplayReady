namespace System.Constants;

public static class Constants {
    public static class ErrorMessages {

        public static string InvertMessage(string message)
            => message.Contains("cannot")
                ? message.Replace("cannot", "must")
                : message.Contains("must")
                    ? message.Replace("must", "cannot")
                    : message;

        public const string FailedToAssign = "Cannot assign '{1}' to '{0}'.";
        public const string FailedToAssignNull = "Cannot assign null to '{0}'.";
        public const string FailedToAssignToResult = "The value cannot be assined to a result of type '{0}'.";

        public const string MustBeAfter = "'{0}' must be after {1}. Found: {2}.";
        public const string MustBeBefore = "'{0}' must be before {1}. Found: {2}.";
        public const string MustBeEmpty = "'{0}' must be empty.";
        public const string MustBeEmptyOrWhitespace = "'{0}' must be empty or whitespace.";
        public const string MustBeEqualTo = "'{0}' must be equal to {1}. Found: {2}.";
        public const string MustBeGraterThan = "'{0}' must be greater than {1}. Found: {2}.";
        public const string MustBeIn = "'{0}' must be one of these: '{1}'. Found: {2}.";
        public const string MustBeLessThan = "'{0}' must be less than {1}. Found: {2}.";
        public const string MustBeNull = "'{0}' must be null.";
        public const string MustContain = "'{0}' must contain '{1}'.";
        public const string MustContainValue = "'{0}' must contain the value '{1}'.";
        public const string MustContainKey = "'{0}' must contain the key '{1}'.";
        public const string MustContainEmpty = "'{0}' must contain empty string(s).";
        public const string MustContainEmptyOrWhitespace = "'{0}' must contain empty or whitespace string(s).";
        public const string MustContainNull = "'{0}' must contain null item(s).";
        public const string MustContainNullOrEmpty = "'{0}' must contain null or empty string(s).";
        public const string MustContainNullOrWhitespace = "'{0}' must contain null or whitespace string(s).";
        public const string MustHaveACountOf = "'{0}' count must be {1}. Found: {2}.";
        public const string MustHaveALengthOf = "'{0}' length must be {1}. Found: {2}.";
        public const string MustHaveAMaximumCountOf = "'{0}' maximum count must be {1}. Found: {2}.";
        public const string MustHaveAMaximumLengthOf = "'{0}' maximum length must be {1}. Found: {2}.";
        public const string MustHaveAMinimumCountOf = "'{0}' minimum count must be {1}. Found: {2}.";
        public const string MustHaveAMinimumLengthOf = "'{0}' minimum length must be {1}. Found: {2}.";
        public const string MustBeValid = "'{0}' must be valid.";
        public const string MustBeAValidEmail = "'{0}' must be a valid email.";
        public const string MustBeAValidPassword = "'{0}' must be a valid email.";
        public const string MustBeOfType = "'{0}' must be of type '{1}'. Found: '{2}'.";

        public const string ResultContainErrors = "The result contains {0} errors.";
        public const string ResultIsNull = "The result cannot be null.";
    }
}
