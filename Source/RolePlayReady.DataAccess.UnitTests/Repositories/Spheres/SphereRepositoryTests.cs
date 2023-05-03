using RolePlayReady.DataAccess.Repositories.Domains;

namespace RolePlayReady.DataAccess.Repositories.Spheres;

public class SphereRepositoryTests {
    private readonly IJsonFileStorage<SphereData> _storage;
    private readonly SphereRepository _repository;

    public SphereRepositoryTests() {
        _storage = Substitute.For<IJsonFileStorage<SphereData>>();
        var userAccessor = Substitute.For<IUserAccessor>();
        userAccessor.BaseFolder.Returns("User1234");
        _repository = new(_storage, userAccessor);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsAllDomains() {
        // Arrange
        var dataFiles = GenerateList();
        _storage.GetAllAsync().Returns(dataFiles);

        // Act
        var domains = await _repository.GetManyAsync();

        // Assert
        domains.Should().HaveCount(dataFiles.Length);
    }

    [Fact]
    public async Task GetByIdAsync_DomainFound_ReturnsDomain() {
        // Arrange
        var dataFile = GenerateData();
        var tokenSource = new CancellationTokenSource();
        _storage.GetByIdAsync(dataFile.Id, tokenSource.Token).Returns(dataFile);

        // Act
        var domain = await _repository.GetByIdAsync(dataFile.Id, tokenSource.Token);

        // Assert
        domain.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_DomainNotFound_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        _storage.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(default(SphereData));

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
        var expected = GenerateData(id);
        var tokenSource = new CancellationTokenSource();
        _storage.CreateAsync(Arg.Any<SphereData>(), tokenSource.Token).Returns(expected);

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
        var expected = GenerateData(id);
        var tokenSource = new CancellationTokenSource();
        _storage.UpdateAsync(Arg.Any<SphereData>(), tokenSource.Token).Returns(expected);

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
        _storage.UpdateAsync(Arg.Any<SphereData>(), tokenSource.Token).Returns(default(SphereData));

        // Act
        var result = await _repository.UpdateAsync(domain, tokenSource.Token);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Delete_RemovesDomain() {
        // Arrange
        var id = Guid.NewGuid();
        _storage.Delete(id).Returns(true);

        // Act
        var result = _repository.Remove(id);

        // Assert
        result.Should().BeTrue();
    }

    private static SphereData[] GenerateList()
        => new[] { GenerateData() };

    private static SphereData GenerateData(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            State = State.New,
            Name = "Some Id",
            Description = "Some Description",
            Tags = new[] { "SomeTag" },
            AttributeDefinitions = new[] {
                new SphereData.AttributeDefinitionData {
                    Name = "Some Id",
                    Description = "Some Description",
                    DataType = typeof(int).GetName()
                },
            }
        };

    private static Sphere GenerateInput(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            State = State.New,
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