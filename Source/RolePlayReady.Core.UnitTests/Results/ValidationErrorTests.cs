namespace System.Results;

public class ValidationErrorTests {
    [Fact]
    public void DefaultConstructor_WithObjectInitializer_SetsInitializedProperties() {
        //Act
        var validationError = new ValidationError("Error message", "fieldName");

        //Assert
        validationError.Message.Should().Be("Error message");
        validationError.Source.Should().Be("fieldName");
    }

    [Fact]
    public void DefaultConstructor_WithInvalidMessage_ThrowsArgumentException() {
        //Act
        var action = () => _ = new ValidationError("   ", "fieldName");

        //Assert
        action.Should().Throw<ArgumentException>().WithMessage("'message' cannot be whitespace. (Parameter 'message')");
    }

    [Fact]
    public void Constructor_WithNullField_SetsFieldToNull() {
        //Act
        var action = () => _ = new ValidationError("Error message", "   ");

        //Assert
        action.Should().Throw<ArgumentException>().WithMessage("'source' cannot be whitespace. (Parameter 'source')");
    }
}