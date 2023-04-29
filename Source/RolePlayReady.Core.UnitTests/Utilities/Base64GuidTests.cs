namespace System.Utilities;

public class Base64GuidTests {
    [Fact]
    public void ToString_ReturnsBase64Guid() {
        // Arrange
        var input = Guid.NewGuid();
        var subject = new Base64Guid {
            Value = input,
        };
        // Act
        string text = subject;

        // Assert
        text.Should().NotBeNullOrWhiteSpace();
        text.Should().NotContain("/");
        text.Should().NotContain("+");
        text.Should().NotContain("=");
        text.Should().HaveLength(22);
    }

    [Fact]
    public void ToGuid_ReturnsGuid() {
        // Arrange
        var input = Guid.NewGuid();
        var subject = new Base64Guid(input);

        // Act
        Guid guid = subject;

        // Assert
        guid.Should().Be(input);
    }

    [Fact]
    public void FromBase64_SetProperly() {
        // Arrange
        var input = new string('A', 22);

        // Act
        Base64Guid subject = input;

        // Assert
        subject.Value.Should().Be(Guid.Empty);
    }

    [Fact]
    public void FromGuidEmpty_ToString_SetProperly() {
        // Arrange
        var input = (Base64Guid)Guid.Empty;

        // Act
        string subject = input;

        // Assert
        subject.Should().Be(string.Empty);
    }

    [Fact]
    public void FromNullBase64_SetProperly() {
        // Act
        Base64Guid subject = default(string)!;

        // Assert
        subject.Value.Should().Be(Guid.Empty);
    }

    [Fact]
    public void FromInvalid_Throws() {
        // Arrange
        var input = "invalid";

        // Act
        var action = () => {
            Base64Guid _ = input;
        };

        // Assert
        action.Should().Throw<FormatException>();
    }

    [Fact]
    public void FromGuid_SetProperly() {
        // Arrange
        var input = Guid.NewGuid();

        // Act
        Base64Guid subject = input;

        // Assert
        subject.Value.Should().Be(input);
    }

    [Fact]
    public void FromGuidEmpty_ToBase64Guid_SetProperly() {
        // Arrange
        var input = Guid.Empty;

        // Act
        Base64Guid subject = input;

        // Assert
        subject.Value.Should().Be(input);
    }
}