namespace RolePlayReady.DataAccess.Repositories.GameSystems;

public class GameSystemRepositoryTests {
    private readonly IJsonFileHandler<GameSystemData> _files;
    private readonly GameSystemRepository _repository;

    public GameSystemRepositoryTests() {
        _files = Substitute.For<IJsonFileHandler<GameSystemData>>();
        var userAccessor = Substitute.For<IUserAccessor>();
        userAccessor.BaseFolder.Returns("User1234");
        _repository = new(_files, new GameSystemMapper(), userAccessor);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsAll() {
        // Arrange
        var dataFiles = GenerateList();
        _files.GetAllAsync().Returns(dataFiles);

        // Act
        var settings = await _repository.GetManyAsync();

        // Assert
        settings.Should().HaveCount(dataFiles.Length);
    }

    [Fact]
    public async Task GetByIdAsync_SystemFound_ReturnsSystem() {
        // Arrange
        var dataFile = GenerateData();
        var tokenSource = new CancellationTokenSource();
        _files.GetByIdAsync(dataFile.Id, tokenSource.Token).Returns(dataFile);

        // Act
        var setting = await _repository.GetByIdAsync(dataFile.Id, tokenSource.Token);

        // Assert
        setting.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_SystemNotFound_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        _files.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(default(GameSystemData));

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
        var expected = GenerateData(id);
        _files.CreateAsync(Arg.Any<GameSystemData>()).Returns(expected);

        // Act
        var result = await _repository.AddAsync(input);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingSystem() {
        // Arrange
        var id = Guid.NewGuid();
        var input = GenerateInput(id);
        var expected = GenerateData(id);
        _files.UpdateAsync(Arg.Any<GameSystemData>()).Returns(expected);

        // Act
        var result = await _repository.UpdateAsync(input);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_WithNonExistingId_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        var input = GenerateInput(id);
        _files.UpdateAsync(Arg.Any<GameSystemData>()).Returns(default(GameSystemData));

        // Act
        var result = await _repository.UpdateAsync(input);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Delete_RemovesSystem() {
        // Arrange
        var id = Guid.NewGuid();
        _files.Delete(id).Returns(ValidationResult.AsSuccess());

        // Act
        var result = _repository.Remove(id);

        // Assert
        result.Should().BeTrue();
    }

    private static GameSystemData[] GenerateList()
        => new[] { GenerateData() };

    private static GameSystemData GenerateData(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            State = State.New,
            ShortName = "SomeId",
            Name = "Some Id",
            Description = "Some Description",
            Tags = new[] { "SomeTag" },
        };

    private static GameSystem GenerateInput(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            State = State.New,
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