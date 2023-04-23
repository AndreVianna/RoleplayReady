namespace RolePlayReady.Models;

public class PersistedTests {
    private record TestObject(string Name, string Description);

    [Fact]
    public void Constructor_WithDateTimeProvider_CreatesInstance() {
        var dateTime = Substitute.For<IDateTime>();
        dateTime.Now.Returns(DateTime.Parse("2001-01-01 00:00:00"));
        var id = Guid.NewGuid();
        var subject = new Persisted<TestObject>(dateTime) {
            Id = id,
            Content = new TestObject("TestPersisted", "Test persisted.")
        };

        subject.Id.Should().Be(id);
        subject.Timestamp.Should().Be(dateTime.Now);
        subject.State.Should().Be(State.Pending);
        subject.Content.Name.Should().Be("TestPersisted");
        subject.Content.Description.Should().Be("Test persisted.");
    }

    [Fact]
    public void Constructor_WithoutDateTimeProvider_CreatesInstance() {
        var subject = new Persisted<TestObject> {
            Id = Guid.NewGuid(),
            Content = new TestObject("TestPersisted", "Test persisted."),
            State = State.Public,
        };

        subject.Should().NotBeNull();
        subject.Timestamp.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        subject.State.Should().Be(State.Public);
    }
}
