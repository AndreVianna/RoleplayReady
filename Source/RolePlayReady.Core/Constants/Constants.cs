namespace System.Constants;

public static class Constants {
    public static class Commands {
        public static string IsEqualTo { get; } = nameof(IsEqualToCommand).Replace("Command", string.Empty);
        public static string Contains { get; } =  nameof(ContainsCommand).Replace("Command", string.Empty);
        public static string ContainsKey { get; } =  typeof(ContainsKeyCommand<,>).Name.Split('`')[0].Replace("Command", string.Empty);
        public static string ContainsValue { get; } =  typeof(ContainsValueCommand<,>).Name.Split('`')[0].Replace("Command", string.Empty);
        public static string Has { get; } =  typeof(HasCommand<>).Name.Split('`')[0].Replace("Command", string.Empty);
        public static string IsAfter { get; } =  nameof(IsAfterCommand).Replace("Command", string.Empty);
        public static string IsBefore { get; } =  nameof(IsBeforeCommand).Replace("Command", string.Empty);
        public static string IsEmpty { get; } =  nameof(IsEmptyCommand).Replace("Command", string.Empty);
        public static string IsEmptyOrWhiteSpace { get; } =  nameof(IsEmptyOrWhiteSpaceCommand).Replace("Command", string.Empty);
        public static string IsGreaterThan { get; } =  typeof(IsGreaterThanCommand<>).Name.Split('`')[0].Replace("Command", string.Empty);
        public static string IsLessThan { get; } =  typeof(IsLessThanCommand<>).Name.Split('`')[0].Replace("Command", string.Empty);
        public static string IsNull { get; } =  nameof(IsNullCommand).Replace("Command", string.Empty);
        public static string IsIn { get; } =  typeof(IsInCommand<>).Name.Split('`')[0].Replace("Command", string.Empty);
        public static string IsEmail { get; } =  nameof(IsEmailCommand).Replace("Command", string.Empty);
        public static string IsPassword { get; } =  nameof(IsPasswordCommand).Replace("Command", string.Empty);
        public static string IsValid { get; } =  nameof(IsValidCommand).Replace("Command", string.Empty);
        public static string LengthIs { get; } =  nameof(LengthIsCommand).Replace("Command", string.Empty);
        public static string HasAtMost { get; } =  typeof(HasAtMostCommand<>).Name.Split('`')[0].Replace("Command", string.Empty);
        public static string LengthIsAtMost { get; } =  nameof(LengthIsAtMostCommand).Replace("Command", string.Empty);
        public static string HasAtLeast { get; } =  typeof(HasAtLeastCommand<>).Name.Split('`')[0].Replace("Command", string.Empty);
        public static string LengthIsAtLeast { get; } =  nameof(LengthIsAtLeastCommand).Replace("Command", string.Empty);
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
        public static string MustBeAfter { get; } = "'{0}' must be after {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public static string MustBeBefore { get; } = "'{0}' must be before {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public static string MustBeEmpty { get; } = "'{0}' must be empty.";
        [StringSyntax(CompositeFormat)]
        public static string MustBeEmptyOrWhitespace { get; } = "'{0}' must be empty or whitespace.";
        [StringSyntax(CompositeFormat)]
        public static string MustBeEqualTo { get; } = "'{0}' must be equal to {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public static string MustBeGraterThan { get; } = "'{0}' must be greater than {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public static string MustBeIn { get; } = "'{0}' must be one of these: '{1}'. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public static string MustBeLessThan { get; } = "'{0}' must be less than {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public static string MustBeNull { get; } = "'{0}' must be null.";
        [StringSyntax(CompositeFormat)]
        public static string MustContain { get; } = "'{0}' must contain '{1}'.";
        [StringSyntax(CompositeFormat)]
        public static string MustContainValue { get; } = "'{0}' must contain the value '{1}'.";
        [StringSyntax(CompositeFormat)]
        public static string MustContainKey { get; } = "'{0}' must contain the key '{1}'.";
        [StringSyntax(CompositeFormat)]
        public static string MustContainNull { get; } = "'{0}' must contain null item(s).";
        [StringSyntax(CompositeFormat)]
        public static string MustContainNullOrEmpty { get; } = "'{0}' must contain null or empty string(s).";
        [StringSyntax(CompositeFormat)]
        public static string MustContainNullOrWhitespace { get; } = "'{0}' must contain null or whitespace string(s).";
        [StringSyntax(CompositeFormat)]
        public static string MustHaveACountOf { get; } = "'{0}' count must be {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public static string MustHaveALengthOf { get; } = "'{0}' length must be {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public static string MustHaveAMaximumCountOf { get; } = "'{0}' maximum count must be {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public static string MustHaveAMaximumLengthOf { get; } = "'{0}' maximum length must be {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public static string MustHaveAMinimumCountOf { get; } = "'{0}' minimum count must be {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public static string MustHaveAMinimumLengthOf { get; } = "'{0}' minimum length must be {1}. Found: {2}.";
        [StringSyntax(CompositeFormat)]
        public static string MustBeValid { get; } = "'{0}' must be valid.";
        [StringSyntax(CompositeFormat)]
        public static string MustBeAValidEmail { get; } = "'{0}' must be a valid email.";
        [StringSyntax(CompositeFormat)]
        public static string MustBeAValidPassword { get; } = "'{0}' must be a valid password.";
        [StringSyntax(CompositeFormat)]
        public static string MustBeOfType { get; } = "'{0}' must be of type '{1}'. Found: '{2}'.";
    }
}
