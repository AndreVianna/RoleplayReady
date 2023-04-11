namespace System.Results;

public class ValidationErrorTests {
    [Fact]
    public void DefaultConstructor_WithObjectInitializer_SetsInitializedProperties() {
        //Act
        var validationError = new ValidationError {
            Message = "Error message",
            Field = "fieldName",
        };

        //Assert
        validationError.Message.Should().Be("Error message");
        validationError.Field.Should().Be("fieldName");
    }

    [Fact]
    public void DefaultConstructor_WithInvalidMessage_ThrowsArgumentException() {
        //Act
        var action = () => _ = new ValidationError {
            Message = " "
        };

        //Assert
        action.Should().Throw<ArgumentException>().WithMessage("The value cannot be null or whitespaces. (Parameter 'Message')");
    }

    [Fact]
    public void Constructor_WithNullField_SetsFieldToNull() {
        //Act
        var validationError = new ValidationError {
            Message = "Error message",
            Field = null,
        };

        //Assert
        validationError.Field.Should().BeNull();
    }
}