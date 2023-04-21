using static RolePlayReady.Constants.Constants.Validation.Base;

namespace RolePlayReady.Models;

public class BaseTests {
    private record TestBase : Base<string> {
        public TestBase(IDateTime? dateTime = null)
            : base(dateTime) { }
    }

    [Fact]
    public void Constructor_WithDateTime_SetsTimestamp() {
        var dateTime = Substitute.For<IDateTime>();
        dateTime.Now.Returns(DateTime.Parse("2001-01-01 00:00:00"));
        var testBase = new TestBase(dateTime) {
            Id = "TN",
            Name = "TestName",
            Description = "TestDescription",
            Tags = new List<string>()
        };

        testBase.Id.Should().Be("TN");
        testBase.Timestamp.Should().Be(dateTime.Now);
        testBase.Name.Should().Be("TestName");
        testBase.Description.Should().Be("TestDescription");
        testBase.ShortName.Should().BeNull();
        testBase.Tags.Should().BeEmpty();
        testBase.ToString().Should().Be("[TestBase] TestName");
    }

    [Fact]
    public void Constructor_WithoutDateTime_SetsTimestampToUtcNow() {
        var testBase = new TestBase {
            Id = "TN",
            Name = "TestName",
            Description = "TestDescription",
            ShortName = "TST"
        };

        testBase.Should().NotBeNull();
        testBase.ShortName.Should().Be("TST");
        testBase.Id.Should().Be("TN");
        testBase.ToString().Should().Be("[TestBase] TestName (TST)");
    }

    [Theory]
    [InlineData(null, null, null, null, null, 3)]
    [InlineData(0, 0, 0, 0, null, 3)]
    [InlineData(-1, -1, -1, -1, null, 4)]
    [InlineData(1, 1, 1, 1, null, 1)]
    [InlineData(1, 1, 1, 1, 0, 1)]
    [InlineData(1, 1, 1, 1, -1, 1)]
    [InlineData(1, 1, 1, 1, 1, 0)]
    [InlineData(MaxNameSize + 1, MaxDescriptionSize + 1, MaxShortNameSize + 1, 1, MaxTagSize + 1, 4)]
    public void Validate_Validates(int? nameSize, int? descriptionSize, int? shortNameSize, int? tagListCount, int? tagsSize, int expectedErrorCount) {
        var testBase = new TestBase {
            Id = "TN",
            Name = TestDataHelpers.GenerateTestString(nameSize)!,
            Description = TestDataHelpers.GenerateTestString(descriptionSize)!,
            ShortName = TestDataHelpers.GenerateTestString(shortNameSize)!,
            Tags = TestDataHelpers.GenerateTestCollection(tagListCount, TestDataHelpers.GenerateTestString(tagsSize))!,
        };

        var result = testBase.Validate();

        result.Errors.Should().HaveCount(expectedErrorCount);
    }
}