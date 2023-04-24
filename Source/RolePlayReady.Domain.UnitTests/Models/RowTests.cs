namespace RolePlayReady.Models;

public class RowTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var id = Guid.NewGuid();
        var row = new Row {
            Id = id,
            Name = "TestName",
        };

        row.Should().NotBeNull();
        row.Id.Should().Be(id);
        row.Name.Should().Be("TestName");
    }
}