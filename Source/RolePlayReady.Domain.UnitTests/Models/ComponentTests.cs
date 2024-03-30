namespace RolePlayReady.Models;

public class ComponentTests {
    private record TestComponent : Component;

    [Fact]
    public void Constructor_CreatesInstance() {
        var id = Guid.NewGuid();
        var subject = new TestComponent {
            Id = id,
            Name = "TestPersisted",
            Description = "Test persisted.",

            Tags = [
                "Test",
                "Persisted",
            ],
            Attributes = [
                new TextAttribute {
                    Definition = new AttributeDefinition {
                        DataType = typeof(string),
                        Name = "TestAttribute",
                        Description = "Test attribute.",
                    },
                    Value = "Hello",
                }
            ]
        };

        subject.Attributes.Should().HaveCount(1);
    }
}