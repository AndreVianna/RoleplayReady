namespace RolePlayReady.Models.Attributes;

public class AttributeDefinitionTests {
    [Fact]
    public void Constructor_InitializesProperties() {
        var attribute = new AttributeDefinition {
            Name = "TestAttribute",
            ShortName = "TA",
            Description = "Some description.",
            DataType = typeof(int),
        };

        attribute.Name.Should().Be("TestAttribute");
        attribute.ShortName.Should().Be("TA");
        attribute.Description.Should().Be("Some description.");
        attribute.DataType.Should().Be(typeof(int));
        attribute.Constraints.Should().BeEmpty();
        attribute.ToString().Should().Be("[AttributeDefinition] TestAttribute (TA): Integer");
    }

    [Fact]
    public void Constructor_WithoutShortName_InitializesProperties() {
        var attribute = new AttributeDefinition {
            Name = "TestAttribute",
            Description = "Some description.",
            DataType = typeof(List<string>),
        };

        attribute.Name.Should().Be("TestAttribute");
        attribute.ShortName.Should().BeNull();
        attribute.Description.Should().Be("Some description.");
        attribute.DataType.Should().Be(typeof(List<string>));
        attribute.Constraints.Should().BeEmpty();
        attribute.ToString().Should().Be("[AttributeDefinition] TestAttribute: List<String>");
    }
}