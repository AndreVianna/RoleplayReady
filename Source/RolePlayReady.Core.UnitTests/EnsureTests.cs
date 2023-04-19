namespace System;

public class EnsureTests {
    [Fact]
    public void IsOfType_WhenArgumentIsOfWrongType_ThrowsArgumentNullException() {
        var input = 12;
        var action = () => Ensure.IsOfType<string>(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' is not of type 'String'. Found: 'Int32'. (Parameter 'input')");
    }

    [Fact]
    public void IsOfType_WhenArgumentIsOfRightType_ReturnsInput() {
        var input = "value";
        var result = Ensure.IsOfType<string>(input);
        result.Should().BeSameAs(input);
    }

    [Fact]
    public void NotNull_WhenArgumentIsNull_ThrowsArgumentNullException() {
        object? input = null;
        var action = () => Ensure.IsNotNull(input);
        action.Should().Throw<ArgumentNullException>().WithMessage("'input' cannot be null. (Parameter 'input')");
    }

    [Fact]
    public void NotNull_WhenArgumentIsNotNull_ReturnsInput() {
        var input = new object();
        var result = Ensure.IsNotNull(input);
        result.Should().BeSameAs(input);
    }

    [Fact]
    public void NotNullOrEmpty_WhenStringIsNull_ThrowsArgumentException() {
        string? input = null;
        var action = () => Ensure.IsNotNullOrEmpty(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot be null. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmpty_WhenStringIsEmpty_ThrowsArgumentException() {
        var input = string.Empty;
        var action = () => Ensure.IsNotNullOrEmpty(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot be empty. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmpty_WhenStringIsNotEmpty_ReturnsInput() {
        var input = "Hello";
        var result = Ensure.IsNotNullOrEmpty(input);
        result.Should().Be(input);
    }

    [Fact]
    public void NotNullOrWhiteSpace_WhenStringIsNull_ThrowsArgumentException() {
        string? input = null;
        var action = () => Ensure.IsNotNullOrWhiteSpace(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot be null. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrWhiteSpace_WhenStringIsEmpty_ThrowsArgumentException() {
        var input = string.Empty;
        var action = () => Ensure.IsNotNullOrWhiteSpace(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot be empty. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrWhiteSpace_WhenStringIsWhiteSpace_ThrowsArgumentException() {
        var input = " ";
        var action = () => Ensure.IsNotNullOrWhiteSpace(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot be whitespace. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrWhiteSpace_WhenStringIsNotEmpty_ReturnsInput() {
        var input = "Hello";
        var result = Ensure.IsNotNullOrWhiteSpace(input);
        result.Should().Be(input);
    }

    [Fact]
    public void NotNullOrHasNull_WhenIsNull_ThrowsArgumentException() {
        ICollection<int> input = default!;
        var action = () => Ensure.IsNotNullAndHasNoNullItems(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot be null. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrHasNull_WhenHasNull_ThrowsArgumentException() {
        var input = new[] { default(int?) };
        var action = () => Ensure.IsNotNullAndHasNoNullItems(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot contain null items. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrHasNullOrEmpty_WhenHasEmpty_ThrowsArgumentException() {
        var input = new[] { string.Empty };
        var action = () => Ensure.IsNotNullAndHasNoNullOrEmptyItems(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot contain null or empty items. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrHasNullOrEmpty_WhenValid_ThrowsArgumentException() {
        var input = new[] { "Hello" };
        var result = Ensure.IsNotNullAndHasNoNullOrEmptyItems(input);
        result.Should().BeSameAs(input);
    }

    [Fact]
    public void NotNullOrHasNullOrWhiteSpace_WhenHasWhitespace_ThrowsArgumentException() {
        var input = new[] { " " };
        var action = () => Ensure.IsNotNullAndHasNoNullOrWhiteSpaceItems(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot contain null or whitespace items. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrHasNullOrWhiteSpace_WhenValid_ThrowsArgumentException() {
        var input = new[] { "Hello" };
        var result = Ensure.IsNotNullAndHasNoNullOrWhiteSpaceItems(input);
        result.Should().BeSameAs(input);
    }

    [Fact]
    public void NotNullOrHasNull_WhenIsNotEmpty_ReturnsInput() {
        var input = new[] { 1, 2, 3 };
        var result = Ensure.IsNotNullAndHasNoNullItems(input);
        result.Should().BeSameAs(input);
    }

    [Fact]
    public void NotNullOrEmpty_WhenIsNull_ThrowsArgumentException() {
        ICollection<int> input = default!;
        var action = () => Ensure.IsNotNullOrEmpty(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot be null. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmpty_WhenIsEmpty_ThrowsArgumentException() {
        var input = Array.Empty<int>();
        var action = () => Ensure.IsNotNullOrEmpty(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot be empty. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmpty_WhenIsNotEmpty_ReturnsInput() {
        var input = new[] { 1, 2, 3 };
        var result = Ensure.IsNotNullOrEmpty(input);
        result.Should().BeSameAs(input);
    }

    [Fact]
    public void NotNullOrEmptyOrHasNull_WhenHasNull_ThrowsArgumentException() {
        var input = new[] { default(int?) };
        var action = () => Ensure.IsNotNullOrEmptyAndHasNoNullItems(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot contain null items. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmptyOrHasNull_WhenValid_ReturnsSame() {
        var input = new[] { "hello" };
        var result = Ensure.IsNotNullOrEmptyAndHasNoNullItems(input);
        result.Should().BeSameAs(input);
    }

    [Fact]
    public void NotNullOrEmptyOrHasNullOrEmpty_WhenHasEmpty_ThrowsArgumentException() {
        var input = new[] { string.Empty };
        var action = () => Ensure.IsNotNullOrEmptyAndHasNoNullOrEmptyItems(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot contain null or empty items. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmptyOrHasNullOrEmpty_WhenValid_ReturnsSame() {
        var input = new[] { "Hello" };
        var result = Ensure.IsNotNullOrEmptyAndHasNoNullOrEmptyItems(input);
        result.Should().BeSameAs(input);
    }

    [Fact]
    public void NotNullOrEmptyOrHasNullOrWhiteSpace_WhenHasWhitespace_ThrowsArgumentException() {
        var input = new[] { " " };
        var action = () => Ensure.IsNotNullOrEmptyAndHasNoNullOrWhiteSpaceItems(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot contain null or whitespace items. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmptyOrHasNullOrWhiteSpace_WhenEmpty_ThrowsArgumentException() {
        var input = Array.Empty<string>();
        var action = () => Ensure.IsNotNullOrEmptyAndHasNoNullOrWhiteSpaceItems(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot be empty. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmptyOrHasNullOrWhiteSpace_WhenHasNull_ThrowsArgumentException() {
        var input = new[] { default(string) };
        var action = () => Ensure.IsNotNullOrEmptyAndHasNoNullOrWhiteSpaceItems(input!);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot contain null or whitespace items. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmptyOrHasNullOrWhiteSpace_WhenHasEmpty_ThrowsArgumentException() {
        var input = new[] { string.Empty };
        var action = () => Ensure.IsNotNullOrEmptyAndHasNoNullOrWhiteSpaceItems(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot contain null or whitespace items. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmptyOrHasNullOrWhiteSpace_WhenValid_ReturnsSame() {
        var input = new[] { "Hello" };
        var result = Ensure.IsNotNullOrEmptyAndHasNoNullOrWhiteSpaceItems(input);
        result.Should().BeSameAs(input);
    }

    [Fact]
    public void ArgumentExistsAndIsOfType_WhenEmpty_ThrowsArgumentException() {
        var method = "MethodName";
        var arguments = new object?[] { };
        var action = () => Ensure.ArgumentExistsAndIsOfType<string>(method, 0, arguments);
        action.Should().Throw<ArgumentException>().WithMessage("Invalid number of arguments for MethodName. Missing argument 0. (Parameter 'MethodName()')");
    }

    [Fact]
    public void ArgumentExistsAndIsOfType_WhenWrongType_ThrowsArgumentException() {
        var method = "MethodName";
        var arguments = new object?[] { 1, "2", 3 };
        var action = () => Ensure.ArgumentExistsAndIsOfType<string>(method, 0, arguments);
        action.Should().Throw<ArgumentException>().WithMessage("Invalid type for MethodName argument 0. Expected: String. Found: Integer (Parameter 'MethodName(1, '2', 3)')");
    }

    [Fact]
    public void ArgumentExistsAndIsOfType_WhenValid_ReturnsItem() {
        var method = "MethodName";
        var arguments = new object?[] { 1, "2", 3.0m };
        var value = Ensure.ArgumentExistsAndIsOfType<int>(method, 0, arguments);
        value.Should().Be(1);
    }
}
