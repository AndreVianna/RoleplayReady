namespace RolePlayReady.DataAccess.Models;

public class RuleSetDataModel {
    public required string Abbreviation { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string[] Tags { get; set; }
    public required AttributeDataModel[] Attributes { get; set; }

    public class AttributeDataModel {
        public required string Abbreviation { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string DataType { get; set; }
    }
}

