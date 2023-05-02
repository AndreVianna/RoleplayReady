using RolePlayReady.Handlers.Sphere;

namespace RolePlayReady.DataAccess.Repositories.Domains;

public class SphereMapper : ISphereMapper {
    public SphereData ToData(Sphere input)
        => new() {
            Id = input.Id,
            State = input.State,
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            Tags = input.Tags.ToArray(),
            AttributeDefinitions = input.AttributeDefinitions.Select(ToData).ToArray(),
        };

    public Row ToRow(SphereData input)
        => new() {
            Id = input.Id,
            Name = input.Name,
        };

    public Sphere? ToModel(SphereData? input)
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

    private SphereData.AttributeDefinitionData ToData(IAttributeDefinition input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = input.DataType.GetName(),
        };

    private AttributeDefinition ToModel(SphereData.AttributeDefinitionData input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = Make.TypeFrom(input.DataType),
        };
}
