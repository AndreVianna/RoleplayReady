using RolePlayReady.DataAccess.Repositories.Users;

namespace RolePlayReady.DataAccess.Repositories.Domains;

public class DomainRepositoryTests {
    private readonly IJsonFileHandler<DomainData> _files;
    private readonly DomainRepository _repository;

    public DomainRepositoryTests() {
        _files = Substitute.For<IJsonFileHandler<DomainData>>();
        var userAccessor = Substitute.For<IUserAccessor>();
        userAccessor.BaseFolder.Returns("User1234");
        _repository = new(_files, new DomainMapper(), userAccessor);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsAllDomains() {
        // Arrange
        var dataFiles = GenerateList();
        _files.GetAllAsync().Returns(dataFiles);

        // Act
        var domains = await _repository.GetManyAsync();

        // Assert
        domains.Should().HaveCount(dataFiles.Length);
    }

    [Fact]
    public async Task GetByIdAsync_DomainFound_ReturnsDomain() {
        // Arrange
        var dataFile = GeneratePersisted();
        var tokenSource = new CancellationTokenSource();
        _files.GetByIdAsync(dataFile.Id, tokenSource.Token).Returns(dataFile);

        // Act
        var domain = await _repository.GetByIdAsync(dataFile.Id, tokenSource.Token);

        // Assert
        domain.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_DomainNotFound_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        _files.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(default(DomainData));

        // Act
        var domain = await _repository.GetByIdAsync(id);

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
        _files.CreateAsync(Arg.Any<DomainData>(), tokenSource.Token).Returns(expected);

        // Act
        var result = await _repository.AddAsync(domain, tokenSource.Token);

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
        _files.UpdateAsync(Arg.Any<DomainData>(), tokenSource.Token).Returns(expected);

        // Act
        var result = await _repository.UpdateAsync(domain, tokenSource.Token);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_WithNonExistingId_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        var domain = GenerateInput(id);
        var tokenSource = new CancellationTokenSource();
        _files.UpdateAsync(Arg.Any<DomainData>(), tokenSource.Token).Returns(default(DomainData));

        // Act
        var result = await _repository.UpdateAsync(domain, tokenSource.Token);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Delete_RemovesDomain() {
        // Arrange
        var id = Guid.NewGuid();
        _files.Delete(id).Returns(ValidationResult.AsSuccess());

        // Act
        var result = _repository.Remove(id);

        // Assert
        result.Should().BeTrue();
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