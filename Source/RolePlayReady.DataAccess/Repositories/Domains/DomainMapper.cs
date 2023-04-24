using System.Extensions;

namespace RolePlayReady.DataAccess.Repositories.Domains;

internal static class DomainMapper {
    public static DomainData Map(this Domain input)
        => new() {
            Id = input.Id,
            State = input.State,
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            Tags = input.Tags.ToArray(),
            AttributeDefinitions = input.AttributeDefinitions.Select(Map).ToArray(),
        };

    private static DomainData.AttributeDefinitionData Map(this IAttributeDefinition input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = input.DataType.GetName(),
        };

    public static Row MapToRow(this DomainData input)
        => new() {
                Id = input.Id,
                Name = input.Name,
            };

    public static Domain? Map(this DomainData? input)
        => input is null
            ? null
            : new() {
                Id = input.Id,
                State = input.State,
                ShortName = input.ShortName,
                Name = input.Name,
                Description = input.Description,
                Tags = input.Tags,
                AttributeDefinitions = input.AttributeDefinitions.Select(Map).ToArray(),
            };

    private static AttributeDefinition Map(this DomainData.AttributeDefinitionData input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = Make.TypeFrom(input.DataType),
        };
}
