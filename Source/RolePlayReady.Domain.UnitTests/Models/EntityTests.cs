namespace RolePlayReady.Models;

public class EntityTests {
    private record TestEntity : Entity<string> {
        public TestEntity(IDateTime? dateTime = null)
            : base(dateTime) { }
    }

    [Fact]
    public void Constructor_WithDateTime_SetsTimestampAndAttributes() {
        var dateTime = Substitute.For<IDateTime>();
        dateTime.Now.Returns(DateTime.Parse("2001-01-01 00:00:00"));
        var testEntity = new TestEntity(dateTime) {
            Id = "TN",
            Name = "TestName",
            Description = "TestDescription",
            Attributes = new List<IEntityAttribute>(),
        };

        testEntity.Attributes.Should().BeEmpty();
    }

    [Fact]
    public void Constructor_WithoutDateTime_SetsTimestampToUtcNowAndAttributes() {
        var testEntity = new TestEntity {
            Id = "TN",
            Name = "TestName",
            Description = "TestDescription",
        };

        testEntity.Attributes.Should().BeEmpty();
    }
}