using RolePlayReady.Models.Abstractions;

using static RolePlayReady.Constants.Common;

namespace RolePlayReady.DataAccess.Repositories.GameSettings;

public class GameSettingsRepositoryTests {
    private readonly IDataFileRepository _dataFileRepository;
    private readonly GameSettingsRepository _repository;

    public GameSettingsRepositoryTests() {
        _dataFileRepository = Substitute.For<IDataFileRepository>();
        _repository = new GameSettingsRepository(_dataFileRepository);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsAllSettings() {
        // Arrange
        var dataFiles = GenerateDataFiles();
        _dataFileRepository.GetAllAsync<SettingDataModel>(InternalUser, string.Empty).Returns(dataFiles);

        // Act
        var settings = await _repository.GetManyAsync(InternalUser);

        // Assert
        settings.Count().Should().Be(dataFiles.Length);
    }

    [Fact]
    public async Task GetByIdAsync_SettingFound_ReturnsSetting() {
        // Arrange
        var dataFile = GenerateDataFile();
        var tokenSource = new CancellationTokenSource();
        _dataFileRepository.GetByIdAsync<SettingDataModel>(InternalUser, string.Empty, dataFile.Name, tokenSource.Token).Returns(dataFile);

        // Act
        var setting = await _repository.GetByIdAsync(InternalUser, dataFile.Name, tokenSource.Token);

        // Assert
        setting.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_SettingNotFound_ReturnsNull() {
        // Arrange
        _dataFileRepository.GetByIdAsync<SettingDataModel>(InternalUser, string.Empty, "nonExistent").Returns((DataFile<SettingDataModel>?)null);

        // Act
        var setting = await _repository.GetByIdAsync(InternalUser, "nonExistent");

        // Assert
        setting.Should().BeNull();
    }

    [Fact]
    public async Task UpsertAsync_InsertsNewSetting() {
        // Arrange
        var setting = GenerateSetting();

        // Act
        await _repository.UpsertAsync(InternalUser, setting);

        // Assert
        await _dataFileRepository.Received().UpsertAsync(InternalUser, string.Empty, setting.DataFileName, Arg.Any<SettingDataModel>());
    }

    [Fact]
    public async Task UpsertAsync_UpdatesExistingSetting() {
        // Arrange
        var setting = GenerateSetting();

        // Act
        await _repository.UpsertAsync(InternalUser, setting);

        // Assert
        await _dataFileRepository.Received().UpsertAsync(InternalUser, string.Empty, setting.DataFileName, Arg.Any<SettingDataModel>());
    }

    [Fact]
    public void Delete_RemovesSetting() {
        // Arrange
        var id = "testSetting";

        // Act
        _repository.Delete(InternalUser, id);

        // Assert
        _dataFileRepository.Received().Delete(InternalUser, string.Empty, id);
    }

    private static DataFile<SettingDataModel>[] GenerateDataFiles()
        => new[] { GenerateDataFile() };

    private static DataFile<SettingDataModel> GenerateDataFile()
        => new() {
            Name = "SomeId",
            Timestamp = DateTime.Now,
            Content = new SettingDataModel {
                Name = "Some Name",
                Description = "Some Description",
                Tags = new[] { "SomeTag" },
                AttributeDefinitions = new[] {
                    new SettingDataModel.Attribute {
                        Name = "Some Name",
                        Description = "Some Description",
                        DataType = nameof(Int32)
                    },
                }
            }
        };


    private static RolePlayReady.Models.GameSystemSetting GenerateSetting()
        => new() {
            ShortName = "SomeId",
            Timestamp = DateTime.Now,
            Name = "Some Name",
            Description = "Some Description",
            Tags = new[] { "SomeTag" },
            AttributeDefinitions = new IAttributeDefinition[] {
            new AttributeDefinition {
                Name = "Some Name",
                Description = "Some Description",
                DataType = typeof(int)
            },
        }
        };

}