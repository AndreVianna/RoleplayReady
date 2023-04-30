namespace System.Results;

public enum SignInResultValue {
    Invalid = 0, // request has errors.
    Blocked = 1, // account is blocked.
    Locked = 2, // account is locked.
    Failed = 3, // attempt failed.
    Succeeded = 4, // attempt succeeded.
    TwoFactorRequired = 5, // attempt succeeded, but requires 2-factor authentication.
}