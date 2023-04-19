namespace System;

public static class Make {

    public static Type TypeFrom(string name)
        => name switch {
            "Integer" => Type.GetType("System.Int32")!,
            "Decimal" => Type.GetType("System.Decimal")!,
            "String" => Type.GetType("System.String")!,
            _ when name.StartsWith("List<") => typeof(List<>).MakeGenericType(GetInnerTypes(name[5..^1])),
            _ when name.StartsWith("Dictionary<") => typeof(Dictionary<,>).MakeGenericType(GetInnerTypes(name[11..^1])),
            _ => throw new InvalidOperationException($"Unsupported data type: {name}.")
        };

    private static Type[] GetInnerTypes(string list) => list.Split(',').Select(TypeFrom).ToArray();
}