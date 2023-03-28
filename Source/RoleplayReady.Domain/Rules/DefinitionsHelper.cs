namespace RoleplayReady.Domain.Rules;

public static class DefinitionsHelper
{
    public static ElementProperty<T?> GetElementProperty<T>(Element element, string name) =>
        element.Properties.OfType<ElementProperty<T?>>().First(p => p.Name == name);

    public static ElementProperty<T?> CreateElementProperty<T>(Element element, string name)
    {
        var property = element.System.Properties.First(p => p.Name == name);
        return new()
        {
            System = property.System,
            Name = property.Name,
            Description = property.Description
        };
    }

    public static void AddProperty<T>(this Models.System system, string name) =>
        system.Properties.Add(new Property<T?>
        {
            System = system,
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
