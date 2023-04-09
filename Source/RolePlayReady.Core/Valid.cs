namespace System;

public record Valid {
    private Valid() { }

    public static Valid Instance { get; } = new();
}