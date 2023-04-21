namespace System;

public static class TypeExtensions {
    public static string GetName(this Type type) {
        if (type is { IsGenericType: false, IsArray: false })
            return GetNonGenericTypeName(type.Name);
        if (type.IsArray)
            return $"{type.GetElementType()!.GetName()}[]";
        var genericArgs = string.Join(",", type.GetGenericArguments().Select(GetName));
        return $"{type.Name.Split('`')[0]}<{genericArgs}>";
    }

    private static string GetNonGenericTypeName(string name)
        => name switch {
            "Int32" => "Integer",
            "String" => "String",
            "Decimal" => "Decimal",
            _ => name
        };
}