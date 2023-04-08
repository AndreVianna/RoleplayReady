namespace RolePlayReady.DataAccess;

public class RuleSetRepositoryTests {
    private readonly IDataFileRepository _dataFileRepository;
    private readonly RuleSetRepository _ruleSetRepository;

    public RuleSetRepositoryTests() {
        _dataFileRepository = Substitute.For<IDataFileRepository>();
        _ruleSetRepository = new RuleSetRepository(_dataFileRepository);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsAllRuleSets() {
        // Arrange
        var dataFiles = GenerateDataFiles();
        _dataFileRepository.GetAllAsync<RuleSetDataModel>(default).Returns(dataFiles);

        // Act
        var ruleSets = await _ruleSetRepository.GetManyAsync();

        // Assert
        ruleSets.Count().Should().Be(dataFiles.Count());
    }

    [Fact]
    public async Task GetByIdAsync_RuleSetFound_ReturnsRuleSet() {
        // Arrange
        var dataFile = GenerateDataFile();
        _dataFileRepository.GetByIdAsync<RuleSetDataModel>(dataFile.Id, default).Returns(dataFile);

        // Act
        var ruleSet = await _ruleSetRepository.GetByIdAsync(dataFile.Id);

        // Assert
        ruleSet.Should().NotBeNull();
        ruleSet!.ShortName.Should().Be(dataFile.Id);
    }

    [Fact]
    public async Task GetByIdAsync_RuleSetNotFound_ReturnsNull() {
        // Arrange
        _dataFileRepository.GetByIdAsync<RuleSetDataModel>("nonexistent", default).Returns((DataFile<RuleSetDataModel>?)null);

        // Act
        var ruleSet = await _ruleSetRepository.GetByIdAsync("nonexistent");

        // Assert
        ruleSet.Should().BeNull();
    }

    [Fact]
    public async Task UpsertAsync_InsertsNewRuleSet() {
        // Arrange
        var ruleSet = GenerateRuleSet();

        // Act
        await _ruleSetRepository.UpsertAsync(ruleSet);

        // Assert
        await _dataFileRepository.Received().UpsertAsync<RuleSetDataModel>(ruleSet.ShortName, Arg.Any<RuleSetDataModel>(), default);
    }

    [Fact]
    public async Task UpsertAsync_UpdatesExistingRuleSet() {
        // Arrange
        var ruleSet = GenerateRuleSet();

        // Act
        await _ruleSetRepository.UpsertAsync(ruleSet);

        // Assert
        await _dataFileRepository.Received().UpsertAsync<RuleSetDataModel>(ruleSet.ShortName, Arg.Any<RuleSetDataModel>(), default);
    }

    [Fact]
    public void Delete_RemovesRuleSet() {
        // Arrange
        var id = "testRuleSet";

        // Act
        _ruleSetRepository.Delete(id);

        // Assert
        _dataFileRepository.Received().Delete(id);
    }

    private static DataFile<RuleSetDataModel>[] GenerateDataFiles()
        => new[] { GenerateDataFile() };

    private static DataFile<RuleSetDataModel> GenerateDataFile()
        => new() {
            Id = "SomeId",
            Timestamp = DateTime.Now,
            Content = new RuleSetDataModel {
                Name = "Some Name",
                Description = "Some Description",
                Tags = new[] { "SomeTag" },
                AttributeDefinitions = new[] {
                    new RuleSetDataModel.Attribute {
                        Name = "Some Name",
                        Description = "Some Description",
                        DataType = nameof(Int32)
                    },
                }
            }
        };


    private static RuleSet GenerateRuleSet()
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