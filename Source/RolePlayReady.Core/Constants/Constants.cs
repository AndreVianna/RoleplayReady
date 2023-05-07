namespace System.Constants;

public static class Constants {
    public static class Commands {
        public const string ContainsCommand = nameof(Validation.Commands.ContainsCommand);
        public static string ContainsKeyCommand = typeof(ContainsKeyCommand<,>).Name.Split('`')[0];
        public static string ContainsValueCommand = typeof(ContainsValueCommand<,>).Name.Split('`')[0];
        public static string CountIsCommand = typeof(CountIsCommand<>).Name.Split('`')[0];
        public const string IsAfterCommand = nameof(Validation.Commands.IsAfterCommand);
        public const string IsBeforeCommand = nameof(Validation.Commands.IsBeforeCommand);
        public const string IsEmptyCommand = nameof(Validation.Commands.IsEmptyCommand);
        public const string IsEmptyOrWhiteSpaceCommand = nameof(Validation.Commands.IsEmptyOrWhiteSpaceCommand);
        public static string IsEqualToCommand = typeof(IsEqualToCommand<>).Name.Split('`')[0];
        public static string IsGreaterThanCommand = typeof(IsGreaterThanCommand<>).Name.Split('`')[0];
        public static string IsLessThanCommand = typeof(IsLessThanCommand<>).Name.Split('`')[0];
        public const string IsNullCommand = nameof(Validation.Commands.IsNullCommand);
        public static string IsOneOfCommand = typeof(IsOneOfCommand<>).Name.Split('`')[0];
        public const string IsValidCommand = nameof(Validation.Commands.IsValidCommand);
        public const string LengthIsCommand = nameof(Validation.Commands.LengthIsCommand);
        public static string MaximumCountIsCommand = typeof(MaximumCountIsCommand<>).Name.Split('`')[0];
        public const string MaximumLengthIsCommand = nameof(Validation.Commands.MaximumLengthIsCommand);
        public static string MinimumCountIsCommand = typeof(MinimumCountIsCommand<>).Name.Split('`')[0];
        public const string MinimumLengthIsCommand = nameof(Validation.Commands.MinimumLengthIsCommand);
        public const string NullCommand = nameof(Validation.Commands.NullCommand);
    }

    public static class ErrorMessages {
        public static string GetInvertedErrorMessage(string message, params object?[] args)
            => GetErrorMessage(InvertMessage(message), args);

        public static string GetErrorMessage(string message, params object?[] args)
            => string.Format(message, args);

        public static string InvertMessage(string message) => message switch {
            _ when message.Contains(" cannot ") => message.Replace(" cannot ", " must "),
            _ when message.Contains(" must ") => message.Replace(" must ", " cannot "),
            _ when message.Contains(" is not ") => message.Replace(" is not ", " is "),
            _ when message.Contains(" is ") => message.Replace(" is ", " is not "),
            _ => message
        };

        [StringSyntax(CompositeFormat)]
        public const string MustBeAfter = "'{0}' must be after {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public const string MustBeBefore = "'{0}' must be before {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public const string MustBeEmpty = "'{0}' must be empty.";
        [StringSyntax(CompositeFormat)]
        public const string MustBeEmptyOrWhitespace = "'{0}' must be empty or whitespace.";
        [StringSyntax(CompositeFormat)]
        public const string MustBeEqualTo = "'{0}' must be equal to {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public const string MustBeGraterThan = "'{0}' must be greater than {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public const string MustBeIn = "'{0}' must be one of these: '{1}'. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public const string MustBeLessThan = "'{0}' must be less than {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public const string MustBeNull = "'{0}' must be null.";
        [StringSyntax(CompositeFormat)]
        public const string MustContain = "'{0}' must contain '{1}'.";
        [StringSyntax(CompositeFormat)]
        public const string MustContainValue = "'{0}' must contain the value '{1}'.";
        [StringSyntax(CompositeFormat)]
        public const string MustContainKey = "'{0}' must contain the key '{1}'.";
        [StringSyntax(CompositeFormat)]
        public const string MustContainEmpty = "'{0}' must contain empty string(s).";
        [StringSyntax(CompositeFormat)]
        public const string MustContainEmptyOrWhitespace = "'{0}' must contain empty or whitespace string(s).";
        [StringSyntax(CompositeFormat)]
        public const string MustContainNull = "'{0}' must contain null item(s).";
        [StringSyntax(CompositeFormat)]
        public const string MustContainNullOrEmpty = "'{0}' must contain null or empty string(s).";
        [StringSyntax(CompositeFormat)]
        public const string MustContainNullOrWhitespace = "'{0}' must contain null or whitespace string(s).";
        [StringSyntax(CompositeFormat)]
        public const string MustHaveACountOf = "'{0}' count must be {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public const string MustHaveALengthOf = "'{0}' length must be {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public const string MustHaveAMaximumCountOf = "'{0}' maximum count must be {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public const string MustHaveAMaximumLengthOf = "'{0}' maximum length must be {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public const string MustHaveAMinimumCountOf = "'{0}' minimum count must be {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public const string MustHaveAMinimumLengthOf = "'{0}' minimum length must be {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public const string MustBeValid = "'{0}' must be valid.";
        [StringSyntax(CompositeFormat)]
        public const string MustBeAValidEmail = "'{0}' must be a valid email.";
        [StringSyntax(CompositeFormat)]
        public const string MustBeAValidPassword = "'{0}' must be a valid email.";
        [StringSyntax(CompositeFormat)]
        public const string MustBeOfType = "'{0}' must be of type '{1}'. Found: '{2}'.";
    }
}
