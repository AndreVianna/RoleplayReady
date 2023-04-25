using static RolePlayReady.Constants.Constants;

namespace RolePlayReady.DataAccess.Repositories.Domains;

public class DomainRepositoryTests {
    private readonly ITrackedJsonFileRepository<DomainData> _files;
    private readonly DomainRepository _repository;

    public DomainRepositoryTests() {
        _files = Substitute.For<ITrackedJsonFileRepository<DomainData>>();
        _repository = new(_files);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsAllDomains() {
        // Arrange
        var dataFiles = GenerateList();
        _files.GetAllAsync(InternalUser, string.Empty).Returns(dataFiles);

        // Act
        var domains = await _repository.GetManyAsync(InternalUser);

        // Assert
        domains.Should().HaveCount(dataFiles.Length);
    }

    [Fact]
    public async Task GetByIdAsync_DomainFound_ReturnsDomain() {
        // Arrange
        var dataFile = GeneratePersisted();
        var tokenSource = new CancellationTokenSource();
        _files.GetByIdAsync(InternalUser, string.Empty, dataFile.Id, tokenSource.Token).Returns(dataFile);

        // Act
        var domain = await _repository.GetByIdAsync(InternalUser, dataFile.Id, tokenSource.Token);

        // Assert
        domain.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_DomainNotFound_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        _files.GetByIdAsync(InternalUser, string.Empty, id, Arg.Any<CancellationToken>()).Returns(default(DomainData));

        // Act
        var domain = await _repository.GetByIdAsync(InternalUser, id);

        // Assert
        domain.Should().BeNull();
    }

    [Fact]
    public async Task InsertAsync_InsertsNewDomain() {
        // Arrange
        var id = Guid.NewGuid();
        var domain = GenerateInput(id);
        var expected = GeneratePersisted(id);
        var tokenSource = new CancellationTokenSource();
        _files.InsertAsync(InternalUser, string.Empty, Arg.Any<DomainData>(), tokenSource.Token).Returns(expected);

        // Act
        var result = await _repository.InsertAsync(InternalUser, domain, tokenSource.Token);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingDomain() {
        // Arrange
        var id = Guid.NewGuid();
        var domain = GenerateInput(id);
        var expected = GeneratePersisted(id);
        var tokenSource = new CancellationTokenSource();
        _files.UpdateAsync(InternalUser, string.Empty, Arg.Any<DomainData>(), tokenSource.Token).Returns(expected);

        // Act
        var result = await _repository.UpdateAsync(InternalUser, domain, tokenSource.Token);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_WithNonExistingId_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        var domain = GenerateInput(id);
        var tokenSource = new CancellationTokenSource();
        _files.UpdateAsync(InternalUser, string.Empty, Arg.Any<DomainData>(), tokenSource.Token).Returns(default(DomainData));

        // Act
        var result = await _repository.UpdateAsync(InternalUser, domain, tokenSource.Token);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Delete_RemovesDomain() {
        // Arrange
        var id = Guid.NewGuid();
        _files.Delete(InternalUser, string.Empty, id).Returns(Result.Success);

        // Act
        var result = _repository.Delete(InternalUser, id);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    private static DomainData[] GenerateList()
        => new[] { GeneratePersisted() };

    private static DomainData GeneratePersisted(Guid? id = null, State? state = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            State = state ?? State.New,
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
        };

    private static Domain GenerateInput(Guid? id = null, State? state = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            State = state ?? State.New,
            ShortName = "SomeId",
            Name = "Some Id",
            Description = "Some Description",
            Tags = new[] { "SomeTag" },
            Components = new Base[] {
                new() {
                    Name = "Comp1",
                    Description = "Some Description",
                },
                new() {
                    Name = "Component2",
                    ShortName = "Comp2",
                    Description = "Some Description",
                },
            },
            AttributeDefinitions = new AttributeDefinition[] {
                new() {
                    Name = "Some Id",
                    Description = "Some Description",
                    DataType = typeof(int)
                },
            }
        };
}