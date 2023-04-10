namespace System;

public class ThrowTests {
    [Fact]
    public void IfNull_WhenArgumentIsNull_ThrowsArgumentNullException() {
        object? input = null;
        Action act = () => Throw.IfNull(input, paramName: "input");
        act.Should().Throw<ArgumentNullException>().WithMessage("The value cannot be null. (Parameter 'input')");
    }

    [Fact]
    public void IfNull_WhenArgumentIsNull_WithCustomMessage_ThrowsArgumentNullException() {
        object? input = null;
        Action act = () => Throw.IfNull(input, "Custom message", paramName: "input");
        act.Should().Throw<ArgumentNullException>().WithMessage("Custom message (Parameter 'input')");
    }

    [Fact]
    public void IfNull_WhenArgumentIsNotNull_ReturnsInput() {
        var input = new object();
        var result = Throw.IfNull(input);
        result.Should().BeSameAs(input);
    }

    [Fact]
    public void IfNullOrEmpty_WhenCollectionIsNull_ThrowsArgumentException() {
        IEnumerable<int>? input = null;
        Action act = () => Throw.IfNullOrEmpty(input, paramName: "input");
        act.Should().Throw<ArgumentException>().WithMessage("The collection cannot be null or empty. (Parameter 'input')");
    }

    [Fact]
    public void IfNullOrEmpty_WhenCollectionIsEmpty_WithCustomMessage_ThrowsArgumentException() {
        var input = new List<int>();
        Action act = () => Throw.IfNullOrEmpty(input, "Custom message", paramName: "input");
        act.Should().Throw<ArgumentException>().WithMessage("Custom message (Parameter 'input')");
    }

    [Fact]
    public void IfNullOrEmpty_WhenCollectionIsNotEmpty_ReturnsInput() {
        var input = new List<int> { 1, 2, 3 };
        var result = Throw.IfNullOrEmpty(input);
        result.Should().BeSameAs(input);
    }

    [Fact]
    public void IfNullOrEmpty_WhenStringIsNull_ThrowsArgumentException() {
        string? input = null;
        Action act = () => Throw.IfNullOrEmpty(input, paramName: "input");
        act.Should().Throw<ArgumentException>().WithMessage("The value cannot be null or empty. (Parameter 'input')");
    }

    [Fact]
    public void IfNullOrEmpty_WhenStringIsEmpty_WithCustomMessage_ThrowsArgumentException() {
        var input = "";
        Action act = () => Throw.IfNullOrEmpty(input, "Custom message", paramName: "input");
        act.Should().Throw<ArgumentException>().WithMessage("Custom message (Parameter 'input')");
    }

    [Fact]
    public void IfNullOrEmpty_WhenStringIsNotEmpty_ReturnsInput() {
        var input = "Hello";
        var result = Throw.IfNullOrEmpty(input);
        result.Should().Be(input);
    }

    [Fact]
    public void IfNullOrWhiteSpaces_WhenStringIsNull_ThrowsArgumentException() {
        string? input = null;
        Action act = () => Throw.IfNullOrWhiteSpaces(input, paramName: "input");
        act.Should().Throw<ArgumentException>().WithMessage("The value cannot be null or whitespaces. (Parameter 'input')");
    }

    [Fact]
    public void IfNullOrWhiteSpaces_WhenStringIsWhiteSpace_WithCustomMessage_ThrowsArgumentException() {
        var input = " ";
        Action act = () => Throw.IfNullOrWhiteSpaces(input, "Custom message", paramName: "input");
        act.Should().Throw<ArgumentException>().WithMessage("Custom message (Parameter 'input')");
    }

    [Fact]
    public void IfNullOrWhiteSpaces_WhenStringIsNotEmpty_ReturnsInput() {
        var input = "Hello";
        var result = Throw.IfNullOrWhiteSpaces(input);
        result.Should().Be(input);
    }
}
