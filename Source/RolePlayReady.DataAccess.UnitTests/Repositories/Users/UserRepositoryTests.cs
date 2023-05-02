namespace RolePlayReady.DataAccess.Repositories.Users;

public class UserRepositoryTests {
    private readonly IJsonFileStorage<UserData> _storage;
    private readonly UserRepository _repository;

    public UserRepositoryTests() {
        _storage = Substitute.For<IJsonFileStorage<UserData>>();
        _repository = new(_storage);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsAll() {
        // Arrange
        var dataFiles = GenerateList();
        _storage.GetAllAsync().Returns(dataFiles);

        // Act
        var settings = await _repository.GetManyAsync();

        // Assert
        settings.Should().HaveCount(dataFiles.Length);
    }

    [Fact]
    public async Task GetByIdAsync_SystemFound_ReturnsSystem() {
        // Arrange
        var dataFile = GenerateData();
        var tokenSource = new CancellationTokenSource();
        _storage.GetByIdAsync(dataFile.Id, tokenSource.Token).Returns(dataFile);

        // Act
        var setting = await _repository.GetByIdAsync(dataFile.Id, tokenSource.Token);

        // Assert
        setting.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_SystemNotFound_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        _storage.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(default(UserData));

        // Act
        var setting = await _repository.GetByIdAsync(id);

        // Assert
        setting.Should().BeNull();
    }

    [Fact]
    public async Task InsertAsync_InsertsNewSystem() {
        // Arrange
        var id = Guid.NewGuid();
        var input = GenerateInput(id);
        var expected = GenerateData(id);
        _storage.CreateAsync(Arg.Any<UserData>()).Returns(expected);

        // Act
        var result = await _repository.AddAsync(input);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingSystem() {
        // Arrange
        var id = Guid.NewGuid();
        var input = GenerateInput(id);
        var expected = GenerateData(id);
        _storage.UpdateAsync(Arg.Any<UserData>()).Returns(expected);

        // Act
        var result = await _repository.UpdateAsync(input);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_WithNonExistingId_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        var input = GenerateInput(id);
        _storage.UpdateAsync(Arg.Any<UserData>()).Returns(default(UserData));

        // Act
        var result = await _repository.UpdateAsync(input);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Delete_RemovesSystem() {
        // Arrange
        var id = Guid.NewGuid();
        _storage.Delete(id).Returns(ValidationResult.Success);

        // Act
        var result = _repository.Remove(id);

        // Assert
        result.Should().BeTrue();
    }

    private static UserData[] GenerateList()
        => new[] { GenerateData() };

    private static UserData GenerateData(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Email = "some.user@email.com",
            PasswordHash = "PasswordHash",
            PasswordSalt = "PasswordSalt",
            Name = "Some User",
            Birthday = DateOnly.FromDateTime(DateTime.Today.AddYears(-30)),
        };

    private static User GenerateInput(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Email = "some.user@email.com",
            Name = "Some User",
            Birthday = DateOnly.FromDateTime(DateTime.Today.AddYears(-30)),
        };
}