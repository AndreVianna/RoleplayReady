using RolePlayReady.DataAccess.Repositories.Domains;

using static RolePlayReady.Constants.Constants;

namespace RolePlayReady.DataAccess.Repositories.GameSystemSettings;

public class DomainRepositoryTests {
    private readonly ITrackedJsonFileRepository<DomainData> _files;
    private readonly DomainRepository _repository;

    public DomainRepositoryTests() {
        _files = Substitute.For<ITrackedJsonFileRepository<DomainData>>();
        _repository = new(_files);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsAllSettings() {
        // Arrange
        var dataFiles = GenerateList();
        _files.GetAllAsync(InternalUser, string.Empty).Returns(dataFiles);

        // Act
        var domains = await _repository.GetManyAsync(InternalUser);

        // Assert
        domains.HasValue.Should().BeTrue();
        domains.Value.Count().Should().Be(dataFiles.Length);
    }

    [Fact]
    public async Task GetByIdAsync_SettingFound_ReturnsSetting() {
        // Arrange
        var dataFile = GeneratePersisted();
        var tokenSource = new CancellationTokenSource();
        _files.GetByIdAsync(InternalUser, string.Empty, dataFile.Id, tokenSource.Token).Returns(dataFile);

        // Act
        var domain = await _repository.GetByIdAsync(InternalUser, dataFile.Id, tokenSource.Token);

        // Assert
        domain.HasValue.Should().BeTrue();
        domain.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_SettingNotFound_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        _files.GetByIdAsync(InternalUser, string.Empty, id, Arg.Any<CancellationToken>()).Returns((Persisted<DomainData>?)null);

        // Act
        var domain = await _repository.GetByIdAsync(InternalUser, id);

        // Assert
        domain.HasValue.Should().BeFalse();
        domain.IsNull.Should().BeTrue();
    }

    [Fact]
    public async Task InsertAsync_InsertsNewSetting() {
        // Arrange
        var domain = GenerateInput();
        var expected = GeneratePersisted();
        var tokenSource = new CancellationTokenSource();
        _files.UpsertAsync(InternalUser, string.Empty, Arg.Any<Guid>(), Arg.Any<DomainData>(), tokenSource.Token).Returns(expected);

        // Act
        var result = await _repository.InsertAsync(InternalUser, domain, tokenSource.Token);

        // Assert
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingSetting() {
        // Arrange
        var domain = GenerateInput();
        var expected = GeneratePersisted();
        var tokenSource = new CancellationTokenSource();
        _files.UpsertAsync(InternalUser, string.Empty, Arg.Any<Guid>(), Arg.Any<DomainData>(), tokenSource.Token).Returns(expected);

        // Act
        var result = await _repository.UpdateAsync(InternalUser, expected.Id, domain, tokenSource.Token);

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

    private static Persisted<DomainData>[] GenerateList()
        => new[] { GeneratePersisted() };

    private static Persisted<DomainData> GeneratePersisted()
        => new() {
            Id = Guid.NewGuid(),
            Timestamp = DateTime.Now,
            Content = new() {
                Name = "Some Id",
                Description = "Some Description",
                Tags = new[] { "SomeTag" },
                AttributeDefinitions = new[] {
                    new DomainData.AttributeDefinitionData {
                        Name = "Some Id",
                        Description = "Some Description",
                        DataType = typeof(int).GetName()
                    },
                }
            }
        };

    private static Domain GenerateInput()
        => new() {
            ShortName = "SomeId",
            Name = "Some Id",
            Description = "Some Description",
            Tags = new[] { "SomeTag" },
            AttributeDefinitions = new IAttributeDefinition[] {
                new AttributeDefinition {
                    Name = "Some Id",
                    Description = "Some Description",
                    DataType = typeof(int)
                },
            }
        };
}