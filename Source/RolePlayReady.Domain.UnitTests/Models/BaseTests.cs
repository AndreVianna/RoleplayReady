namespace RolePlayReady.Models;

public class BaseTests {
    private record TestBase : Base;

    [Fact]
    public void Constructor_CreatesObject() {
        var testBase = new TestBase {
            Name = "TestName",
            Description = "TestDescription",
            Tags = new List<string> { "tag1", "tag2" }
        };

        testBase.Name.Should().Be("TestName");
        testBase.Description.Should().Be("TestDescription");
        testBase.ShortName.Should().BeNull();
        testBase.Tags.Should().BeEquivalentTo("tag1", "tag2");
    }

    [Theory]
    [InlineData(null, null, null, null, null, 3)]
    [InlineData(0, 0, 0, 0, null, 3)]
    [InlineData(-1, -1, -1, -1, null, 4)]
    [InlineData(1, 1, 1, 1, null, 1)]
    [InlineData(1, 1, 1, 1, 0, 1)]
    [InlineData(1, 1, 1, 1, -1, 1)]
    [InlineData(1, 1, 1, 1, 1, 0)]
    [InlineData(Validation.Name.MaximumLength + 1, Validation.Description.MaximumLength + 1, Validation.ShortName.MaximumLength + 1, 1, Validation.Tag.MaximumLength + 1, 4)]
    public void Validate_Validates(int? nameSize, int? descriptionSize, int? shortNameSize, int? tagListCount, int? tagsSize, int expectedErrorCount) {
        var testBase = new TestBase {
            Name = TestDataHelpers.GenerateTestString(nameSize)!,
            Description = TestDataHelpers.GenerateTestString(descriptionSize)!,
            ShortName = TestDataHelpers.GenerateTestString(shortNameSize)!,
            Tags = TestDataHelpers.GenerateTestCollection(tagListCount, TestDataHelpers.GenerateTestString(tagsSize))!,
        };

        var result = testBase.Validate();

        result.Errors.Should().HaveCount(expectedErrorCount);
    }
}