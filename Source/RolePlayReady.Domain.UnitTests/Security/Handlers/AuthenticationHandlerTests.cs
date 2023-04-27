namespace RolePlayReady.Security.Handlers;

public class AuthenticationHandlerTests {
    private readonly AuthenticationHandler _handler;
    private static readonly string _dummyToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJhODc4ODU4OC05MjlhLTQ4NTktODNkNC1jMTA2YjMwZTNhZmQiLCJnaXZlbl9uYW1lIjoiU29tZSBVc2VyIiwidW5pcXVlX25hbWUiOiJTb21lVXNlcjEyMyIsImVtYWlsIjoic29tZS51c2VyQGhvc3QuY29tIiwibmJmIjoxNjgyNjIxMDAwLCJleHAiOjE2ODI2NDYyMDAsImlhdCI6MTY4MjYyMTAwMH0.pHCY0Zdg9NaN5NFWN1tEngdx2e_vRPRc99DsvTcWS5A";
    private static readonly string _validUser = "some.user@host.com";
    private static readonly string _validPassword = "Secret1234!";


    public AuthenticationHandlerTests() {
        var configuration = Substitute.For<IConfiguration>();
        configuration["Security:DefaultUser:Id"].Returns("a8788588-929a-4859-83d4-c106b30e3afd");
        configuration["Security:DefaultUser:Name"].Returns("Some User");
        configuration["Security:DefaultUser:Username"].Returns("SomeUser123");
        configuration["Security:DefaultUser:Email"].Returns(_validUser);
        configuration["Security:DefaultUser:Password"].Returns(_validPassword);
        configuration["Security:IssuerSigningKey"].Returns("12345678901234567890123456789012");
        configuration["Security:TokenExpirationInHours"].Returns("7");
        var dateTime = Substitute.For<IDateTime>();
        dateTime.Now.Returns(DateTime.UtcNow);

        _handler = new AuthenticationHandler(configuration, dateTime);
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public async Task Authenticate_ReturnsToken(Login login, bool isSuccess, string[] errors) {
        // Act
        var result = _handler.Authenticate(login);

        // Assert
        result.IsSuccess.Should().Be(isSuccess);
        result.Errors.Select(i => i.Message).Should().BeEquivalentTo(errors);
    }

    private class TestData : TheoryData<Login, bool, string[]> {
        public TestData() {
            Add(new() { Email = _validUser, Password = _validPassword }, true, Array.Empty<string>());
            Add(new() { Email = _validUser, Password = "WrongPassword" }, false, new[] { "AuthenticationFailed" });
            Add(new() { Email = "invalid.user@email.com", Password = _validPassword }, false, new[] { "AuthenticationFailed" });
            Add(new() { Email = null!, Password = "" }, false, new[] { "'Email' cannot be null.", "'Password' cannot be empty or whitespace." });
        }

    }
}