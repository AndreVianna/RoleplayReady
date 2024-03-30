namespace System.Validation.Builder;

[Flags]
public enum ValidatorMode {
    None = 0,
    Not = 0b0001,
    And = 0b0010,
    AndNot = And | Not,
    Or = 0b0100,
    OrNot = Or | Not,
}

public static class ValidatorModeExtensions {
    public static bool Has(this ValidatorMode mode, ValidatorMode flag) => (mode & flag) == flag;
}