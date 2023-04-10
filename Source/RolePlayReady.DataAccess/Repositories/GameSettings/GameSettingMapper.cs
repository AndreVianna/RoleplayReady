using Attribute = RolePlayReady.Models.Attribute;

namespace RolePlayReady.DataAccess.Repositories.GameSettings;

public class GameSettingMapper
{
    public static SettingDataModel MapFrom(IGameSetting input)
        => new()
        {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            Tags = input.Tags.ToArray(),
            AttributeDefinitions = input.AttributeDefinitions.Select(MapFrom).ToArray(),
        };

    private static SettingDataModel.Attribute MapFrom(IAttribute input)
        => new()
        {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = input.DataType.Name,
        };

    public static GameSetting? MapFrom(DataFile<SettingDataModel>? input)
        => input is null
            ? null
            : new GameSetting
            {
                ShortName = input.Content.ShortName,
                Name = input.Content.Name,
                Description = input.Content.Description,
                Timestamp = input.Timestamp,
                State = State.NotReady,
                Tags = input.Content.Tags,
                AttributeDefinitions = input.Content.AttributeDefinitions.Select(MapFrom).ToArray(),
            };

    private static IAttribute MapFrom(SettingDataModel.Attribute input)
        => new Attribute
        {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = Type.GetType(input.DataType)!
        };
}
