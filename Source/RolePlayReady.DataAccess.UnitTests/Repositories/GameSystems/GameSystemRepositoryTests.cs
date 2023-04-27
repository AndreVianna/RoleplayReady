namespace RolePlayReady.DataAccess.Repositories.GameSystems;

public class GameSystemRepositoryTests {
    private readonly ITrackedJsonFileRepository<GameSystemData> _files;
    private readonly GameSystemRepository _repository;
    private const string _dummyUser = "DummyUser";

    public GameSystemRepositoryTests() {
        _files = Substitute.For<ITrackedJsonFileRepository<GameSystemData>>();
        _repository = new(_files);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsAll() {
        // Arrange
        var dataFiles = GenerateList();
        _files.GetAllAsync(_dummyUser, string.Empty).Returns(dataFiles);

        // Act
        var settings = await _repository.GetManyAsync(_dummyUser);

        // Assert
        settings.Should().HaveCount(dataFiles.Length);
    }

    [Fact]
    public async Task GetByIdAsync_SystemFound_ReturnsSystem() {
        // Arrange
        var dataFile = GeneratePersisted();
        var tokenSource = new CancellationTokenSource();
        _files.GetByIdAsync(_dummyUser, string.Empty, dataFile.Id, tokenSource.Token).Returns(dataFile);

        // Act
        var setting = await _repository.GetByIdAsync(_dummyUser, dataFile.Id, tokenSource.Token);

        // Assert
        setting.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_SystemNotFound_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        _files.GetByIdAsync(_dummyUser, string.Empty, id, Arg.Any<CancellationToken>()).Returns(default(GameSystemData));

        // Act
        var setting = await _repository.GetByIdAsync(_dummyUser, id);

        // Assert
        setting.Should().BeNull();
    }

    [Fact]
    public async Task InsertAsync_InsertsNewSystem() {
        // Arrange
        var id = Guid.NewGuid();
        var input = GenerateInput(id);
        var expected = GeneratePersisted(id);
        _files.InsertAsync(_dummyUser, string.Empty, Arg.Any<GameSystemData>()).Returns(expected);

        // Act
        var result = await _repository.InsertAsync(_dummyUser, input);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingSystem() {
        // Arrange
        var id = Guid.NewGuid();
        var input = GenerateInput(id, State.Hidden);
        var expected = GeneratePersisted(id, State.Hidden);
        _files.UpdateAsync(_dummyUser, string.Empty, Arg.Any<GameSystemData>()).Returns(expected);

        // Act
        var result = await _repository.UpdateAsync(_dummyUser, input);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_WithNonExistingId_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        var input = GenerateInput(id, State.Hidden);
        _files.UpdateAsync(_dummyUser, string.Empty, Arg.Any<GameSystemData>()).Returns(default(GameSystemData));

        // Act
        var result = await _repository.UpdateAsync(_dummyUser, input);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Delete_RemovesSystem() {
        // Arrange
        var id = Guid.NewGuid();
        _files.Delete(_dummyUser, string.Empty, id).Returns(Result.Success);

        // Act
        var result = _repository.Delete(_dummyUser, id);

        // Assert
        result.IsSuccess.Should().BeTrue();
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