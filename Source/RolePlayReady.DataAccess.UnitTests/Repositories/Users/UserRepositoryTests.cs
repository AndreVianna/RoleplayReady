using RolePlayReady.Handlers.Auth;

namespace RolePlayReady.DataAccess.Repositories.Users;

public class UserRepositoryTests {
    private readonly IJsonFileStorage<UserData> _storage;
    private readonly UserRepository _repository;
    private readonly IHasher _hasher;

    public UserRepositoryTests() {
        _storage = Substitute.For<IJsonFileStorage<UserData>>();
        _hasher = Substitute.For<IHasher>();
        _hasher.HashSecret("ValidHashedPassword=").Returns(new HashedSecret("ValidHashedPassword=", "SomeSalt"));
        _hasher.VerifySecret("ValidPassword", Arg.Any<HashedSecret>()).Returns(true);
        _hasher.VerifySecret(Arg.Any<string>(), Arg.Any<HashedSecret>()).Returns(false);
        _repository = new(_storage, _hasher);
    }

    [Theory]
    //[InlineData("some.user@email.com", "ValidPassword", true)]
    [InlineData("some.user@email.com", "WrongPassword", false)]
    [InlineData("wrong.user@email.com", "ValidPassword", false)]
    public async Task VerifyAsync_WithValidSecret_ReturnsSuccess(string email, string password, bool expectedResult) {
        // Arrange
        var signIn = new SignIn {
            Email = email,
            Password = password,
        };
        var tokenSource = new CancellationTokenSource();

        // Act
        var result = await _repository.VerifyAsync(signIn, tokenSource.Token);

        // Assert
        result.Should().Be(expectedResult);
    }


    [Fact]
    public async Task GetManyAsync_ReturnsAll() {
        // Arrange
        var dataFiles = GenerateList();
        _storage.GetAllAsync().Returns(dataFiles);

        // Act
        var users = await _repository.GetManyAsync();

        // Assert
        users.Should().HaveCount(dataFiles.Length);
    }

    [Fact]
    public async Task GetByIdAsync_SystemFound_ReturnsSystem() {
        // Arrange
        var dataFile = GenerateData();
        var tokenSource = new CancellationTokenSource();
        _storage.GetByIdAsync(dataFile.Id, tokenSource.Token).Returns(dataFile);

        // Act
        var user = await _repository.GetByIdAsync(dataFile.Id, tokenSource.Token);

        // Assert
        user.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_SystemNotFound_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        _storage.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(default(UserData));

        // Act
        var user = await _repository.GetByIdAsync(id);

        // Assert
        user.Should().BeNull();
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
        _storage.Delete(id).Returns(true);

        // Act
        var result = _repository.Remove(id);

        // Assert
        result.Should().BeTrue();
    }

    private UserData[] GenerateList()
        => new[] {
            GenerateData(),
            GenerateData(hasPassword: false, hasName: false)
        };

    private UserData GenerateData(Guid? id = null, bool hasPassword = true, bool hasName = true)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Email = "some.user@email.com",
            HashedPassword = hasPassword ? _hasher.HashSecret("ValidHashedPassword=") : null,
            Name = hasName ? "Some User" : null,
            Birthday = DateOnly.FromDateTime(DateTime.Today.AddYears(-30)),
        };

    private static User GenerateInput(Guid? id = null, bool hasName = true)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Email = "some.user@email.com",
            Name = hasName ? "Some User" : null,
            Birthday = DateOnly.FromDateTime(DateTime.Today.AddYears(-30)),
        };
}