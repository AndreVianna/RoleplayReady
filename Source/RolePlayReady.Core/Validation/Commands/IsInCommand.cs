﻿namespace System.Validation.Commands;

public sealed class IsInCommand<TItem>
    : ValidationCommand {
    public IsInCommand(TItem?[] list, string source)
        : base(source) {
        ValidateAs = i => list.Contains((TItem)i);
        ValidationErrorMessage = MustBeIn;
        GetErrorMessageArguments = i => [list, i];
    }
}