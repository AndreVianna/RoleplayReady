using RolePlayReady.Api.Controllers.Users.Models;

namespace RolePlayReady.Api.Controllers.Users;

[Authorize]
[ApiController]
[Route("api/v{version:apiVersion}/users", Order = 99)]
[ApiExplorerSettings(GroupName = "Users")]
[Produces("application/json")]
public class UsersController(IAuthHandler handler, IDateTime dateTime, ILogger<UsersController> logger) : ControllerBase {
    [HttpGet]
    [SwaggerOperation(Summary = "Get all users",
                      Description = "Retrieves a collection of users.",
                      OperationId = "GetUserById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserRowResponse[]))]
    public async Task<IActionResult> GetMany(CancellationToken ct = default) {
        logger.LogDebug("Getting all users requested.");
        var result = await handler.GetManyAsync(ct);
        var response = result.Value!.ToResponse();
        logger.LogDebug("{count} users retrieved successfully.", response.Length);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get a user",
                      Description = "Retrieves a user by its ID.",
                      OperationId = "GetUserById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute]
        [SwaggerParameter("The id of the user.", Required = true)]
        string id,
        CancellationToken ct = default) {
        logger.LogDebug("Getting user '{id}' requested.", id);
        if (!Base64Guid.TryParse(id, out var uuid)) {
            ModelState.AddModelError("id", "Not a valid base64 uuid.");
            return BadRequest(ModelState);
        }

        var result = await handler.GetByIdAsync(uuid, ct);
        if (result.Value is null) {
            logger.LogDebug("Fail to retrieve user '{id}' (not found).", id);
            return NotFound();
        }

        var response = result.Value!.ToResponse(dateTime.Now);
        logger.LogDebug("Game system '{id}' retrieved successfully.", id);
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a user",
                      Description = "Creates a new user using the provided request data.",
                      OperationId = "CreateUser")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> Create(
        [FromBody]
        [SwaggerParameter("New user data.", Required = true)]
        UserRequest request,
        CancellationToken ct = default) {
        logger.LogDebug("Create user requested.");
        var model = request.ToDomain();
        var result = await handler.AddAsync(model, ct);
        if (result.IsInvalid) {
            logger.LogDebug("Fail to create user (bad request).");
            return BadRequest(result.Errors.UpdateModelState(ModelState));
        }

        if (result.IsConflict) {
            logger.LogDebug("Fail to create user (conflict).");
            return Conflict("A user with same email already exists.");
        }

        var response = result.Value!.ToResponse(dateTime.Now);
        logger.LogDebug("Game system '{id}' created successfully.", response.Id);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a user",
                      Description = "Updates an existing user with the given ID using the provided request data.",
                      OperationId = "UpdateUser")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromRoute]
        [SwaggerParameter("The id of the user.", Required = true)]
        string id,
        [FromBody]
        [SwaggerParameter("Updated user data.", Required = true)]
        UserRequest request,
        CancellationToken ct = default) {
        logger.LogDebug("Update user '{id}' requested.", id);
        if (!Base64Guid.TryParse(id, out var uuid)) {
            ModelState.AddModelError("id", "Not a valid base64 uuid.");
            return BadRequest(ModelState);
        }

        var model = request.ToDomain(uuid);
        var result = await handler.UpdateAsync(model, ct);
        if (result.IsInvalid) {
            logger.LogDebug("Fail to update user '{id}' (bad request).", id);
            return BadRequest(result.Errors.UpdateModelState(ModelState));
        }

        if (result.IsNotFound) {
            logger.LogDebug("Fail to update user '{id}' (not found).", id);
            return NotFound();
        }

        var response = result.Value!.ToResponse(dateTime.Now);
        logger.LogDebug("Game system '{id}' updated successfully.", id);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Remove a user",
                      Description = "Removes an existing user with the given ID.",
                      OperationId = "RemoveUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove(
        [FromRoute]
        [SwaggerParameter("The id of the user.", Required = true)]
        string id,
        CancellationToken ct = default) {
        logger.LogDebug("Remove user '{id}' requested.", id);
        if (!Base64Guid.TryParse(id, out var uuid)) {
            ModelState.AddModelError("id", "Not a valid base64 uuid.");
            return BadRequest(ModelState);
        }

        var result = await handler.RemoveAsync(uuid, ct);
        if (result.IsNotFound) {
            logger.LogDebug("Fail to remove user '{id}' (not found).", id);
            return NotFound();
        }

        logger.LogDebug("Game system '{id}' removed successfully.", id);
        return Ok();
    }
}