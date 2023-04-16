﻿namespace System.Validations.Extensions;

public static class IntegerExtension {
    public static IIntegerValidations ValueIs(this int subject, [CallerArgumentExpression(nameof(subject))]string? source = null)
        => new IntegerValidation(subject, source);
}