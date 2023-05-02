﻿namespace System.Results;

public sealed record SignInResult : Result {
    public SignInResult(SignInResultType type, string? token = null, IEnumerable<ValidationError>? errors = null)
        : base(errors){
        Type = IsInvalid ? SignInResultType.Invalid : type;
        Token = IsSuccess ? Ensure.IsNotNull(token) : null; // only set token if success.
    }

    public string? Token { get; }
    public SignInResultType Type { get; }
    public override bool IsSuccess => !IsInvalid && Type is SignInResultType.Success or SignInResultType.TwoFactorRequired;
    public bool IsBlocked => !IsInvalid ? Type is SignInResultType.Blocked : throw new InvalidOperationException("The sign in has validation errors. You must check for validation errors before checking if user is blocked.");
    public bool IsLocked => !IsInvalid ? Type is SignInResultType.Locked : throw new InvalidOperationException("The sign in has validation errors. You must check for validation errors before checking if user is locked.");
    public bool IsFailure => !IsInvalid ? Type is SignInResultType.Failed : throw new InvalidOperationException("The sign in has validation errors. You must check for validation errors before checking if the sign in attempt failed.");
    public bool RequiresTwoFactor => !IsInvalid ? Type is SignInResultType.TwoFactorRequired : throw new InvalidOperationException("The sign in has validation errors. You must check for validation errors before checking if the sign in requires two factor authentication.");

    public static SignInResult Success(string token, bool requires2Factor = false) => new(requires2Factor ? SignInResultType.TwoFactorRequired : SignInResultType.Success, token);
    public static SignInResult Invalid(string message, string source) => Invalid(new ValidationError(message, source));
    public static SignInResult Invalid(ValidationError error) => Invalid(new[] { error });
    public static SignInResult Invalid(IEnumerable<ValidationError> errors) => new(SignInResultType.Invalid, null, Ensure.IsNotNullOrEmpty(errors));
    public static SignInResult Blocked { get; } = new(SignInResultType.Blocked);
    public static SignInResult Locked { get; } = new(SignInResultType.Locked);
    public static SignInResult Failure { get; } = new(SignInResultType.Failed);

    public static implicit operator SignInResult(List<ValidationError> errors) => new(SignInResultType.Invalid, null, Ensure.IsNotNullOrEmpty(errors));
    public static implicit operator SignInResult(ValidationError[] errors) => new(SignInResultType.Invalid, null, Ensure.IsNotNullOrEmpty(errors));
    public static implicit operator SignInResult(ValidationError error) => new(SignInResultType.Invalid, null, new[] { error }.AsEnumerable());
    public static implicit operator SignInResult(SignInResultType resultType) 
        => resultType switch {
            SignInResultType.Invalid => throw new InvalidCastException("To make an invalid result assigned the errors directly. i.e. SingInResult result = \"[Token]\";"),
            SignInResultType.Success or SignInResultType.TwoFactorRequired => throw new InvalidCastException("To make a successful result assigned the token to it. i.e. SingInResult result = \"[Token]\";"),
            _ => new SignInResult(resultType)
        };

    public static implicit operator SignInResultType(SignInResult result) => result.Type;

    public static SignInResult operator +(SignInResult left, ValidationResult right)
        => right.Errors.Any()
            ? new(SignInResultType.Invalid, null, !left.Errors.Any() ? right.Errors : left.Errors.Union(right.Errors))
            : left;

    public static bool operator ==(SignInResult left, SignInResultType right) => left.Type == right;
    public static bool operator !=(SignInResult left, SignInResultType right) => left.Type != right;

}