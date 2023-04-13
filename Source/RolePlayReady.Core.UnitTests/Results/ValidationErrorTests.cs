namespace System.Results;

public class ValidationErrorTests {
    [Fact]
    public void DefaultConstructor_WithObjectInitializer_SetsInitializedProperties() {
        //Act
        var validationError = new ValidationError("Error message for {0} at {1}.", "fieldName", 42);

        //Assert
        validationError.Message.Should().Be("Error message for fieldName at 42.");
        validationError.Arguments.Should().BeEquivalentTo(new object?[] { "fieldName", 42 });
    }

    [Fact]
    public void DefaultConstructor_WithInvalidMessageTemplate_ThrowsArgumentException() {
        //Act
        var action = () => _ = new ValidationError("   ", "fieldName");

        //Assert
        action.Should().Throw<ArgumentException>().WithMessage("'messageTemplate' cannot be whitespace. (Parameter 'messageTemplate')");
    }

    [Fact]
    public void Constructor_WithNullField_SetsFieldToNull() {
        //Act
        var action = () => _ = new ValidationError("Error message", "   ");

        //Assert
        action.Should().Throw<ArgumentException>().WithMessage("'source' cannot be whitespace. (Parameter 'source')");
    }
}