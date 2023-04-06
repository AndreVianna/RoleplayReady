namespace RolePlayReady.Engine.Exceptions;

public class ProcedureExceptionTests
{
    [Fact]
    public void Constructor_WithMessage_SetsMessage()
    {
        // Arrange
        const string message = "Test message";

        // Act
        var exception = new ProcedureException(message);

        // Assert
        exception.Message.Should().Be(message);
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_SetsMessageAndInnerException()
    {
        // Arrange
        const string message = "Test message";
        var innerException = new Exception("Inner exception");

        // Act
        var exception = new ProcedureException(message, innerException);

        // Assert
        exception.Message.Should().Be(message);
        exception.InnerException.Should().Be(innerException);
    }
}