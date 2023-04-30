using static System.Results.SignInResultValue;

namespace System.Results;

public sealed record SignInResult : ResultBase {
    private SignInResult(IEnumerable<ValidationError> errors) {
        Errors = Ensure.IsNotNullOrEmpty(errors).ToList();
        Value = Invalid;
        Token = null;
    }

    private SignInResult(SignInResultValue value, string? token = null) {
        Errors = new List<ValidationError>();
        Value = value; // if there are errors, then the result is invalid.
        Token = IsSuccess ? Ensure.IsNotNull(token) : null; // only set token if success.
    }

    public string? Token { get; }
    public SignInResultValue Value { get; }

    public bool IsInvalid => Value is Invalid;
    public bool IsBlocked => Value is Blocked;
    public bool IsLocked => Value is Locked;
    public bool IsFailure => Value is Failed;
    public override bool IsSuccess => Value is Succeeded or TwoFactorRequired;
    public bool RequiresTwoFactor => Value is TwoFactorRequired;

    //public static SignInResult Succeeded { get; } = new();
    public static SignInResult FromSuccess(string token, bool requires2Factor = false)
        => new(requires2Factor ? TwoFactorRequired : Succeeded, token);
    public static SignInResult FromError(string message, string source)
        => FromError(new ValidationError(message, source));
    public static SignInResult FromError(ValidationError error)
        => FromErrors(new[] { error });
    public static SignInResult FromErrors(IEnumerable<ValidationError> errors)
        => new(Ensure.IsNotNullOrEmpty(errors));

    public static implicit operator SignInResult(List<ValidationError> errors)
        => new(Ensure.IsNotNullOrEmpty(errors));
    public static implicit operator SignInResult(ValidationError[] errors)
        => new(Ensure.IsNotNullOrEmpty(errors));
    public static implicit operator SignInResult(ValidationError error)
        => new(new[] { error }.AsEnumerable());
    public static implicit operator SignInResult(SignInResultValue resultValue)
        => resultValue switch {
            Invalid => throw new InvalidCastException("Use 'FromError' static method to build a invalid sign in result."),
            Succeeded or TwoFactorRequired => throw new InvalidCastException("Use 'FromSuccess' static method to build a successful sign in result."),
            _ => new SignInResult(resultValue)
        };

    public static implicit operator SignInResultValue(SignInResult result) => result.Value;

    public static SignInResult operator +(SignInResult left, Result right)
        => right.Errors.Any() ? new(right.Errors) : left;

    public static SignInResult operator +(Result left, SignInResult right)
        => left.Errors.Any() ? new(left.Errors) : right;

    public static bool operator ==(SignInResult left, SignInResultValue right) => left.Value == right;
    public static bool operator !=(SignInResult left, SignInResultValue right) => left.Value != right;

}