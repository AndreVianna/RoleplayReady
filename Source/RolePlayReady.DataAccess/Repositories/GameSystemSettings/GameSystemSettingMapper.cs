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
            DataType = input.DataType.GetFriendlyName(),
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

    private static IAttributeDefinition Map(this DataModelAttribute input) {
        return input.DataType switch {
            "Integer" => CreateAttributeDefinition<int>(),
            "Decimal" => CreateAttributeDefinition<decimal>(),
            "String" => CreateAttributeDefinition<string>(),
            "List<Integer>" => CreateAttributeDefinition<List<int>>(),
            "List<String>" => CreateAttributeDefinition<List<string>>(),
            "Dictionary<Integer,String>" => CreateAttributeDefinition<Dictionary<int,string>>(),
            "Dictionary<String,Integer>" => CreateAttributeDefinition<Dictionary<string,int>>(),
            "Dictionary<String,String>" => CreateAttributeDefinition<Dictionary<string,string>>(),
            _ => throw new NotSupportedException($"Data type '{input.DataType}' is not supported."),
        };

        IAttributeDefinition CreateAttributeDefinition<TValue>()
            => new AttributeDefinition<TValue> {
                ShortName = input.ShortName,
                Name = input.Name,
                Description = input.Description,
            };
        }
}
