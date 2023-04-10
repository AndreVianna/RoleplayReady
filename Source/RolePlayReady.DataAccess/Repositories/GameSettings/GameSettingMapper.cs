using RolePlayReady.Models.Abstractions;

namespace RolePlayReady.DataAccess.Repositories.GameSettings;

public class GameSettingMapper {
    public static SettingDataModel MapFrom(IGameSetting input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            Tags = input.Tags.ToArray(),
            AttributeDefinitions = input.AttributeDefinitions.Select(MapFrom).ToArray(),
        };

    private static SettingDataModel.Attribute MapFrom(IAttributeDefinition input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = input.DataType.Name,
        };

    public static GameSystemSetting? MapFrom(DataFile<SettingDataModel>? input)
        => input is null
            ? null
            : new GameSystemSetting {
                ShortName = input.Content.ShortName,
                Name = input.Content.Name,
                Description = input.Content.Description,
                Timestamp = input.Timestamp,
                State = State.NotReady,
                Tags = input.Content.Tags,
                AttributeDefinitions = input.Content.AttributeDefinitions.Select(MapFrom).ToArray(),
            };

    private static IAttributeDefinition MapFrom(SettingDataModel.Attribute input)
        => new AttributeDefinition {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = Type.GetType(input.DataType)!
        };
}
