namespace RolePlayReady.Handlers.Sphere;

public class SphereHandlerTests {
    private readonly SphereHandler _handler;
    private readonly ISphereRepository _repository;

    public SphereHandlerTests() {
        _repository = Substitute.For<ISphereRepository>();
        _handler = new(_repository);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsSpheres() {
        // Arrange
        var expected = new[] { CreateRow() };
        _repository.GetManyAsync(Arg.Any<CancellationToken>()).Returns(expected);

        // Act
        var result = await _handler.GetManyAsync();

        // Assert
        result.HasErrors.Should().BeFalse();
        result.Value.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsSphere() {
        // Arrange
        var id = Guid.NewGuid();
        var expected = CreateInput(id);
        _repository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(expected);

        // Act
        var result = await _handler.GetByIdAsync(id);

        // Assert
        result.IsNotFound.Should().BeFalse();
        result.HasErrors.Should().BeFalse();
        result.Value.Should().Be(expected);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNotFound() {
        // Arrange
        var id = Guid.NewGuid();
        _repository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(default(Sphere));

        // Act
        var result = await _handler.GetByIdAsync(id);

        // Assert
        result.IsNotFound.Should().BeTrue();
        result.HasErrors.Should().BeFalse();
        result.Value.Should().BeNull();
    }

    [Fact]
    public async Task AddAsync_ReturnsSphere() {
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
        _repository.AddAsync(input, Arg.Any<CancellationToken>()).Returns(default(Sphere));

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
        result.HasErrors.Should().BeFalse();
        result.IsConflict.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task AddAsync_WithErrors_ReturnsFailure() {
        // Arrange
        var input = new Sphere {
            Name = null!,
            Description = null!,
        };

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
        result.HasErrors.Should().BeTrue();
        result.Invoking(x => x.IsConflict).Should().Throw<InvalidOperationException>();
        result.Value.Should().Be(input);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsSphere() {
        // Arrange
        var id = Guid.NewGuid();
        var input = CreateInput(id);
        _repository.UpdateAsync(input, Arg.Any<CancellationToken>()).Returns(input);

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.HasErrors.Should().BeFalse();
        result.IsNotFound.Should().BeFalse();
        result.Value.Should().Be(input);
    }

    [Fact]
    public async Task UpdateAsync_WithInvalidId_ReturnsNotFound() {
        // Arrange
        var id = Guid.NewGuid();
        var input = CreateInput(id);
        _repository.UpdateAsync(input, Arg.Any<CancellationToken>()).Returns(default(Sphere));

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.HasErrors.Should().BeFalse();
        result.IsNotFound.Should().BeTrue();
        result.Value.Should().Be(input);
    }

    [Fact]
    public async Task UpdateAsync_WithErrors_ReturnsFailure() {
        // Arrange
        var input = new Sphere {
            Name = null!,
            Description = null!,
        };

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.HasErrors.Should().BeTrue();
        result.Invoking(x => x.IsNotFound).Should().Throw<InvalidOperationException>();
        result.Value.Should().Be(input);
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
    public void Remove_WithInvalidId_ReturnsNotFound() {
        // Arrange
        var id = Guid.NewGuid();
        _repository.Remove(id).Returns(false);

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

    private static Sphere CreateInput(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            ShortName = "SM",
            Name = "Some Name",
            Description = "Some description.",
            AttributeDefinitions = Array.Empty<AttributeDefinition>(),
        };
}