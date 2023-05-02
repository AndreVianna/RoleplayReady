using RolePlayReady.Api.Controllers.Auth;
using RolePlayReady.Api.Controllers.Auth.Models;
using RolePlayReady.Handlers.Auth;

using SignInResult = System.Results.SignInResult;

namespace RolePlayReady.Api.Controllers;

public class AccountsControllerTests {
    private readonly IAuthHandler _handler = Substitute.For<IAuthHandler>();
    private static readonly ILogger<AuthController> _logger = Substitute.For<ILogger<AuthController>>();
    private static readonly Login _sample = new() {
        Email = "some.user@host.com",
        Password = "Password!1234",
    };

    private readonly AuthController _controller;

    public AccountsControllerTests() {
        _controller = new AuthController(_handler, _logger);
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
        _handler.Authenticate(Arg.Any<Login>()).Returns(SignInResult.Success(token));

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
                .Returns(SignInResult.Failure());

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
                .Returns(SignInResult.Invalid("Some validation error.", "login"));

        // Act
        var response = _controller.Login(request);

        // Assert
        var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = result.Value.Should().BeOfType<SerializableError>().Subject;
        error.Should().HaveCount(1);
    }
}