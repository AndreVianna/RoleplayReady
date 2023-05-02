namespace RolePlayReady.DataAccess.Repositories.Domains;

public static class SphereMapper {
    public static SphereData ToData(this Sphere input)
        => new() {
            Id = input.Id,
            State = input.State,
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            Tags = input.Tags.ToArray(),
            AttributeDefinitions = input.AttributeDefinitions.Select(ToData).ToArray(),
        };

    public static Row ToRow(this SphereData input)
        => new() {
            Id = input.Id,
            Name = input.Name,
        };

    public static Sphere? ToModel(this SphereData? input)
        => input is null
            ? null
            : new() {
                Id = input.Id,
                State = input.State,
                ShortName = input.ShortName,
                Name = input.Name,
                Description = input.Description,
                Tags = input.Tags,
                AttributeDefinitions = input.AttributeDefinitions.Select(ToModel).ToArray(),
            };

    private static SphereData.AttributeDefinitionData ToData(IAttributeDefinition input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = input.DataType.GetName(),
        };

    private static AttributeDefinition ToModel(SphereData.AttributeDefinitionData input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = Make.TypeFrom(input.DataType),
        };
}
