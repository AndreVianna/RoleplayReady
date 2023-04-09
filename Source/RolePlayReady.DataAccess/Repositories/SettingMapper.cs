namespace RolePlayReady.DataAccess.Repositories;

public class SettingMapper {
    public static SettingDataModel MapFrom(ISetting input)
        => new() {
            Name = input.Name,
            Description = input.Description,
            Tags = input.Tags.ToArray(),
            AttributeDefinitions = input.AttributeDefinitions.Select(MapFrom).ToArray<SettingDataModel.Attribute>(),
        };

    private static SettingDataModel.Attribute MapFrom(IAttributeDefinition input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = input.DataType.Name,
        };

    public static Setting? MapFrom(DataFile<SettingDataModel>? input)
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

    private static IAttributeDefinition MapFrom(SettingDataModel.Attribute input)
        => new AttributeDefinition() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = Type.GetType(input.DataType)!
        };
}
