using DataModel = RolePlayReady.DataAccess.Repositories.GameSystemSettings.GameSystemSettingDataModel;
using DataModelAttribute = RolePlayReady.DataAccess.Repositories.GameSystemSettings.GameSystemSettingDataModel.AttributeDefinition;

namespace RolePlayReady.DataAccess.Repositories.GameSystemSettings;

public static class GameSystemSettingMapper {
    public static DataModel Map(this IGameSystemSetting input)
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
            DataType = input.DataType.Name,
        };

    public static GameSystemSetting Map(this IDataFile<DataModel> input)
        => new() {
            Id = Guid.Parse(input.Name),
            ShortName = input.Content.ShortName,
            Name = input.Content.Name,
            Description = input.Content.Description,
            Timestamp = input.Timestamp,
            State = State.Pending,
            Tags = input.Content.Tags,
            AttributeDefinitions = input.Content.AttributeDefinitions.Select(Map).ToArray(),
        };

    private static AttributeDefinition Map(this DataModelAttribute input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = Type.GetType(input.DataType)!
        };
}
