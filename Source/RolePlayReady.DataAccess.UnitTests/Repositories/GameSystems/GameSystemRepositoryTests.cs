using static RolePlayReady.Constants.Constants;

namespace RolePlayReady.DataAccess.Repositories.GameSystems;

public class GameSystemsRepositoryTests {
    private readonly ITrackedJsonFileRepository<GameSystemData> _files;
    private readonly GameSystemsRepository _repository;

    public GameSystemsRepositoryTests() {
        _files = Substitute.For<ITrackedJsonFileRepository<GameSystemData>>();
        _repository = new(_files);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsAllSettings() {
        // Arrange
        var dataFiles = GenerateList();
        _files.GetAllAsync(InternalUser, string.Empty).Returns(dataFiles);

        // Act
        var settings = await _repository.GetManyAsync(InternalUser);

        // Assert
        settings.HasValue.Should().BeTrue();
        settings.Value.Count().Should().Be(dataFiles.Length);
    }

    [Fact]
    public async Task GetByIdAsync_SettingFound_ReturnsSetting() {
        // Arrange
        var dataFile = GeneratePersisted();
        var tokenSource = new CancellationTokenSource();
        _files.GetByIdAsync(InternalUser, string.Empty, dataFile.Id, tokenSource.Token).Returns(dataFile);

        // Act
        var setting = await _repository.GetByIdAsync(InternalUser, dataFile.Id, tokenSource.Token);

        // Assert
        setting.HasValue.Should().BeTrue();
        setting.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_SettingNotFound_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        _files.GetByIdAsync(InternalUser, string.Empty, id, Arg.Any<CancellationToken>()).Returns((Persisted<GameSystemData>?)null);

        // Act
        var setting = await _repository.GetByIdAsync(InternalUser, id);

        // Assert
        setting.HasValue.Should().BeFalse();
        setting.IsNull.Should().BeTrue();
    }

    [Fact]
    public async Task InsertAsync_InsertsNewSetting() {
        // Arrange
        var input = GenerateInput();
        var expected = GeneratePersisted();
        _files.UpsertAsync(InternalUser, string.Empty, Arg.Any<Guid>(), Arg.Any<GameSystemData>()).Returns(expected);

        // Act
        var result = await _repository.InsertAsync(InternalUser, input);

        // Assert
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingSetting() {
        // Arrange
        var input = GenerateInput();
        var expected = GeneratePersisted();
        _files.UpsertAsync(InternalUser, string.Empty, Arg.Any<Guid>(), Arg.Any<GameSystemData>()).Returns(expected);

        // Act
        var result = await _repository.UpdateAsync(InternalUser, Guid.NewGuid(), input);

        // Assert
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public void Delete_RemovesSetting() {
        // Arrange
        var id = Guid.NewGuid();
        _files.Delete(InternalUser, string.Empty, id).Returns<Result<bool>>(true);

        // Act
        var result = _repository.Delete(InternalUser, id);

        // Assert
        result.HasValue.Should().BeTrue();
    }

    private static Persisted<GameSystemData>[] GenerateList()
        => new[] { GeneratePersisted() };

    private static Persisted<GameSystemData> GeneratePersisted()
        => new() {
            Id = Guid.NewGuid(),
            Timestamp = DateTime.Now,
            Content = new() {
                ShortName = "SomeId",
                Name = "Some Id",
                Description = "Some Description",
                Tags = new[] { "SomeTag" },
                Domains = new[] { "SomeDomain" },
            }
        };

    private static GameSystem GenerateInput()
        => new() {
            ShortName = "SomeId",
            Name = "Some Id",
            Description = "Some Description",
            Tags = new[] { "SomeTag" },
            Domains = new[] { new Domain {
                Name = "SomeName",
                Description = "SomeDescription",
            } },
        };
}