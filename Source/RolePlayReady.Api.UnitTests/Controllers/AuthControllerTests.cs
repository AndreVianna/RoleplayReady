using SignInResult = System.Results.SignInResult;

namespace RolePlayReady.Api.Controllers;

public class AuthControllerTests {
    private readonly IAuthHandler _handler = Substitute.For<IAuthHandler>();
    private static readonly ILogger<AuthController> _logger = Substitute.For<ILogger<AuthController>>();
    private static readonly SignIn _sample = new() {
        Email = "some.user@host.com",
        Password = "Password!1234",
    };

    private readonly AuthController _controller;

    public AuthControllerTests() {
        _controller = new AuthController(_handler, _logger);
    }

    [Fact]
    public async Task Login_WithValidLogin_ReturnsToken() {
        // Arrange
        var request = new LoginRequest {
            Email = _sample.Email,
            Password = _sample.Password,
        };
        const string token = "ValidToken";
        var expected = token.ToLoginResponse();
        _handler.SignInAsync(Arg.Any<SignIn>()).Returns(SignInResult.Success(token));

        // Act
        var response = await _controller.LoginAsync(request);

        // Assert
        var result = response.Should().BeOfType<OkObjectResult>().Subject;
        var content = result.Value.Should().BeOfType<LoginResponse>().Subject;
        content.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Login_WithIncorrectLogin_ReturnsToken() {
        // Arrange
        var request = new LoginRequest {
            Email = _sample.Email,
            Password = _sample.Password,
        };
        _handler.SignInAsync(Arg.Any<SignIn>()).Returns(SignInResult.Failure());

        // Act
        var response = await _controller.LoginAsync(request);

        // Assert
        response.Should().BeOfType<UnauthorizedResult>();
    }

    [Fact]
    public async Task Login_WithInvalidRequest_ReturnsToken() {
        // Arrange
        var request = new LoginRequest {
            Email = "invalid",
            Password = "invalid",
        };
        _handler.SignInAsync(Arg.Any<SignIn>()).Returns(SignInResult.Invalid("Some validation error.", "login"));

        // Act
        var response = await _controller.LoginAsync(request);

        // Assert
        var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = result.Value.Should().BeOfType<SerializableError>().Subject;
        error.Should().HaveCount(1);
    }
}