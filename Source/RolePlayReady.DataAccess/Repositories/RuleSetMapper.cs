namespace RolePlayReady.DataAccess.Repositories;

public class RuleSetMapper {
    public static RuleSetDataModel MapFrom(IRuleSet input)
        => new() {
            Name = input.Name,
            Description = input.Description,
            Tags = input.Tags.ToArray(),
            AttributeDefinitions = input.AttributeDefinitions.Select(MapFrom).ToArray<RuleSetDataModel.Attribute>(),
        };

    private static RuleSetDataModel.Attribute MapFrom(IAttributeDefinition input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = input.DataType.Name,
        };

    public static RuleSet? MapFrom(DataFile<RuleSetDataModel>? input)
        => input is null
            ? null
            : new() {
                ShortName = input.Id,
                Name = input.Content.Name,
                Description = input.Content.Description,
                Timestamp = input.Timestamp,
                State = State.NotReady,
                Tags = input.Content.Tags,
                AttributeDefinitions = input.Content.AttributeDefinitions.Select(MapFrom).ToArray(),
            };

    private static IAttributeDefinition MapFrom(RuleSetDataModel.Attribute input)
        => new AttributeDefinition() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = Type.GetType(input.DataType)!
        };
}
