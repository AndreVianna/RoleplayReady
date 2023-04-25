namespace RolePlayReady.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[SwaggerTag("Manages game systems.")]
public class SystemsController : ControllerBase {
    private readonly IGameSystemHandler _handler;
    private readonly ILogger _logger;

    public SystemsController(IGameSystemHandler handler, ILogger<SystemsController> logger) {
        _handler = handler;
        _logger = logger;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all game systems",
                      Description = "Retrieves a collection of game systems.",
                      OperationId = "GetGameSystemById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameSystemRowModel[]))]
    public async Task<IActionResult> GetMany(CancellationToken cancellationToken = default) {
        _logger.LogDebug("Getting all game systems requested.");
        var result = await _handler.GetManyAsync(cancellationToken);
        var response = result.Value.ToResponse();
        _logger.LogDebug("{count} game systems retrieved successfully.", response.Length);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Get a game system",
                      Description = "Retrieves a game system by its ID.",
                      OperationId = "GetGameSystemById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameSystemModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute]
        [SwaggerParameter("The id of the game system.", Required = true)]
        Guid id,
        CancellationToken cancellationToken = default) {
        _logger.LogDebug("Getting game system '{id}' requested.", id);
        var result = await _handler.GetByIdAsync(id, cancellationToken);
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
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GameSystemModel))]
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

        var response = result.Value.ToResponse();
        _logger.LogDebug("Game system '{id}' created successfully.", response.Id);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(Summary = "Update a game system",
                      Description = "Updates an existing game system with the given ID using the provided request data.",
                      OperationId = "UpdateGameSystem")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameSystemModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromRoute]
        [SwaggerParameter("The id of the game system.", Required = true)]
        Guid id,
        [FromBody]
        [SwaggerParameter("Updated game system data.", Required = true)]
        GameSystemRequest request,
        CancellationToken cancellationToken = default) {
        _logger.LogDebug("Update game system '{id}' requested.", id);
        var model = request.ToDomain(id);
        var result = await _handler.UpdateAsync(model, cancellationToken);
        if (result.HasErrors) {
            _logger.LogDebug("Fail to update game system '{id}' (bad request).", id);
            return BadRequest(result.Errors.UpdateModelState(ModelState));
        }

        if (result.Value is null) {
            _logger.LogDebug("Fail to update game system '{id}' (not found).", id);
            return NotFound();
        }

        var response = result.Value.ToResponse();
        _logger.LogDebug("Game system '{id}' updated successfully.", id);
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Remove a game system",
                      Description = "Removes an existing game system with the given ID.",
                      OperationId = "RemoveGameSystem")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Remove(
        [FromRoute]
        [SwaggerParameter("The id of the game system.", Required = true)]
        Guid id) {
        _logger.LogDebug("Remove game system '{id}' requested.", id);
        var result = _handler.Remove(id);
        if (!result.IsTrue) {
            _logger.LogDebug("Fail to remove game system '{id}' (not found).", id);
            return NotFound();
        }

        _logger.LogDebug("Game system '{id}' removed successfully.", id);
        return Ok();
    }
}