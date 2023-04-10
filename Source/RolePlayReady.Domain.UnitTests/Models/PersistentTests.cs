namespace RolePlayReady.Models;

public class PersistentTests {
    private record TestPersistent : Persistent<string> {
        public TestPersistent(IDateTime? dateTime = null) : base(dateTime) { }
    }

    [Fact]
    public void Constructor_WithDateTime_CreatesInstance() {
        var dateTime = Substitute.For<IDateTime>();
        dateTime.Now.Returns(DateTime.Parse("2001-01-01 00:00:00"));
        var testPersistent = new TestPersistent(dateTime) {
            Id = "TP",
        };

        testPersistent.Timestamp.Should().Be(dateTime.Now);
        testPersistent.State.Should().Be(State.Pending);
        testPersistent.Id.Should().Be("TP");
    }

    [Fact]
    public void Constructor_WithoutDateTime_CreatesInstance() {
        var testPersistent = new TestPersistent {
            Id = "TP",
            State = State.Public,
        };

        testPersistent.Should().NotBeNull();
        testPersistent.State.Should().Be(State.Public);
    }
}
