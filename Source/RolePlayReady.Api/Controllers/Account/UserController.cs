using RolePlayReady.Api.Controllers.Account.Models;
using RolePlayReady.Api.Controllers.GameSystem;
using RolePlayReady.Api.Controllers.Models;

using IAuthenticationHandler = RolePlayReady.Security.Handlers.IAuthenticationHandler;

namespace RolePlayReady.Api.Controllers.Account;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[SwaggerTag("Manages user information.")]
public class UserController : ControllerBase {
    // Inject the necessary services
    private readonly IAuthenticationHandler _handler;
    private readonly ILogger<GameSystemsController> _logger;

    public UserController(IAuthenticationHandler handler, ILogger<GameSystemsController> logger) {
        _handler = handler;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request) {
        var login = request.ToDomain();
        var result = _handler.Authenticate(login);
        if (!result.HasErrors) {
            var response = new LoginResponse { Token = result.Value };
            return Ok(response);
        }

        if (result.Errors[0].Message == "Invalid.") {
            _logger.LogDebug("Fail to login (failed attempt).");
            return Unauthorized();
        }

        _logger.LogDebug("Fail to login (bad request).");
        return BadRequest(result.Errors.UpdateModelState(ModelState));
    }
}