namespace RolePlayReady.Validations;

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
        action.Should().Throw<ArgumentException>().WithMessage("The value cannot be null or whitespaces. (Parameter 'value')");
    }

    [Fact]
    public void DefaultConstructor_WithoutInitializeField_SetsFieldToNull() {
        //Act
        var validationError = new ValidationError {
            Message = "Error message",
        };

        //Assert
        validationError.Field.Should().BeNull();
    }

    [Fact]
    public void Constructor_WithValidMessage_SetsMessage() {
        //Act
        var validationError = new ValidationError("Error message");

        //Assert
        validationError.Message.Should().Be("Error message");
    }

    [Fact]
    public void Constructor_WithValidMessageAndField_SetsMessageAndField() {
        //Act
        var validationError = new ValidationError("Error message", "fieldName");

        //Assert
        validationError.Message.Should().Be("Error message");
        validationError.Field.Should().Be("fieldName");
    }

    [Fact]
    public void Constructor_WithNullOrWhiteSpaceMessage_ThrowsArgumentException() {
        //Act
        var act = () => _ = new ValidationError(" ");

        //Assert
        act.Should().Throw<ArgumentException>().WithMessage("The value cannot be null or whitespaces. (Parameter 'message')");
    }

    [Fact]
    public void Constructor_WithNullField_SetsFieldToNull() {
        //Act
        var validationError = new ValidationError("Error message", null);

        //Assert
        validationError.Field.Should().BeNull();
    }
}