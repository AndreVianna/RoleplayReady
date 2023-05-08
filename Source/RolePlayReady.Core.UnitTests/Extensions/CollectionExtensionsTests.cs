using System.Validation.Builder.Abstractions;
using System.Validation.Builder;

namespace System.Extensions;

public class CollectionExtensionsTests {
    [Fact]
    public void IsRequired_FromNull_ReturnsConnector() {
        // Arrange
        List<int>? subject = default;

        // Act
        var result = subject.IsRequired();

        // Assert
        result.Should().BeOfType<IConnectors<ICollection<int?>, CollectionValidators<int>>>();
    }
}