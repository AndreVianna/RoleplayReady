namespace RolePlayReady.Models.Attributes;

public class AttributeDefinitionTests {
    [Fact]
    public void Constructor_InitializesProperties() {
        var attribute = new AttributeDefinition<int> {
            Name = "TestAttribute",
            ShortName = "TA",
            Description = "Some description.",
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
        var attribute = new AttributeDefinition<decimal> {
            Name = "TestAttribute",
            Description = "Some description.",
        };

        attribute.Name.Should().Be("TestAttribute");
        attribute.ShortName.Should().BeNull();
        attribute.Description.Should().Be("Some description.");
        attribute.DataType.Should().Be(typeof(decimal));
        attribute.Constraints.Should().BeEmpty();
        attribute.ToString().Should().Be("[AttributeDefinition] TestAttribute: Decimal");
    }

    [Fact]
    public void ToString_ForStringAttribute_ShowsCorrectValue() {
        var attribute = new AttributeDefinition<string> {
            Name = "TestAttribute",
            Description = "Some description.",
        };

        attribute.DataType.Should().Be(typeof(string));
        attribute.ToString().Should().Be("[AttributeDefinition] TestAttribute: String");
    }

    [Fact]
    public void ToString_ForDateTimeAttribute_ShowsCorrectValue() {
        var attribute = new AttributeDefinition<DateTime> {
            Name = "TestAttribute",
            Description = "Some description.",
        };

        attribute.DataType.Should().Be(typeof(DateTime));
        attribute.ToString().Should().Be("[AttributeDefinition] TestAttribute: DateTime");
    }

    [Fact]
    public void ToString_ForListAttribute_ShowsCorrectValue() {
        var attribute = new AttributeDefinition<List<int>> {
            Name = "TestAttribute",
            Description = "Some description.",
        };

        attribute.DataType.Should().Be(typeof(List<int>));
        attribute.ToString().Should().Be("[AttributeDefinition] TestAttribute: List<Integer>");
    }

    [Fact]
    public void ToString_ForDictionaryAttribute_ShowsCorrectValue() {
        var attribute = new AttributeDefinition<Dictionary<string,int>> {
            Name = "TestAttribute",
            Description = "Some description.",
        };

        attribute.DataType.Should().Be(typeof(Dictionary<string, int>));
        attribute.ToString().Should().Be("[AttributeDefinition] TestAttribute: Dictionary<String,Integer>");
    }
}