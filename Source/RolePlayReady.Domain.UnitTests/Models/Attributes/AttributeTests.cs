namespace RolePlayReady.Models.Attributes;

public class AttributeTests {
    [Fact]
    public void Constructor_InitializesProperties() {
        var attribute = new AttributeDefinition {
            Name = "TestName",
            ShortName = "TST",
            Description = "TestDescription",
            DataType = typeof(int)
        };

        attribute.Name.Should().Be("TestName");
        attribute.ShortName.Should().Be("TST");
        attribute.Description.Should().Be("TestDescription");
        attribute.DataType.Should().Be(typeof(int));
        attribute.ToString().Should().Be("[AttributeDefinition] TestName (TST): Int32");
    }

    [Fact]
    public void Constructor_WithoutShortName_InitializesProperties() {
        var attribute = new AttributeDefinition {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(int)
        };

        attribute.Name.Should().Be("TestName");
        attribute.ShortName.Should().BeNull();
        attribute.Description.Should().Be("TestDescription");
        attribute.DataType.Should().Be(typeof(int));
        attribute.ToString().Should().Be("[AttributeDefinition] TestName: Int32");
    }
}