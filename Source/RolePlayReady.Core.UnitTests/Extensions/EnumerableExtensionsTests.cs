namespace System.Extensions;

public class EnumerableExtensionsTests {
    [Fact]
    public void ToArray_GetsArray() {
        // Act
        var result = Enumerable.Range(0, 100).ToArray<int>(i => i + 2);

        // Assert
        result.Should().BeOfType<int[]>();
        result.Should().HaveCount(100);
    }

    [Fact]
    public void ToArray_WithOutput_GetsArray() {
        // Act
        var result = Enumerable.Range(0, 100).ToArray(i => $"{i + 2}");

        // Assert
        result.Should().BeOfType<string[]>();
        result.Should().HaveCount(100);
    }
}