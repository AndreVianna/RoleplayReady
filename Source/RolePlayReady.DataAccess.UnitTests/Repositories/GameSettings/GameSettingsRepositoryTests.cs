using Attribute = RolePlayReady.Models.Attribute;

namespace RolePlayReady.DataAccess.Repositories.GameSettings;

public class GameSettingsRepositoryTests
{
    private readonly IDataFileRepository _dataFileRepository;
    private readonly GameSettingsRepository _repository;

    public GameSettingsRepositoryTests()
    {
        _dataFileRepository = Substitute.For<IDataFileRepository>();
        _repository = new GameSettingsRepository(_dataFileRepository);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsAllSettings()
    {
        // Arrange
        var dataFiles = GenerateDataFiles();
        _dataFileRepository.GetAllAsync<SettingDataModel>("System", string.Empty).Returns(dataFiles);

        // Act
        var settings = await _repository.GetManyAsync("System");

        // Assert
        settings.Count().Should().Be(dataFiles.Length);
    }

    [Fact]
    public async Task GetByIdAsync_SettingFound_ReturnsSetting()
    {
        // Arrange
        var dataFile = GenerateDataFile();
        var tokenSource = new CancellationTokenSource();
        _dataFileRepository.GetByIdAsync<SettingDataModel>("System", string.Empty, dataFile.Name, tokenSource.Token).Returns(dataFile);

        // Act
        var setting = await _repository.GetByIdAsync("System", dataFile.Name, tokenSource.Token);

        // Assert
        setting.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_SettingNotFound_ReturnsNull()
    {
        // Arrange
        _dataFileRepository.GetByIdAsync<SettingDataModel>("System", string.Empty, "nonExistent").Returns((DataFile<SettingDataModel>?)null);

        // Act
        var setting = await _repository.GetByIdAsync("System", "nonExistent");

        // Assert
        setting.Should().BeNull();
    }

    [Fact]
    public async Task UpsertAsync_InsertsNewSetting()
    {
        // Arrange
        var setting = GenerateSetting();

        // Act
        await _repository.UpsertAsync("System", setting);

        // Assert
        await _dataFileRepository.Received().UpsertAsync("System", string.Empty, setting.DataFileName, Arg.Any<SettingDataModel>());
    }

    [Fact]
    public async Task UpsertAsync_UpdatesExistingSetting()
    {
        // Arrange
        var setting = GenerateSetting();

        // Act
        await _repository.UpsertAsync("System", setting);

        // Assert
        await _dataFileRepository.Received().UpsertAsync("System", string.Empty, setting.DataFileName, Arg.Any<SettingDataModel>());
    }

    [Fact]
    public void Delete_RemovesSetting()
    {
        // Arrange
        var id = "testSetting";

        // Act
        _repository.Delete("System", id);

        // Assert
        _dataFileRepository.Received().Delete("System", string.Empty, id);
    }

    private static DataFile<SettingDataModel>[] GenerateDataFiles()
        => new[] { GenerateDataFile() };

    private static DataFile<SettingDataModel> GenerateDataFile()
        => new()
        {
            Name = "SomeId",
            Timestamp = DateTime.Now,
            Content = new SettingDataModel
            {
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


    private static RolePlayReady.Models.GameSetting GenerateSetting()
        => new()
        {
            ShortName = "SomeId",
            Timestamp = DateTime.Now,
            Name = "Some Name",
            Description = "Some Description",
            Tags = new[] { "SomeTag" },
            AttributeDefinitions = new IAttribute[] {
            new Attribute {
                Name = "Some Name",
                Description = "Some Description",
                DataType = typeof(int)
            },
        }
        };

}