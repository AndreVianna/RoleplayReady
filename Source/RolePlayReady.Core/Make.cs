namespace System;

public static class Make {
    public static Type TypeFrom(string name)
        => name switch {
            "Integer" => Type.GetType("System.Int32")!,
            "Long" => Type.GetType("System.Int64")!,
            "Decimal" => Type.GetType("System.Decimal")!,
            "String" => Type.GetType("System.String")!,
            _ when name.StartsWith("List<") => typeof(List<>).MakeGenericType(GetInnerTypes(name[5..^1])),
            _ when name.StartsWith("Dictionary<") => typeof(Dictionary<,>).MakeGenericType(GetInnerTypes(name[11..^1])),
            _ => Type.GetType($"System.{name}") ?? throw new InvalidOperationException($"Unsupported type '{name}'.")
        };

    private static Type[] GetInnerTypes(string list) => list.Split(',').Select(TypeFrom).ToArray();
}