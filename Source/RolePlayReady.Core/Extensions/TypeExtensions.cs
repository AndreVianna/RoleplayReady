namespace System;

public static class TypeExtensions {
    public static string GetFriendlyName(this Type type) {
        if (!type.IsGenericType) return GetFriendlyName(type.Name);
        var genericArgs = string.Join(",", type.GetGenericArguments().Select(GetFriendlyName));
        return $"{type.Name.Split('`')[0]}<{genericArgs}>";
    }

    private static string GetFriendlyName(string name)
        => name switch {
            "Int32" => "Integer",
            _ => name
        };
}