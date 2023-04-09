namespace RolePlayReady.DataAccess.Repositories;

public class SettingRepositoryTests {
    private readonly IDataFileRepository _dataFileRepository;
    private readonly SettingRepository _settingRepository;

    public SettingRepositoryTests() {
        _dataFileRepository = Substitute.For<IDataFileRepository>();
        _settingRepository = new SettingRepository(_dataFileRepository);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsAllSettings() {
        // Arrange
        var dataFiles = GenerateDataFiles();
        _dataFileRepository.GetAllAsync<SettingDataModel>(null).Returns(dataFiles);

        // Act
        var settings = await _settingRepository.GetManyAsync();

        // Assert
        settings.Count().Should().Be(dataFiles.Count());
    }

    [Fact]
    public async Task GetByIdAsync_SettingFound_ReturnsSetting() {
        // Arrange
        var dataFile = GenerateDataFile();
        _dataFileRepository.GetByIdAsync<SettingDataModel>(null, dataFile.Id, default).Returns(dataFile);

        // Act
        var setting = await _settingRepository.GetByIdAsync(dataFile.Id);

        // Assert
        setting.Should().NotBeNull();
        setting!.ShortName.Should().Be(dataFile.Id);
    }

    [Fact]
    public async Task GetByIdAsync_SettingNotFound_ReturnsNull() {
        // Arrange
        _dataFileRepository.GetByIdAsync<SettingDataModel>(null, "nonexistent", default).Returns((DataFile<SettingDataModel>?)null);

        // Act
        var setting = await _settingRepository.GetByIdAsync("nonexistent");

        // Assert
        setting.Should().BeNull();
    }

    [Fact]
    public async Task UpsertAsync_InsertsNewSetting() {
        // Arrange
        var setting = GenerateSetting();

        // Act
        await _settingRepository.UpsertAsync(setting);

        // Assert
        await _dataFileRepository.Received().UpsertAsync(null, setting.ShortName, Arg.Any<SettingDataModel>(), default);
    }

    [Fact]
    public async Task UpsertAsync_UpdatesExistingSetting() {
        // Arrange
        var setting = GenerateSetting();

        // Act
        await _settingRepository.UpsertAsync(setting);

        // Assert
        await _dataFileRepository.Received().UpsertAsync(null, setting.ShortName, Arg.Any<SettingDataModel>(), default);
    }

    [Fact]
    public void Delete_RemovesSetting() {
        // Arrange
        var id = "testSetting";

        // Act
        _settingRepository.Delete(id);

        // Assert
        _dataFileRepository.Received().Delete(null, id);
    }

    private static DataFile<SettingDataModel>[] GenerateDataFiles()
        => new[] { GenerateDataFile() };

    private static DataFile<SettingDataModel> GenerateDataFile()
        => new() {
            Id = "SomeId",
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


    private static Setting GenerateSetting()
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