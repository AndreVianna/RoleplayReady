namespace RolePlayReady.Models;

public class BaseTests {
    public record TestBase : Base;

    [Fact]
    public void Constructor_CreatesObject() {
        var testBase = new TestBase {
            Name = "TestName",
            Description = "TestDescription",
            Tags = ["tag1", "tag2"]
        };

        testBase.Name.Should().Be("TestName");
        testBase.Description.Should().Be("TestDescription");
        testBase.ShortName.Should().BeNull();
        testBase.Tags.Should().BeEquivalentTo("tag1", "tag2");
    }

    private class TestData : TheoryData<TestBase, bool, int> {
        public TestData() {
            Add(new() { Name = "TestName", Description = "TestDescription", ShortName = "TN", Tags = ["tag1", "tag2"] }, true, 0);
            Add(new() { Name = null!, Description = null!, ShortName = null, Tags = null! }, false, 3);
            Add(new() { Name = "", Description = "", ShortName = "", Tags = [null!, ""] }, false, 5);
            Add(new() { Name = "  ", Description = "  ", ShortName = "  ", Tags = ["  "] }, false, 4);
            Add(new() {
                Name = new('X', Validation.Name.MaximumLength + 1),
                Description = new('X', Validation.Description.MaximumLength + 1),
                ShortName = new('X', Validation.ShortName.MaximumLength + 1),
                Tags = [new string('X', Validation.Tag.MaximumLength + 1)],
            }, false, 4);
        }
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Validate_Validates(TestBase subject, bool isSuccess, int errorCount) {
        // Act
        var result = subject.Validate();

        // Assert
        result.IsSuccess.Should().Be(isSuccess);
        result.Errors.Should().HaveCount(errorCount);
    }
}