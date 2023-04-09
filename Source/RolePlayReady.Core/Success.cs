namespace System;

public record Success {
    private Success() { }

    public static Success Instance { get; } = new();
}