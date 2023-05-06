﻿namespace System.Validation.Commands;

public sealed class NullCommand : ValidationCommand<object?> {
    public NullCommand() : base(null, string.Empty) { }
}