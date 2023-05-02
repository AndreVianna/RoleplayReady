namespace RolePlayReady.Handlers.GameSystem;

public class GameSystemsHandlerTests {
    private readonly GameSystemHandler _handler;
    private readonly IGameSystemRepository _repository;

    public GameSystemsHandlerTests() {
        _repository = Substitute.For<IGameSystemRepository>();
        _handler = new(_repository);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsSystems() {
        // Arrange
        var expected = new[] { CreateRow() };
        _repository.GetManyAsync(Arg.Any<CancellationToken>()).Returns(expected);

        // Act
        var result = await _handler.GetManyAsync();

        // Assert
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsSystem() {
        // Arrange
        var id = Guid.NewGuid();
        var expected = CreateInput(id);
        _repository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(expected);

        // Act
        var result = await _handler.GetByIdAsync(id);

        // Assert
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNotFound() {
        // Arrange
        var id = Guid.NewGuid();
        _repository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(default(GameSystem));

        // Act
        var result = await _handler.GetByIdAsync(id);

        // Assert
        result.IsNotFound.Should().BeTrue();
    }

    [Fact]
    public async Task AddAsync_ReturnsSystem() {
        // Arrange
        var input = CreateInput();
        _repository.AddAsync(input, Arg.Any<CancellationToken>()).Returns(input);

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
        result.HasErrors.Should().BeFalse();
         result.IsConflict.Should().BeFalse();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task AddAsync_ForExistingId_ReturnsConflict() {
        // Arrange
        var input = CreateInput();
        _repository.AddAsync(input, Arg.Any<CancellationToken>()).Returns(default(GameSystem));

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
        result.HasErrors.Should().BeFalse();
        result.IsConflict.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task AddAsync_WithValidationError_ReturnsFailure() {
        // Arrange
        var input = CreateInput();
        input = input with { Name = "" };

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
        result.HasErrors.Should().BeTrue();
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsSystem() {
        // Arrange
        var id = Guid.NewGuid();
        var input = CreateInput(id);
        _repository.UpdateAsync(input, Arg.Any<CancellationToken>()).Returns(input);

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_WithInvalidId_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        var input = CreateInput(id);
        _repository.UpdateAsync(input, Arg.Any<CancellationToken>()).Returns(default(GameSystem));

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsNotFound.Should().BeTrue();
        result.Value.Should().Be(input);
    }

    [Fact]
    public async Task UpdateAsync_WithValidationError_ReturnsFailure() {
        // Arrange
        var input = CreateInput();
        input = input with { Name = "" };

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.HasErrors.Should().BeTrue();
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void Remove_ReturnsTrue() {
        // Arrange
        var id = Guid.NewGuid();
        _repository.Remove(id).Returns(true);

        // Act
        var result = _handler.Remove(id);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Remove_WithInvalidId_ReturnsTrue() {
        // Arrange
        var id = Guid.NewGuid();
        _repository.Remove(id).Returns(false);

        // Act
        var result = _handler.Remove(id);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    private static Row CreateRow(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Name = "Some Name",
        };

    private static GameSystem CreateInput(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Name = "Some Name",
            ShortName = "SM",
            Description = "Some description."
        };
}