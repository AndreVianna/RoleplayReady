using RolePlayReady.Models.Attributes;

using static RolePlayReady.Constants.Constants;

namespace RolePlayReady.DataAccess.Repositories.GameSystemSettings;

public class GameSystemSettingsRepositoryTests {
    private readonly ITrackedJsonFileRepository _files;
    private readonly GameSystemSettingsRepository _repository;

    public GameSystemSettingsRepositoryTests() {
        _files = Substitute.For<ITrackedJsonFileRepository>();
        _repository = new(_files);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsAllSettings() {
        // Arrange
        var dataFiles = GenerateDataFiles();
        _files.GetAllAsync<GameSystemSettingDataModel>(InternalUser, string.Empty).Returns(dataFiles);

        // Act
        var settings = await _repository.GetManyAsync(InternalUser);

        // Assert
        settings.HasValue.Should().BeTrue();
        settings.Value.Count().Should().Be(dataFiles.Length);
    }

    [Fact]
    public async Task GetByIdAsync_SettingFound_ReturnsSetting() {
        // Arrange
        var dataFile = GenerateDataFile();
        var tokenSource = new CancellationTokenSource();
        _files.GetByIdAsync<GameSystemSettingDataModel>(InternalUser, string.Empty, dataFile.Name, tokenSource.Token).Returns(dataFile);

        // Act
        var setting = await _repository.GetByIdAsync(InternalUser, Guid.Parse(dataFile.Name), tokenSource.Token);

        // Assert
        setting.HasValue.Should().BeTrue();
        setting.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_SettingNotFound_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        _files.GetByIdAsync<GameSystemSettingDataModel>(InternalUser, string.Empty, id.ToString(), Arg.Any<CancellationToken>()).Returns((DataFile<GameSystemSettingDataModel>?)null);

        // Act
        var setting = await _repository.GetByIdAsync(InternalUser, id);

        // Assert
        setting.HasValue.Should().BeFalse();
        setting.IsNull.Should().BeTrue();
    }

    [Fact]
    public async Task InsertAsync_InsertsNewSetting() {
        // Arrange
        var setting = GenerateSetting();
        var tokenSource = new CancellationTokenSource();
        _files.UpsertAsync(InternalUser, string.Empty, Arg.Any<string>(), Arg.Any<GameSystemSettingDataModel>(), tokenSource.Token).Returns(DateTime.Now);

        // Act
        var result = await _repository.InsertAsync(InternalUser, setting, tokenSource.Token);

        // Assert
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingSetting() {
        // Arrange
        var setting = GenerateSetting();
        var tokenSource = new CancellationTokenSource();
        _files.UpsertAsync(InternalUser, string.Empty, setting.Id.ToString(), Arg.Any<GameSystemSettingDataModel>(), tokenSource.Token).Returns(DateTime.Now);

        // Act
        var result = await _repository.UpdateAsync(InternalUser, setting, tokenSource.Token);

        // Assert
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public void Delete_RemovesSetting() {
        // Arrange
        var id = Guid.NewGuid();
        _files.Delete(InternalUser, string.Empty, id.ToString()).Returns<Result<bool>>(true);

        // Act
        var result = _repository.Delete(InternalUser, id);

        // Assert
        result.HasValue.Should().BeTrue();
    }

    private static DataFile<GameSystemSettingDataModel>[] GenerateDataFiles()
        => new[] { GenerateDataFile() };

    private static DataFile<GameSystemSettingDataModel> GenerateDataFile()
        => new() {
            Name = Guid.NewGuid().ToString(),
            Timestamp = DateTime.Now,
            Content = new() {
                Name = "Some Name",
                Description = "Some Description",
                Tags = new[] { "SomeTag" },
                AttributeDefinitions = new[] {
                    new GameSystemSettingDataModel.AttributeDefinition {
                        Name = "Some Name",
                        Description = "Some Description",
                        DataType = nameof(Int32)
                    },
                }
            }
        };

    private static GameSystemSetting GenerateSetting()
        => new() {
            Id = Guid.NewGuid(),
            ShortName = "SomeId",
            Timestamp = DateTime.Now,
            Name = "Some Name",
            Description = "Some Description",
            Tags = new[] { "SomeTag" },
            AttributeDefinitions = new AttributeDefinition[] {
            new() {
                Name = "Some Name",
                Description = "Some Description",
                DataType = typeof(int)
            },
        }
        };

}