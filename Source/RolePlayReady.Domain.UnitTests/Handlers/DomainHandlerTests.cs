namespace RolePlayReady.Handlers;

public class DomainHandlerTests {
    private readonly DomainHandler _handler;
    private readonly IDomainRepository _repository;

    public DomainHandlerTests() {
        _repository = Substitute.For<IDomainRepository>();
        _handler = new(_repository);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsDomains() {
        // Arrange
        var expected = new[] { CreateRow() };
        _repository.GetManyAsync(Arg.Any<CancellationToken>()).Returns(expected);

        // Act
        var result = await _handler.GetManyAsync();

        // Assert
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsDomain() {
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
        _repository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(default(Domain));

        // Act
        var result = await _handler.GetByIdAsync(id);

        // Assert
        result.IsNotFound.Should().BeTrue();
    }

    [Fact]
    public async Task AddAsync_ReturnsDomain() {
        // Arrange
        var input = CreateInput();
        _repository.InsertAsync(input, Arg.Any<CancellationToken>()).Returns(input);

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
        result.HasErrors.Should().BeFalse();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task AddAsync_WithErrors_ReturnsFailure() {
        // Arrange
        var input = new Domain {
            Name = null!,
            Description = null!,
        };

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
        result.HasErrors.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_ReturnsDomain() {
        // Arrange
        var id = Guid.NewGuid();
        var input = CreateInput(id);
        _repository.UpdateAsync(input, Arg.Any<CancellationToken>()).Returns(input);

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.HasErrors.Should().BeFalse();
        result.IsNotFound.Should().BeFalse();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_WithInvalidId_ReturnsNotFound() {
        // Arrange
        var id = Guid.NewGuid();
        var input = CreateInput(id);
        _repository.UpdateAsync(input, Arg.Any<CancellationToken>()).Returns(default(Domain));

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.HasErrors.Should().BeTrue();
        result.IsNotFound.Should().BeTrue();
        result.Value.Should().Be(input);
    }

    [Fact]
    public async Task UpdateAsync_WithErrors_ReturnsFailure() {
        // Arrange
        var input = new Domain {
            Name = null!,
            Description = null!,
        };

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.HasErrors.Should().BeTrue();
        result.IsNotFound.Should().BeFalse();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public void Remove_ReturnsTrue() {
        // Arrange
        var id = Guid.NewGuid();
        _repository.Delete(id).Returns(true);

        // Act
        var result = _handler.Remove(id);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Remove_WithInvalidId_ReturnsNotFound() {
        // Arrange
        var id = Guid.NewGuid();
        _repository.Delete(id).Returns(false);

        // Act
        var result = _handler.Remove(id);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsNotFound.Should().BeTrue();
    }

    private static Row CreateRow(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Name = "Some Name",
        };

    private static Domain CreateInput(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            ShortName = "SM",
            Name = "Some Name",
            Description = "Some description.",
            AttributeDefinitions = Array.Empty<AttributeDefinition>(),
        };
}