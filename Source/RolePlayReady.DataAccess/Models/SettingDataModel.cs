namespace RolePlayReady.DataAccess.Models;

public class SettingDataModel {
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string[] Tags { get; set; }
    public required Attribute[] AttributeDefinitions { get; set; }

    public class Attribute {
        public string? ShortName { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string DataType { get; set; }
    }
}
