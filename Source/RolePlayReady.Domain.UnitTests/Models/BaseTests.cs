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
        var testBase = new BaseTests.TestBase(dateTime) {
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
        var testBase = new BaseTests.TestBase {
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
    [InlineData(null, null, null, null, 3)]
    [InlineData(0, 0, 0, 0, 3)]
    [InlineData(-1, -1, -1, -1, 4)]
    [InlineData(1, 1, 1, 1, 0)]
    [InlineData(TestBase.MaxNameSize + 1, TestBase.MaxDescriptionSize + 1, TestBase.MaxShortNameSize + 1, TestBase.MaxShortNameSize + 1, 3)]
    public void Validate_Validates(int? nameSize, int? descriptionSize, int? shortNameSize, int? tagsSize, int expectedErrorCount) {
        var testBase = new BaseTests.TestBase {
            Id = "TN",
            Name = TestDataHelpers.GenerateTestString(nameSize)!,
            Description = TestDataHelpers.GenerateTestString(descriptionSize)!,
            ShortName = TestDataHelpers.GenerateTestString(shortNameSize)!,
            Tags = TestDataHelpers.GenerateTestCollection(tagsSize, "Valid")!
        };


        var result = testBase.Validate();

        result.Errors.Should().HaveCount(expectedErrorCount);
    }
}