namespace RolePlayReady.DataAccess.Repositories.GameSystems;

public class GameSystemRepositoryTests {
    private readonly ITrackedJsonFileRepository<GameSystemData> _files;
    private readonly GameSystemRepository _repository;

    public GameSystemRepositoryTests() {
        _files = Substitute.For<ITrackedJsonFileRepository<GameSystemData>>();
        _repository = new(_files);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsAll() {
        // Arrange
        var dataFiles = GenerateList();
        _files.GetAllAsync(string.Empty).Returns(dataFiles);

        // Act
        var settings = await _repository.GetManyAsync();

        // Assert
        settings.Should().HaveCount(dataFiles.Length);
    }

    [Fact]
    public async Task GetByIdAsync_SystemFound_ReturnsSystem() {
        // Arrange
        var dataFile = GeneratePersisted();
        var tokenSource = new CancellationTokenSource();
        _files.GetByIdAsync(string.Empty, dataFile.Id, tokenSource.Token).Returns(dataFile);

        // Act
        var setting = await _repository.GetByIdAsync(dataFile.Id, tokenSource.Token);

        // Assert
        setting.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_SystemNotFound_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        _files.GetByIdAsync(string.Empty, id, Arg.Any<CancellationToken>()).Returns(default(GameSystemData));

        // Act
        var setting = await _repository.GetByIdAsync(id);

        // Assert
        setting.Should().BeNull();
    }

    [Fact]
    public async Task InsertAsync_InsertsNewSystem() {
        // Arrange
        var id = Guid.NewGuid();
        var input = GenerateInput(id);
        var expected = GeneratePersisted(id);
        _files.InsertAsync(string.Empty, Arg.Any<GameSystemData>()).Returns(expected);

        // Act
        var result = await _repository.InsertAsync(input);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingSystem() {
        // Arrange
        var id = Guid.NewGuid();
        var input = GenerateInput(id, State.Hidden);
        var expected = GeneratePersisted(id, State.Hidden);
        _files.UpdateAsync(string.Empty, Arg.Any<GameSystemData>()).Returns(expected);

        // Act
        var result = await _repository.UpdateAsync(input);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_WithNonExistingId_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        var input = GenerateInput(id, State.Hidden);
        _files.UpdateAsync(string.Empty, Arg.Any<GameSystemData>()).Returns(default(GameSystemData));

        // Act
        var result = await _repository.UpdateAsync(input);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Delete_RemovesSystem() {
        // Arrange
        var id = Guid.NewGuid();
        _files.Delete(string.Empty, id).Returns(Result.AsSuccess());

        // Act
        var result = _repository.Delete(id);

        // Assert
        result.Should().BeTrue();
    }

    private static GameSystemData[] GenerateList()
        => new[] { GeneratePersisted() };

    private static GameSystemData GeneratePersisted(Guid? id = null, State? state = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            State = state ?? State.New,
            ShortName = "SomeId",
            Name = "Some Id",
            Description = "Some Description",
            Tags = new[] { "SomeTag" },
        };

    private static GameSystem GenerateInput(Guid? id = null, State? state = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            State = state ?? State.New,
            ShortName = "SomeId",
            Name = "Some Id",
            Description = "Some Description",
            Tags = new[] { "SomeTag" },
            Domains = new[] {
                new Base {
                    Name = "Dom1",
                    Description = "SomeDescription",
                },
                new Base {
                    Name = "Domain1",
                    ShortName = "Dom2",
                    Description = "SomeDescription",
                }
            },
        };
}