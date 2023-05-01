using static System.Results.SignInResultValue;

namespace System.Results;

public sealed record SignInResult : IResult {
    private SignInResult(SignInResultValue value, string? token = null, IEnumerable<ValidationError>? errors = null) {
        Errors = errors?.ToList() ?? new List<ValidationError>();
        Value = HasErrors ? Invalid : value;
        Token = IsSuccess ? Ensure.IsNotNull(token) : null; // only set token if success.
    }

    public SignInResultValue Value { get; }
    public string? Token { get; }

    public IList<ValidationError> Errors { get; } = new List<ValidationError>();
    public bool HasErrors => Errors.Count != 0;
    public bool IsBlocked => !HasErrors && Value is Blocked;
    public bool IsLocked => !HasErrors && Value is Locked;
    public bool IsFailure => !HasErrors && Value is Failed;
    public bool IsSuccess => !HasErrors && Value is Succeeded or TwoFactorRequired;
    public bool RequiresTwoFactor => !HasErrors && Value is TwoFactorRequired;

    public static SignInResult AsSuccess(string token, bool requires2Factor = false)
        => new(requires2Factor ? TwoFactorRequired : Succeeded, token);
    public static SignInResult AsInvalid(string message, string source)
        => AsInvalid(new ValidationError(message, source));
    public static SignInResult AsInvalid(ValidationError error)
        => AsInvalid(new[] { error });
    public static SignInResult AsInvalid(IEnumerable<ValidationError> errors)
        => new(Invalid, null, Ensure.IsNotNullOrEmpty(errors));
    public static SignInResult AsBlocked() => new(Blocked);
    public static SignInResult AsLocked() => new(Locked);
    public static SignInResult AsFailure() => new(Failed);

    public static implicit operator SignInResult(List<ValidationError> errors)
        => new(Invalid, null, Ensure.IsNotNullOrEmpty(errors));
    public static implicit operator SignInResult(ValidationError[] errors)
        => new(Invalid, null, Ensure.IsNotNullOrEmpty(errors));
    public static implicit operator SignInResult(ValidationError error)
        => new(Invalid, null, new[] { error }.AsEnumerable());
    public static implicit operator SignInResult(SignInResultValue resultValue)
        => resultValue switch {
            Invalid => throw new InvalidCastException("Use 'AsInvalid' static method to build a invalid sign in result."),
            Succeeded or TwoFactorRequired => throw new InvalidCastException("Use 'AsSuccess' static method to build a successful sign in result."),
            _ => new SignInResult(resultValue)
        };

    public static implicit operator SignInResultValue(SignInResult result) => result.Value;

    public static SignInResult operator +(SignInResult left, Result right)
        => right.Errors.Any()
            ? new(Invalid, null, !left.Errors.Any() ? right.Errors : left.Errors.Union(right.Errors))
            : left;

    public static SignInResult operator +(Result left, SignInResult right)
        => left.Errors.Any()
            ? new(Invalid, null, !right.Errors.Any() ? left.Errors : right.Errors.Union(right.Errors))
            : right;

    public static bool operator ==(SignInResult left, SignInResultValue right) => left.Value == right;
    public static bool operator !=(SignInResult left, SignInResultValue right) => left.Value != right;

}