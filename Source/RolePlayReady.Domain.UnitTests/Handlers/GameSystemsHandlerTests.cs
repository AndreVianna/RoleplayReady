using RolePlayReady.Repositories.Abstractions;
using RolePlayReady.Security.Abstractions;

using static RolePlayReady.Constants.Constants;

namespace RolePlayReady.Handlers;

public class GameSystemsHandlerTests {
    private readonly GameSystemHandler _handler;
    private readonly IGameSystemsRepository _repository;

    public GameSystemsHandlerTests() {
        _repository = Substitute.For<IGameSystemsRepository>();
        var userAccessor = Substitute.For<IUserAccessor>();
        userAccessor.Id.Returns(InternalUser);
        _handler = new(_repository, userAccessor);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsSettings() {
        // Arrange
        var expectedSettings = new[] { CreateRow() };
        _repository.GetManyAsync(InternalUser, Arg.Any<CancellationToken>()).Returns(expectedSettings);

        // Act
        var result = await _handler.GetManyAsync();

        // Assert
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsSetting() {
        // Arrange
        var id = Guid.NewGuid();
        var record = CreateInput();
        var expected = CreatePersisted(record, id);
        _repository.GetByIdAsync(InternalUser, id, Arg.Any<CancellationToken>()).Returns(expected);

        // Act
        var result = await _handler.GetByIdAsync(id);

        // Assert
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public async Task AddAsync_ReturnsSetting() {
        // Arrange
        var input = CreateInput();
        var expected = CreatePersisted(input);
        _repository.InsertAsync(InternalUser, input, Arg.Any<CancellationToken>()).Returns(expected);

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
        result.HasValue.Should().BeTrue();
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
    public async Task UpdateAsync_ReturnsSetting() {
        // Arrange
        var input = CreateInput();
        var id = Guid.NewGuid();
        var expected = CreatePersisted(input, id);
        _repository.UpdateAsync(InternalUser, id, input, Arg.Any<CancellationToken>()).Returns(expected);

        // Act
        var result = await _handler.UpdateAsync(id, input);

        // Assert
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateAsync_WithValidationError_ReturnsFailure() {
        // Arrange
        var input = CreateInput();
        input = input with { Name = "" };

        // Act
        var result = await _handler.UpdateAsync(Guid.NewGuid(), input);

        // Assert
        result.HasErrors.Should().BeTrue();
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void Remove_ReturnsTrue() {
        // Arrange
        var id = Guid.NewGuid();
        _repository.Delete(InternalUser, id).Returns<Result<bool>>(true);

        // Act
        var result = _handler.Remove(id);

        // Assert
        result.HasValue.Should().BeTrue();
    }

    private static Row CreateRow(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Name = "Some Name",
        };

    private static GameSystem CreateInput()
        => new() {
            Name = "Some Name",
            ShortName = "SM",
            Description = "Some description."
        };

    private static Persisted<GameSystem> CreatePersisted(GameSystem content, Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Content = content,
        };
}