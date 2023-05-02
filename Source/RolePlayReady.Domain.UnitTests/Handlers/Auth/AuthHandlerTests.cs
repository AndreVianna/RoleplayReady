namespace RolePlayReady.Handlers.Auth;

public class AuthHandlerTests {
    private readonly AuthHandler _handler;
    private static readonly string _validUser = "some.user@host.com";
    private static readonly string _validPassword = "Secret1234!";

    public AuthHandlerTests() {
        var configuration = Substitute.For<IConfiguration>();
        configuration["Security:DefaultUser:Id"].Returns("a8788588-929a-4859-83d4-c106b30e3afd");
        configuration["Security:DefaultUser:Name"].Returns("Some User");
        configuration["Security:DefaultUser:FolderName"].Returns("SomeUser123");
        configuration["Security:DefaultUser:Email"].Returns(_validUser);
        configuration["Security:DefaultUser:Password"].Returns(_validPassword);
        configuration["Security:IssuerSigningKey"].Returns("12345678901234567890123456789012");
        configuration["Security:TokenExpirationInHours"].Returns("7");
        var dateTime = Substitute.For<IDateTime>();
        dateTime.Now.Returns(DateTime.UtcNow);

        _handler = new AuthHandler(configuration, dateTime, NullLogger<AuthHandler>.Instance);
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Authenticate_ReturnsToken(Login login, bool isSuccess, string[] errors) {
        // Act
        var result = _handler.Authenticate(login);

        // Assert
        result.IsSuccess.Should().Be(isSuccess);
        result.Errors.Select(i => i.Message).Should().BeEquivalentTo(errors);
    }

    private class TestData : TheoryData<Login, bool, string[]> {
        public TestData() {
            Add(new() { Email = _validUser, Password = _validPassword }, true, Array.Empty<string>());
            Add(new() { Email = _validUser, Password = "WrongPassword" }, false, Array.Empty<string>());
            Add(new() { Email = "invalid.user@email.com", Password = _validPassword }, false, Array.Empty<string>());
            Add(new() { Email = null!, Password = "" }, false, new[] { "'Email' cannot be null.", "'Password' cannot be empty or whitespace." });
        }
    }
}