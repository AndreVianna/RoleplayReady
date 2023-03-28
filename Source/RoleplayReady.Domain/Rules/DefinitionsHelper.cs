namespace RoleplayReady.Domain.Rules;

public static class DefinitionsHelper
{
    public static EntityProperty<T?> GetEntityProperty<T>(GameEntity gameEntity, string propertyName) =>
        gameEntity.Properties.OfType<EntityProperty<T?>>().First(p => p.Property.Name == propertyName);

    public static EntityProperty<T?> CreateEntityProperty<T>(GameEntity entity, string propertyName) =>
        new() { Property = entity.System.Properties.First(p => p.Name == propertyName) };

    public static Validation CreatePropertyValidation<T>(ValidationSeverityLevel level, string propertyName, Func<T?, bool> validate, string failureMessage) =>
        new() {
            Severity = level,
            Validate = e => validate(GetEntityProperty<T>(e, propertyName).Value),
            Message = failureMessage,
        };
}
