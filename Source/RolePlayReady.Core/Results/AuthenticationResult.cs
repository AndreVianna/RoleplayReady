namespace System.Results;

public enum AuthenticationOutcome {
    Invalid = 0, // request has errors (bad request).
    Blocked = 1, // account is blocked.
    Locked = 2, // account is locked.
    Failed = 3, // attempt failed.
    Success = 4, // attempt succeeded.
    Requires2Factor = 5, // attempt succeeded, but requires 2-factor authentication.
}

public sealed record AuthenticationResult : ResultBase {

    private AuthenticationResult(AuthenticationOutcome outcome, string? token = null, DateTimeOffset? lockExpiration = null, IEnumerable<ValidationError>? errors = null) {
        Outcome = outcome;
        Token = token;
        Errors = errors?.ToList() ?? new List<ValidationError>();
        LockExpiration = lockExpiration;
    }

    public AuthenticationOutcome Outcome { get; }
    public string? Token { get; }
    public DateTimeOffset? LockExpiration { get; }

    //public static AuthenticationResult Success { get; } = new();
    //public static Result<TValue> FromValue<TValue>(TValue value) => new(value);
    //public static Result Failure(string message, string source) => Failure(new ValidationError(message, source));
    //public static Result Failure(ValidationError error) => Failure(new[] { error });
    //public static Result Failure(IEnumerable<ValidationError> errors) => new(errors);
}