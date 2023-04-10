namespace RolePlayReady.Models;

public class BaseTests {
    private record TestBase : Base {
        public TestBase(IDateTime? dateTime = null)
            : base(dateTime) { }
    }

    [Fact]
    public void Constructor_WithDateTime_SetsTimestamp() {
        var dateTime = Substitute.For<IDateTime>();
        dateTime.Now.Returns(DateTime.Parse("2001-01-01 00:00:00"));
        var testBase = new TestBase(dateTime) {
            Name = "TestName",
            Description = "TestDescription",
            Tags = new List<string>()
        };

        testBase.Timestamp.Should().Be(dateTime.Now);
        testBase.Name.Should().Be("TestName");
        testBase.Description.Should().Be("TestDescription");
        testBase.ShortName.Should().BeNull();
        testBase.DataFileName.Should().Be("TN");
        testBase.Tags.Should().BeEmpty();
        testBase.ToString().Should().Be("[TestBase] TestName");
    }

    [Fact]
    public void Constructor_WithoutDateTime_SetsTimestampToUtcNow() {
        var testBase = new TestBase {
            Name = "TestName",
            Description = "TestDescription",
            ShortName = "TST"
        };

        testBase.Should().NotBeNull();
        testBase.ShortName.Should().Be("TST");
        testBase.DataFileName.Should().Be("TST");
        testBase.ToString().Should().Be("[TestBase] TestName (TST)");
    }
}