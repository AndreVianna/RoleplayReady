namespace RolePlayReady.DataAccess.Repositories.Domains;

public class SphereData : IKey {
    public Guid Id { get; init; }
    public State State { get; init; }
    public string? ShortName { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string[] Tags { get; init; }
    public required AttributeDefinitionData[] AttributeDefinitions { get; init; }

    public class AttributeDefinitionData {
        public string? ShortName { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required string DataType { get; init; }
    }
}
