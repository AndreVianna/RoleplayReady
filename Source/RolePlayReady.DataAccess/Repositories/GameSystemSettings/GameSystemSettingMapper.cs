using DataModel = RolePlayReady.DataAccess.Repositories.GameSystemSettings.GameSystemSettingDataModel;
using DataModelAttribute = RolePlayReady.DataAccess.Repositories.GameSystemSettings.GameSystemSettingDataModel.AttributeDefinition;

namespace RolePlayReady.DataAccess.Repositories.GameSystemSettings;

public static class GameSystemSettingMapper {
    public static DataModel Map(this GameSystemSetting input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            Tags = input.Tags.ToArray(),
            AttributeDefinitions = input.AttributeDefinitions.Select(Map).ToArray(),
        };

    private static DataModelAttribute Map(this IAttributeDefinition input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = input.DataType.GetName(),
        };

    public static GameSystemSetting? Map(this DataFile<DataModel>? input)
        => input is null
            ? null
            : new() {
                Id = Guid.Parse(input.Name),
                ShortName = input.Content.ShortName,
                Name = input.Content.Name,
                Description = input.Content.Description,
                Timestamp = input.Timestamp,
                State = State.Pending,
                Tags = input.Content.Tags,
                AttributeDefinitions = input.Content.AttributeDefinitions.Select(Map).ToArray(),
            };

    private static IAttributeDefinition Map(this DataModelAttribute input)
        => new AttributeDefinition {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = Make.TypeFrom(input.DataType),
        };
}
