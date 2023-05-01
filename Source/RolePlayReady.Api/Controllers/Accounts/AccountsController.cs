using IAuthenticationHandler = RolePlayReady.Security.Handlers.IAuthenticationHandler;

namespace RolePlayReady.Api.Controllers.Accounts;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]", Order = 0)]
[ApiExplorerSettings(GroupName = "Account Management")]
[Produces("application/json")]
public class AccountsController : ControllerBase {
    private readonly IAuthenticationHandler _handler;
    private readonly ILogger<AccountsController> _logger;

    public AccountsController(IAuthenticationHandler handler, ILogger<AccountsController> logger) {
        _handler = handler;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request) {
        var login = request.ToDomain();
        var result = _handler.Authenticate(login);
        if (result.HasErrors) {
            _logger.LogDebug("'{user}' fail to login (bad request).", request.Email);
            return BadRequest(result.Errors.UpdateModelState(ModelState));
        }

        if (!result.IsSuccess) {
            _logger.LogDebug("'{user}' failed to login (failed attempt).", request.Email);
            return Unauthorized();
        }

        _logger.LogDebug("'{user}' logged in successfully.", request.Email);
        return Ok(result.Token!.ToLoginResponse());
    }
}