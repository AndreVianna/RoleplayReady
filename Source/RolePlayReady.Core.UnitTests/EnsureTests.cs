namespace System;

public class EnsureTests {
    [Fact]
    public void NotNull_WhenArgumentIsNull_ThrowsArgumentNullException() {
        object? input = null;
        var action = () => Ensure.NotNull(input);
        action.Should().Throw<ArgumentNullException>().WithMessage("'input' is required. (Parameter 'input')");
    }

    [Fact]
    public void NotNull_WhenArgumentIsNotNull_ReturnsInput() {
        var input = new object();
        var result = Ensure.NotNull(input);
        result.Should().BeSameAs(input);
    }
    [Fact]
    public void NotNullOrEmpty_WhenStringIsNull_ThrowsArgumentException() {
        string? input = null;
        var action = () => Ensure.NotNullOrEmpty(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' is required. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmpty_WhenStringIsEmpty_ThrowsArgumentException() {
        var input = string.Empty;
        var action = () => Ensure.NotNullOrEmpty(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot be empty. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmpty_WhenStringIsNotEmpty_ReturnsInput() {
        var input = "Hello";
        var result = Ensure.NotNullOrEmpty(input);
        result.Should().Be(input);
    }

    [Fact]
    public void NotNullOrWhiteSpace_WhenStringIsNull_ThrowsArgumentException() {
        string? input = null;
        var action = () => Ensure.NotNullOrWhiteSpace(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' is required. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrWhiteSpace_WhenStringIsEmpty_ThrowsArgumentException() {
        var input = string.Empty;
        var action = () => Ensure.NotNullOrWhiteSpace(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot be empty. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrWhiteSpace_WhenStringIsWhiteSpace_ThrowsArgumentException() {
        var input = " ";
        var action = () => Ensure.NotNullOrWhiteSpace(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot be whitespace. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrWhiteSpace_WhenStringIsNotEmpty_ReturnsInput() {
        var input = "Hello";
        var result = Ensure.NotNullOrWhiteSpace(input);
        result.Should().Be(input);
    }

    [Fact]
    public void NotNullOrHasNull_WhenIsNull_ThrowsArgumentException() {
        ICollection<int> input = default!;
        var action = () => Ensure.NotNullOrHasNull(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' is required. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrHasNull_WhenHasNull_ThrowsArgumentException() {
        var input = new[] { default(int?) };
        var action = () => Ensure.NotNullOrHasNull(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot contain null items. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrHasNullOrEmpty_WhenHasEmpty_ThrowsArgumentException() {
        var input = new[] { string.Empty };
        var action = () => Ensure.NotNullOrHasNullOrEmpty(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot contain null or empty items. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrHasNullOrEmpty_WhenValid_ThrowsArgumentException() {
        var input = new[] { "Hello" };
        var result = Ensure.NotNullOrHasNullOrEmpty(input);
        result.Should().BeSameAs(input);
    }

    [Fact]
    public void NotNullOrHasNullOrWhiteSpace_WhenHasWhitespace_ThrowsArgumentException() {
        var input = new[] { " " };
        var action = () => Ensure.NotNullOrHasNullOrWhiteSpace(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot contain null or whitespace items. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrHasNullOrWhiteSpace_WhenValid_ThrowsArgumentException() {
        var input = new[] { "Hello" };
        var result = Ensure.NotNullOrHasNullOrWhiteSpace(input);
        result.Should().BeSameAs(input);
    }

    [Fact]
    public void NotNullOrHasNull_WhenIsNotEmpty_ReturnsInput() {
        var input = new[] { 1, 2, 3 };
        var result = Ensure.NotNullOrHasNull(input);
        result.Should().BeSameAs(input);
    }

    [Fact]
    public void NotNullOrEmpty_WhenIsNull_ThrowsArgumentException() {
        ICollection<int> input = default!;
        var action = () => Ensure.NotNullOrEmpty(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' is required. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmpty_WhenIsEmpty_ThrowsArgumentException() {
        var input = Array.Empty<int>();
        var action = () => Ensure.NotNullOrEmpty(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot be empty. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmpty_WhenIsNotEmpty_ReturnsInput() {
        var input = new[] { 1, 2, 3 };
        var result = Ensure.NotNullOrEmpty(input);
        result.Should().BeSameAs(input);
    }

    [Fact]
    public void NotNullOrEmptyOrHasNull_WhenHasNull_ThrowsArgumentException() {
        var input = new[] { default(int?) };
        var action = () => Ensure.NotNullOrEmptyOrHasNull(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot contain null items. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmptyOrHasNullOrEmpty_WhenHasEmpty_ThrowsArgumentException() {
        var input = new[] { string.Empty };
        var action = () => Ensure.NotNullOrEmptyOrHasNullOrEmpty(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot contain null or empty items. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmptyOrHasNullOrEmpty_WhenValid_ReturnsSame() {
        var input = new[] { "Hello" };
        var result = Ensure.NotNullOrEmptyOrHasNullOrEmpty(input);
        result.Should().BeSameAs(input);
    }

    [Fact]
    public void NotNullOrEmptyOrHasNullOrWhiteSpace_WhenHasWhitespace_ThrowsArgumentException() {
        var input = new[] { " " };
        var action = () => Ensure.NotNullOrEmptyOrHasNullOrWhiteSpace(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot contain null or whitespace items. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmptyOrHasNullOrWhiteSpace_WhenEmpty_ThrowsArgumentException() {
        var input = Array.Empty<string>();
        var action = () => Ensure.NotNullOrEmptyOrHasNullOrWhiteSpace(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot be empty. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmptyOrHasNullOrWhiteSpace_WhenHasNull_ThrowsArgumentException() {
        var input = new[] { default(string) };
        var action = () => Ensure.NotNullOrEmptyOrHasNullOrWhiteSpace(input!);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot contain null or whitespace items. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmptyOrHasNullOrWhiteSpace_WhenHasEmpty_ThrowsArgumentException() {
        var input = new[] { string.Empty };
        var action = () => Ensure.NotNullOrEmptyOrHasNullOrWhiteSpace(input);
        action.Should().Throw<ArgumentException>().WithMessage("'input' cannot contain null or whitespace items. (Parameter 'input')");
    }

    [Fact]
    public void NotNullOrEmptyOrHasNullOrWhiteSpace_WhenValid_ReturnsSame() {
        var input = new[] { "Hello" };
        var result = Ensure.NotNullOrEmptyOrHasNullOrWhiteSpace(input);
        result.Should().BeSameAs(input);
    }
}
