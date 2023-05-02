using static System.Results.CrudResult;

namespace RolePlayReady.Api.Controllers;

public class GameSystemsControllerTests {
    private readonly IGameSystemHandler _handler = Substitute.For<IGameSystemHandler>();
    private static readonly ILogger<GameSystemsController> _logger = Substitute.For<ILogger<GameSystemsController>>();
    private static readonly Row[] _rows = new[] {
        new Row {
            Id = Guid.NewGuid(),
            Name = "Lairs & Lizards 3e",
        },
        new Row {
            Id = Guid.NewGuid(),
            Name = "RoadScout",
        }
    };
    private static readonly GameSystem _sample = new() {
        Id = Guid.NewGuid(),
        Name = "Lairs & Lizards 3e",
        Description = "A very nice role playing game.",
        ShortName = "LnL3e",
        Tags = new[] { "Fantasy", "Adventure" },
    };

    private readonly GameSystemsController _controller;

    public GameSystemsControllerTests() {
        _controller = new GameSystemsController(_handler, _logger);
    }

    [Fact]
    public async Task GetMany_ReturnsArrayOfGameSystemRowResponses() {
        // Arrange
        var expectedRows = _rows.ToResponse();
        _handler.GetManyAsync(Arg.Any<CancellationToken>()).Returns(SuccessFor(_rows.AsEnumerable()));

        // Act
        var response = await _controller.GetMany();

        // Assert
        var result = response.Should().BeOfType<OkObjectResult>().Subject;
        var content = result.Value.Should().BeOfType<GameSystemRowResponse[]>().Subject;
        content.Should().BeEquivalentTo(expectedRows);
    }

    [Fact]
    public async Task GetById_WithValidId_ReturnsGameSystemResponse() {
        // Arrange
        var expected = _sample.ToResponse();
        var base64Id = (Base64Guid)_sample.Id;
        _handler.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(SuccessFor(_sample));

        // Act
        var response = await _controller.GetById(base64Id);

        // Assert
        var result = response.Should().BeOfType<OkObjectResult>().Subject;
        var content = result.Value.Should().BeOfType<GameSystemResponse>().Subject;
        content.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetById_WithInvalidId_ReturnsNotFound() {
       // Act
        var response = await _controller.GetById("invalid");

        // Assert
        response.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task GetById_WithNonExistingId_ReturnsNotFound() {
        var base64Id = (Base64Guid)Guid.NewGuid();
        _handler.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(NotFoundFor(default(GameSystem)));

        // Act
        var response = await _controller.GetById(base64Id);

        // Assert
        response.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Create_WithValidData_ReturnsGameSystemResponse() {
        // Arrange
        var request = new GameSystemRequest {
            Name = "Lairs & Lizards 3e",
            Description = "A very nice role playing game.",
            ShortName = "LnL3e",
            Tags = new[] { "Fantasy", "Adventure" },
        };
        var expected = _sample.ToResponse();
        _handler.AddAsync(Arg.Any<GameSystem>(), Arg.Any<CancellationToken>())
                .Returns(SuccessFor(_sample));

        // Act
        var response = await _controller.Create(request);

        // Assert
        var result = response.Should().BeOfType<CreatedAtActionResult>().Subject;
        var content = result.Value.Should().BeOfType<GameSystemResponse>().Subject;
        content.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Create_WithExistingId_ReturnsConflict() {
        // Arrange
        var request = new GameSystemRequest {
            Name = "Lairs & Lizards 3e",
            Description = "A very nice role playing game.",
            ShortName = "LnL3e",
            Tags = new[] { "Fantasy", "Adventure" },
        };
        var expected = request.ToDomain();
        _handler.AddAsync(Arg.Any<GameSystem>(), Arg.Any<CancellationToken>())
                .Returns(ConflictFor(expected));

        // Act
        var response = await _controller.Create(request);

        // Assert
        var result = response.Should().BeOfType<ConflictObjectResult>().Subject;
        result.Value.Should().BeOfType<string>(); // just an error message.
    }

    [Fact]
    public async Task Create_WithInvalidData_ReturnsBadRequest() {
        // Act
        var request = new GameSystemRequest {
            Name = null!,
            Description = null!,
        };
        _handler.AddAsync(Arg.Any<GameSystem>(), Arg.Any<CancellationToken>())
                .Returns(InvalidFor(_sample, "Some error.", "request"));

        var response = await _controller.Create(request);

        // Assert
        var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = result.Value.Should().BeOfType<SerializableError>().Subject;
        error.Should().HaveCount(1);
    }

    [Fact]
    public async Task Update_WithValidData_ReturnsGameSystemResponse() {
        // Arrange
        var base64Id = (Base64Guid)_sample.Id;
        var request = new GameSystemRequest {
            Name = "Lairs & Lizards 3e",
            Description = "A very nice role playing game.",
            ShortName = "LnL3e",
            Tags = new[] { "Fantasy", "Adventure" },
        };
        var expected = _sample.ToResponse();
        _handler.UpdateAsync(Arg.Any<GameSystem>(), Arg.Any<CancellationToken>())
                .Returns(SuccessFor(_sample));

        // Act
        var response = await _controller.Update(base64Id, request);

        // Assert
        var result = response.Should().BeOfType<OkObjectResult>().Subject;
        var content = result.Value.Should().BeOfType<GameSystemResponse>().Subject;
        content.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Update_WithInvalidId_ReturnsBadRequest() {
        // Arrange
        var request = new GameSystemRequest {
            Name = "Lairs & Lizards 3e",
            Description = "A very nice role playing game.",
            ShortName = "LnL3e",
            Tags = new[] { "Fantasy", "Adventure" },
        };

        // Act
        var response = await _controller.Update("invalid", request);

        // Assert
        response.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Update_WithNonExistingId_ReturnsNotFound() {
        var base64Id = (Base64Guid)Guid.NewGuid();
        var request = new GameSystemRequest {
            Name = "Lairs & Lizards 3e",
            Description = "A very nice role playing game.",
            ShortName = "LnL3e",
            Tags = new[] { "Fantasy", "Adventure" },
        };
        var input = request.ToDomain();
        _handler.UpdateAsync(Arg.Any<GameSystem>(), Arg.Any<CancellationToken>())
                .Returns(NotFoundFor(input));

        // Act
        var response = await _controller.Update(base64Id, request);

        // Assert
        response.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Update_WithInvalidData_ReturnsBadRequest() {
        // Act
        var base64Id = (Base64Guid)_sample.Id;
        var request = new GameSystemRequest {
            Name = null!,
            Description = null!,
        };
        _handler.UpdateAsync(Arg.Any<GameSystem>(), Arg.Any<CancellationToken>())
                .Returns(InvalidFor(_sample, "Some error.", "request"));

        var response = await _controller.Update(base64Id, request);

        // Assert
        var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = result.Value.Should().BeOfType<SerializableError>().Subject;
        error.Should().HaveCount(1);
    }

    [Fact]
    public void Remove_WithValidId_ReturnsGameSystemResponse() {
        // Arrange
        var base64Id = (Base64Guid)_sample.Id;
        _handler.Remove(Arg.Any<Guid>()).Returns(SuccessFor);

        // Act
        var response = _controller.Remove(base64Id);

        // Assert
        response.Should().BeOfType<OkResult>();
    }

    [Fact]
    public void Remove_WithInvalidId_ReturnsNotFound() {
        // Act
        var response = _controller.Remove("invalid");

        // Assert
        response.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public void Remove_WithNonExistingId_ReturnsNotFound() {
        var base64Id = (Base64Guid)Guid.NewGuid();
        _handler.Remove(Arg.Any<Guid>()).Returns(NotFoundFor("id"));

        // Act
        var response = _controller.Remove(base64Id);

        // Assert
        response.Should().BeOfType<NotFoundResult>();
    }
}
