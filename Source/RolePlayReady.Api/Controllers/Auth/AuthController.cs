namespace RolePlayReady.Api.Controllers.Auth;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth", Order = 0)]
[ApiExplorerSettings(GroupName = "Authentication & Authorization")]
[Produces("application/json")]
public class AuthController(IAuthHandler handler, ILogger<AuthController> logger) : ControllerBase {
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequest request, CancellationToken ct = default) {
        var login = request.ToDomain();
        var result = await handler.SignInAsync(login, ct).ConfigureAwait(false);
        if (result.IsInvalid) {
            logger.LogDebug("'{user}' fail to login (bad request).", request.Email);
            return BadRequest(result.Errors.UpdateModelState(ModelState));
        }

        if (!result.IsSuccess) {
            logger.LogDebug("'{user}' failed to login (failed attempt).", request.Email);
            return Unauthorized();
        }

        logger.LogDebug("'{user}' logged in.", request.Email);
        return Ok(result.Token!.ToLoginResponse());
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request, CancellationToken ct = default) {
        var registration = request.ToDomain();
        var result = await handler.RegisterAsync(registration, ct).ConfigureAwait(false);
        if (result.IsInvalid) {
            logger.LogDebug("'{user}' failed to register (bad request).", request.Email);
            return BadRequest(result.Errors.UpdateModelState(ModelState));
        }

        if (result.IsConflict) {
            logger.LogDebug("'{user}' is already in use.", request.Email);
            return Conflict($"'{request.Email}' is already in use.");
        }

        logger.LogDebug("'{user}' registered.", request.Email);
        return Ok();
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost("users/{userId}/grant")]
    public async Task<IActionResult> GrantRoleAsync([FromRoute] Guid userId, RoleRequest request, CancellationToken ct = default) {
        var model = request.ToDomain(userId);
        var result = await handler.GrantRoleAsync(model, ct).ConfigureAwait(false);
        if (result.IsInvalid) {
            logger.LogDebug("Failed to grant '{Role}' role to '{userId}' (bad request).", request.Role, userId);
            return BadRequest(result.Errors.UpdateModelState(ModelState));
        }

        if (result.IsNotFound) {
            logger.LogDebug("Failed to grant '{role}' role to to '{userId}' (not found).", request.Role, userId);
            return NotFound();
        }

        logger.LogDebug("'{role}' role granted to user '{user}'.", request.Role, userId);
        return Ok();
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost("users/{userId}/revoke")]
    public async Task<IActionResult> RevokeRoleAsync([FromRoute] Guid userId, RoleRequest request, CancellationToken ct = default) {
        var model = request.ToDomain(userId);
        var result = await handler.RevokeRoleAsync(model, ct).ConfigureAwait(false);
        if (result.IsInvalid) {
            logger.LogDebug("Failed to revoke '{Role}' role from '{userId}' (bad request).", request.Role, userId);
            return BadRequest(result.Errors.UpdateModelState(ModelState));
        }

        if (result.IsNotFound) {
            logger.LogDebug("Failed to revoke '{role}' role from '{userId}' (not found).", request.Role, userId);
            return NotFound();
        }

        logger.LogDebug("'{role}' role revoked from user '{user}'.", request.Role, userId);
        return Ok();
    }
}