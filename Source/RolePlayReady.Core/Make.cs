namespace System;

public static class Make {

    public static Type TypeFrom(string typeFriendlyName)
        => typeFriendlyName switch {
            "Integer" => Type.GetType("System.Int32")!,
            "Decimal" => Type.GetType("System.Decimal")!,
            "String" => Type.GetType("System.String")!,
            _ when typeFriendlyName.StartsWith("List<") => typeof(List<>).MakeGenericType(GetInnerTypes(typeFriendlyName[5..^1])),
            _ when typeFriendlyName.StartsWith("Dictionary<") => typeof(Dictionary<,>).MakeGenericType(GetInnerTypes(typeFriendlyName[11..^1])),
            _ => throw new InvalidOperationException($"Unsupported type '{typeFriendlyName}'.")
        };

    private static Type[] GetInnerTypes(string list) => list.Split(',').Select(TypeFrom).ToArray();
}