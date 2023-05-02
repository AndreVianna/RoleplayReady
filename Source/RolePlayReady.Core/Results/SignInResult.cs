using static System.Results.SignInResultType;

namespace System.Results;

public sealed record SignInResult : ValidationResult {
    public SignInResult(SignInResultType type, string? token = null, IEnumerable<ValidationError>? errors = null)
        : base(errors){
        Type = HasValidationErrors ? Invalid : type;
        Token = IsSuccess ? Ensure.IsNotNull(token) : null; // only set token if success.
    }

    public string? Token { get; }
    public SignInResultType Type { get; }
    public override bool IsSuccess => !HasValidationErrors && Type is Succeeded or TwoFactorRequired;
    public bool IsBlocked => !HasValidationErrors ? Type is Blocked : throw new InvalidOperationException("The sign in has validation errors. You must check for validation errors before checking if user is blocked.");
    public bool IsLocked => !HasValidationErrors ? Type is Locked : throw new InvalidOperationException("The sign in has validation errors. You must check for validation errors before checking if user is locked.");
    public bool IsFailure => !HasValidationErrors ? Type is Failed : throw new InvalidOperationException("The sign in has validation errors. You must check for validation errors before checking if the sign in attempt failed.");
    public bool RequiresTwoFactor => !HasValidationErrors ? Type is TwoFactorRequired : throw new InvalidOperationException("The sign in has validation errors. You must check for validation errors before checking if the sign in requires two factor authentication.");

    public static SignInResult AsSuccess(string token, bool requires2Factor = false)
        => new(requires2Factor ? TwoFactorRequired : Succeeded, token);
    public static SignInResult AsInvalid(string message, string source)
        => AsInvalid(new ValidationError(message, source));
    public static SignInResult AsInvalid(ValidationError error)
        => AsInvalid(new[] { error });
    public static new SignInResult AsInvalid(IEnumerable<ValidationError> errors)
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
    public static implicit operator SignInResult(SignInResultType resultType)
        => resultType switch {
            Invalid => throw new InvalidCastException("Use 'AsInvalid' static method to build a invalid sign in result."),
            Succeeded or TwoFactorRequired => throw new InvalidCastException("Use 'AsSuccess' static method to build a successful sign in result."),
            _ => new SignInResult(resultType)
        };

    public static implicit operator SignInResultType(SignInResult result) => result.Type;

    public static SignInResult operator +(SignInResult left, ValidationResult right)
        => right.Errors.Any()
            ? new(Invalid, null, !left.Errors.Any() ? right.Errors : left.Errors.Union(right.Errors))
            : left;

    public static SignInResult operator +(ValidationResult left, SignInResult right)
        => left.Errors.Any()
            ? new(Invalid, null, !right.Errors.Any() ? left.Errors : right.Errors.Union(right.Errors))
            : right;

    public static bool operator ==(SignInResult left, SignInResultType right) => left.Type == right;
    public static bool operator !=(SignInResult left, SignInResultType right) => left.Type != right;

}