namespace RolePlayReady.DataAccess.Repositories.Domains;

public static class DomainMapper {
    public static DomainData Map(this Domain input)
        => new() {
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

    public static Row MapToRow(this Persisted<DomainData> input)
        => new() {
                Id = input.Id,
                Name = input.Content.Name,
            };

    public static Persisted<Domain>? Map(this Persisted<DomainData>? input)
        => input is null
            ? null
            : new() {
                Id = input.Id,
                Timestamp = input.Timestamp,
                State = State.Pending,
                Content = new() {
                    ShortName = input.Content.ShortName,
                    Name = input.Content.Name,
                    Description = input.Content.Description,
                    Tags = input.Content.Tags,
                    AttributeDefinitions = input.Content.AttributeDefinitions.Select(Map).ToArray(),
                },
            };

    private static IAttributeDefinition Map(this DomainData.AttributeDefinitionData input)
        => new AttributeDefinition {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = Make.TypeFrom(input.DataType),
        };
}
