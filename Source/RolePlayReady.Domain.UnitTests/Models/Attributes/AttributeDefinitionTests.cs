using static RolePlayReady.Constants.Constants.Validation.AttributeDefinition;

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

    [Theory]
    [InlineData(null, null, null, 2)]
    [InlineData(0, 0, 0, 3)]
    [InlineData(-1, -1, -1, 3)]
    [InlineData(1, 1, 1, 0)]
    [InlineData(MaxNameSize + 1, MaxDescriptionSize + 1, MaxShortNameSize + 1, 3)]
    public void Validate_Validates(int? nameSize, int? descriptionSize, int? shortNameSize, int expectedErrorCount) {
        var testBase = new AttributeDefinition {
            Name = TestDataHelpers.GenerateTestString(nameSize)!,
            Description = TestDataHelpers.GenerateTestString(descriptionSize)!,
            ShortName = TestDataHelpers.GenerateTestString(shortNameSize)!,
            DataType = typeof(string),
        };

        var result = testBase.Validate();

        result.Errors.Should().HaveCount(expectedErrorCount);
    }
}