namespace System.Security.Cryptography;

public class HasherTests {
    [Fact]
    public void Constructor_CreatesObject() {
        var subject = new Hasher();

        subject.Should().NotBeNull();
    }

    [Fact]
    public void HashSecret_CreatesObject() {
        var hasher = new Hasher();

        var subject = hasher.HashSecret("Text to be encrypted.");

        subject.Hash.Should().NotBeNull();
        subject.Salt.Should().NotBeNull();
    }

    [Theory]
    [InlineData("Text to be encrypted.", true)]
    [InlineData("Invalid hash to be compared.", false)]
    public void VerifySecret_CreatesObject(string input, bool expectedResult) {
        var hasher = new Hasher();
        var secret = hasher.HashSecret("Text to be encrypted.");

        var subject = hasher.VerifySecret(input, secret);

        subject.Should().Be(expectedResult);
    }
}