namespace RolePlayReady.DataAccess.Repositories.GameSystemSettings;

public class GameSystemSettingDataModel {
    public string? ShortName { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string[] Tags { get; set; }
    public required AttributeDefinition[] AttributeDefinitions { get; set; }

    public class AttributeDefinition {
        public string? ShortName { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string DataType { get; set; }
    }
}
