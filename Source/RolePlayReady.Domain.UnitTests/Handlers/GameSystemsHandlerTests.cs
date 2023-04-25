using static RolePlayReady.Constants.Constants;

namespace RolePlayReady.Handlers;

public class GameSystemsHandlerTests {
    private readonly GameSystemHandler _handler;
    private readonly IGameSystemRepository _repository;

    public GameSystemsHandlerTests() {
        _repository = Substitute.For<IGameSystemRepository>();
        var userAccessor = Substitute.For<IUserAccessor>();
        userAccessor.Id.Returns(InternalUser);
        _handler = new(_repository, userAccessor);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsSystems() {
        // Arrange
        var expected = new[] { CreateRow() };
        _repository.GetManyAsync(InternalUser, Arg.Any<CancellationToken>()).Returns(expected);

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
        _repository.GetByIdAsync(InternalUser, id, Arg.Any<CancellationToken>()).Returns(expected);

        // Act
        var result = await _handler.GetByIdAsync(id);

        // Assert
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task AddAsync_ReturnsSystem() {
        // Arrange
        var input = CreateInput();
        _repository.InsertAsync(InternalUser, input, Arg.Any<CancellationToken>()).Returns(input);

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
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
        _repository.UpdateAsync(InternalUser, input, Arg.Any<CancellationToken>()).Returns(input);

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
        _repository.UpdateAsync(InternalUser, input, Arg.Any<CancellationToken>()).Returns(default(GameSystem));

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.Value.Should().BeNull();
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
        _repository.Delete(InternalUser, id).Returns<FlagResult>(true);

        // Act
        var result = _handler.Remove(id);

        // Assert
        result.IsTrue.Should().BeTrue();
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