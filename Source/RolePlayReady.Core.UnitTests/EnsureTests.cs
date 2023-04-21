namespace System;

public class EnsureTests {
    [Fact]
    public void IsOfType_WhenArgumentIsOfWrongType_ThrowsArgumentNullException() {
        const int input = 12;
        var action = () => Ensure.IsOfType<string>(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' is not of type 'String'. Found: 'Int32'. (Parameter 'input')");
    }

    [Fact]
    public void IsOfType_WhenArgumentIsOfRightType_ReturnsInput() {
        const string input = "value";
        var result = Ensure.IsOfType<string>(input);
        result.Should().BeSameAs(input);
    }

    [Fact]
    public void NotNull_WhenArgumentIsNull_ThrowsArgumentNullException() {
        const object? input = null;
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
        const string? input = null;
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
        const string input = "Hello";
        var result = Ensure.IsNotNullOrEmpty(input);
        result.Should().Be(input);
    }

    [Fact]
    public void NotNullOrWhiteSpace_WhenStringIsNull_ThrowsArgumentException() {
        const string? input = null;
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
        const string input = " ";
        var action = () => Ensure.IsNotNullOrWhiteSpace(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot be whitespace. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrWhiteSpace_WhenStringIsNotEmpty_ReturnsInput() {
        const string input = "Hello";
        var result = Ensure.IsNotNullOrWhiteSpace(input);
        result.Should().Be(input);
    }

    [Fact]
    public void NotNullOrHasNull_WhenIsNull_ThrowsArgumentException() {
        const ICollection<int> input = default!;
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
        const ICollection<int> input = default!;
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
        string[] input = { default! };
        var action = () => Ensure.IsNotNullOrEmptyAndHasNoNullOrWhiteSpaceItems(input);
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
        const string method = "MethodName";
        var arguments = Array.Empty<object?>();
        var action = () => Ensure.ArgumentExistsAndIsOfType<string>(arguments, method, 0);
        action.Should().Throw<ArgumentException>().WithMessage("Invalid number of arguments for 'MethodName'. Missing argument 0. (Parameter 'arguments')");
    }

    [Fact]
    public void ArgumentExistsAndIsOfType_WhenWrongType_ThrowsArgumentException() {
        const string method = "MethodName";
        var arguments = new object?[] { 1, "2", 3 };
        var action = () => Ensure.ArgumentExistsAndIsOfType<string>(arguments, method, 0);
        action.Should().Throw<ArgumentException>().WithMessage("Invalid type of arguments[0] of 'MethodName'. Expected: String. Found: Integer. (Parameter 'arguments[0]')");
    }

    [Fact]
    public void ArgumentExistsAndIsOfType_WhenValid_ReturnsItem() {
        const string method = "MethodName";
        var arguments = new object?[] { 1, "2", 3.0m };
        var value = Ensure.ArgumentExistsAndIsOfType<int>(arguments, method, 0);
        value.Should().Be(1);
    }

    [Fact]
    public void ArgumentsAreAllOfType_WhenEmpty_ThrowsArgumentException() {
        const string method = "MethodName";
        var arguments = Array.Empty<object?>();
        var action = () => Ensure.ArgumentsAreAllOfType<string>(arguments, method);
        action.Should().Throw<ArgumentException>().WithMessage("'arguments' cannot be empty. (Parameter 'arguments')");
    }

    [Fact]
    public void ArgumentsAreAllOfType_WhenWrongType_ThrowsArgumentException() {
        const string method = "MethodName";
        var arguments = new object?[] { 1, "2", 3 };
        var action = () => Ensure.ArgumentsAreAllOfType<int>(arguments, method);
        action.Should().Throw<ArgumentException>().WithMessage("At least one argument of 'MethodName' is of an invalid type. Expected: Integer.  Found: String. (Parameter 'arguments[1]')");
    }

    [Fact]
    public void ArgumentsAreAllOfType_WhenValid_ReturnsItem() {
        const string method = "MethodName";
        var arguments = new object?[] { 1, 2, 3 };
        var value = Ensure.ArgumentsAreAllOfType<int>(arguments, method);
        value.Should().BeOfType<int[]>().Subject.Should().BeEquivalentTo(arguments);
    }
}
