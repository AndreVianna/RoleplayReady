using RolePlayReady.Api.Controllers.GameSystems.Models;

namespace RolePlayReady.Api.Controllers.GameSystems;

[Authorize]
[ApiController]
[Route("api/v{version:apiVersion}/systems", Order = 1)]
[ApiExplorerSettings(GroupName = "Game Systems")]
[Produces("application/json")]
public class GameSystemsController : ControllerBase {
    private readonly IGameSystemHandler _handler;
    private readonly ILogger<GameSystemsController> _logger;

    public GameSystemsController(IGameSystemHandler handler, ILogger<GameSystemsController> logger) {
        _handler = handler;
        _logger = logger;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all game systems",
                      Description = "Retrieves a collection of game systems.",
                      OperationId = "GetGameSystemById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameSystemRowResponse[]))]
    public async Task<IActionResult> GetMany(CancellationToken cancellationToken = default) {
        _logger.LogDebug("Getting all game systems requested.");
        var result = await _handler.GetManyAsync(cancellationToken);
        var response = result.Value!.ToResponse();
        _logger.LogDebug("{count} game systems retrieved successfully.", response.Length);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get a game system",
                      Description = "Retrieves a game system by its ID.",
                      OperationId = "GetGameSystemById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameSystemResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute]
        [SwaggerParameter("The id of the game system.", Required = true)]
        string id,
        CancellationToken cancellationToken = default) {
        _logger.LogDebug("Getting game system '{id}' requested.", id);
        if (!Base64Guid.TryParse(id, out var uuid)) {
            ModelState.AddModelError("id", "Not a valid base64 uuid.");
            return BadRequest(ModelState);
        }

        var result = await _handler.GetByIdAsync(uuid, cancellationToken);
        if (result.Value is null) {
            _logger.LogDebug("Fail to retrieve game system '{id}' (not found).", id);
            return NotFound();
        }

        var response = result.Value!.ToResponse();
        _logger.LogDebug("Game system '{id}' retrieved successfully.", id);
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a game system",
                      Description = "Creates a new game system using the provided request data.",
                      OperationId = "CreateGameSystem")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GameSystemResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> Create(
        [FromBody]
        [SwaggerParameter("New game system data.", Required = true)]
        GameSystemRequest request,
        CancellationToken cancellationToken = default) {
        _logger.LogDebug("Create game system requested.");
        var model = request.ToDomain();
        var result = await _handler.AddAsync(model, cancellationToken);
        if (result.HasErrors) {
            _logger.LogDebug("Fail to create game system (bad request).");
            return BadRequest(result.Errors.UpdateModelState(ModelState));
        }

        if (result.IsConflict) {
            _logger.LogDebug("Fail to create game system (conflict).");
            return Conflict("A game system with same name already exists.");
        }

        var response = result.Value!.ToResponse();
        _logger.LogDebug("Game system '{id}' created successfully.", response.Id);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a game system",
                      Description = "Updates an existing game system with the given ID using the provided request data.",
                      OperationId = "UpdateGameSystem")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameSystemResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromRoute]
        [SwaggerParameter("The id of the game system.", Required = true)]
        string id,
        [FromBody]
        [SwaggerParameter("Updated game system data.", Required = true)]
        GameSystemRequest request,
        CancellationToken cancellationToken = default) {
        _logger.LogDebug("Update game system '{id}' requested.", id);
        if (!Base64Guid.TryParse(id, out var uuid)) {
            ModelState.AddModelError("id", "Not a valid base64 uuid.");
            return BadRequest(ModelState);
        }

        var model = request.ToDomain(uuid);
        var result = await _handler.UpdateAsync(model, cancellationToken);
        if (result.HasErrors) {
            _logger.LogDebug("Fail to update game system '{id}' (bad request).", id);
            return BadRequest(result.Errors.UpdateModelState(ModelState));
        }

        if (result.IsNotFound) {
            _logger.LogDebug("Fail to update game system '{id}' (not found).", id);
            return NotFound();
        }

        var response = result.Value!.ToResponse();
        _logger.LogDebug("Game system '{id}' updated successfully.", id);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Remove a game system",
                      Description = "Removes an existing game system with the given ID.",
                      OperationId = "RemoveGameSystem")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Remove(
        [FromRoute]
        [SwaggerParameter("The id of the game system.", Required = true)]
        string id) {
        _logger.LogDebug("Remove game system '{id}' requested.", id);
        if (!Base64Guid.TryParse(id, out var uuid)) {
            ModelState.AddModelError("id", "Not a valid base64 uuid.");
            return BadRequest(ModelState);
        }

        var result = _handler.Remove(uuid);
        if (result.IsNotFound) {
            _logger.LogDebug("Fail to remove game system '{id}' (not found).", id);
            return NotFound();
        }

        _logger.LogDebug("Game system '{id}' removed successfully.", id);
        return Ok();
    }
}