namespace RoleplayReady.Domain.Rules;

public static class DefinitionsHelper
{
    public static ElementFeature<T?> GetElementProperty<T>(Element element, string name) =>
        element.Features.OfType<ElementFeature<T?>>().First(p => p.Name == name);

    public static ElementFeature<T?> CreateElementProperty<T>(Element element, string name)
    {
        var property = element.System.Features.First(p => p.Name == name);
        return new()
        {
            System = property.System,
            Name = property.Name,
            Description = property.Description
        };
    }

    public static void AddProperty<T>(this Models.GameSystem gameSystem, string name) =>
        gameSystem.Features.Add(new Feature<T?>
        {
            System = gameSystem,
            Name = name,
            Description = string.Empty
        });

    public static ElementValidation CreatePropertyValidation<T>(ValidationSeverityLevel level, string name, Func<T?, bool> validate, string failureMessage) =>
        new()
        {
            Severity = level,
            Validate = e => validate(GetElementProperty<T>(e, name).Value),
            Message = failureMessage,
        };
}
