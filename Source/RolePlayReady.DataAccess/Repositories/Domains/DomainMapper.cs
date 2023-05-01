namespace RolePlayReady.DataAccess.Repositories.Domains;

public class DomainMapper : IDataMapper<Domain, Row, DomainData> {
    public DomainData ToData(Domain input)
        => new() {
            Id = input.Id,
            State = input.State,
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            Tags = input.Tags.ToArray(),
            AttributeDefinitions = input.AttributeDefinitions.Select(ToData).ToArray(),
        };

    public Row ToRow(DomainData input)
        => new() {
            Id = input.Id,
            Name = input.Name,
        };

    public Domain? ToModel(DomainData? input)
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

    private DomainData.AttributeDefinitionData ToData(IAttributeDefinition input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = input.DataType.GetName(),
        };

    private AttributeDefinition ToModel(DomainData.AttributeDefinitionData input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = Make.TypeFrom(input.DataType),
        };
}
