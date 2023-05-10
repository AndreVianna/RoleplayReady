namespace System.Validation.Builder;

public class ConnectorTests {
    [Fact]
    public void Constructor_CreatesConnectors() {
        // Arrange
        var subject = CreateConnectorFor("value");

        // Assert
        subject.Result.IsSuccess.Should().BeTrue();
    }

    private static IConnector<StringValidator> CreateConnectorFor(string value, ValidatorMode mode = ValidatorMode.And) {
        var validator = new StringValidator(value, "Source", mode);
        return validator.Connector;
    }
}