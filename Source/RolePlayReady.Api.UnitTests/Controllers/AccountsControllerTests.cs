using SignInResult = System.Results.SignInResult;

namespace RolePlayReady.Api.Controllers;

public class AccountsControllerTests {
    private readonly IAuthenticationHandler _handler = Substitute.For<IAuthenticationHandler>();
    private static readonly ILogger<AccountsController> _logger = Substitute.For<ILogger<AccountsController>>();
    private static readonly Login _sample = new() {
        Email = "some.user@host.com",
        Password = "Password!1234",
    };

    private readonly AccountsController _controller;

    public AccountsControllerTests() {
        _controller = new AccountsController(_handler, _logger);
    }

    [Fact]
    public void Login_WithValidLogin_ReturnsToken() {
        // Arrange
        var request = new LoginRequest {
            Email = _sample.Email,
            Password = _sample.Password,
        };
        const string token = "ValidToken";
        var expected = token.ToLoginResponse();
        _handler.Authenticate(Arg.Any<Login>()).Returns(SignInResult.AsSuccess(token));

        // Act
        var response = _controller.Login(request);

        // Assert
        var result = response.Should().BeOfType<OkObjectResult>().Subject;
        var content = result.Value.Should().BeOfType<LoginResponse>().Subject;
        content.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Login_WithIncorrectLogin_ReturnsToken() {
        // Arrange
        var request = new LoginRequest {
            Email = _sample.Email,
            Password = _sample.Password,
        };
        _handler.Authenticate(Arg.Any<Login>())
                .Returns(SignInResult.AsFailure());

        // Act
        var response = _controller.Login(request);

        // Assert
        response.Should().BeOfType<UnauthorizedResult>();
    }

    [Fact]
    public void Login_WithInvalidRequest_ReturnsToken() {
        // Arrange
        var request = new LoginRequest {
            Email = "invalid",
            Password = "invalid",
        };
        _handler.Authenticate(Arg.Any<Login>())
                .Returns(SignInResult.AsInvalid("Some validation error.", "login"));

        // Act
        var response = _controller.Login(request);

        // Assert
        var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = result.Value.Should().BeOfType<SerializableError>().Subject;
        error.Should().HaveCount(1);
    }
}