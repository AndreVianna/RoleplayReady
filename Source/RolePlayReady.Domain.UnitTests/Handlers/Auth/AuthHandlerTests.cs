namespace RolePlayReady.Handlers.Auth;

public class AuthHandlerTests {
    private readonly AuthHandler _handler;
    private static readonly string _validUser = "some.user@host.com";
    private static readonly string _validPassword = "Secret1234!";
    private static readonly string _invalidPassword = "Invalid";
    private readonly IUserRepository _repository;
    private static readonly IHasher _hasher = Substitute.For<IHasher>();
    private static readonly HashedSecret _validSecret = new HashedSecret("ValidHashedPassword=", "SomeSalt");

    public AuthHandlerTests() {
        _hasher.HashSecret(Arg.Any<string>()).Returns(_validSecret);
        _hasher.VerifySecret("RightPassword", Arg.Any<HashedSecret>()).Returns(true);
        _hasher.VerifySecret("WrongPassword", Arg.Any<HashedSecret>()).Returns(false);


        _repository = Substitute.For<IUserRepository>();
        var configuration = Substitute.For<IConfiguration>();
        configuration["Security:DefaultUser:Id"].Returns("a8788588-929a-4859-83d4-c106b30e3afd");
        configuration["Security:DefaultUser:Name"].Returns("Some User");
        configuration["Security:DefaultUser:FolderName"].Returns("SomeUser123");
        configuration["Security:DefaultUser:Email"].Returns(_validUser);
        configuration["Security:DefaultUser:Password"].Returns(_validPassword);
        configuration["Security:IssuerSigningKey"].Returns("12345678901234567890123456789012");
        configuration["Security:TokenExpirationInHours"].Returns("7");
        var dateTime = Substitute.For<IDateTime>();
        dateTime.Now.Returns(DateTime.UtcNow);

        _handler = new AuthHandler(_repository, _hasher, configuration, dateTime, NullLogger<AuthHandler>.Instance);
    }

    private class TestLoginData : TheoryData<SignIn, bool, string[]> {
        public TestLoginData() {
            //Add(new() { Email = _validUser, Password = _validPassword }, true, Array.Empty<string>());
            Add(new() { Email = _validUser, Password = _invalidPassword }, false, Array.Empty<string>());
            Add(new() { Email = "invalid.user@email.com", Password = _validPassword }, false, Array.Empty<string>());
            Add(new() { Email = null!, Password = null! }, false, new[] { "'Email' cannot be null.", "'Password' cannot be null." });
        }
    }

    [Theory]
    [ClassData(typeof(TestLoginData))]
    public async Task SignInAsync_AuthenticatesUserLogin(SignIn signIn, bool isSuccess, string[] errors) {
        // Act
        var result = await _handler.SignInAsync(signIn);

        // Assert
        result.IsSuccess.Should().Be(isSuccess);
        result.Errors.Select(i => i.Message).Should().BeEquivalentTo(errors);
    }

    private class TestRegisterData : TheoryData<SignOn, bool, string[]> {
        public TestRegisterData() {
            Add(new() { Email = _validUser, Password = _validPassword }, true, Array.Empty<string>());
            //Add(new() { Email = "duplicated.user@email.com", Password = _validPassword }, false, Array.Empty<string>());
            Add(new() { Email = null!, Password = null! }, false, new[] { "'Email' cannot be null.", "'Password' cannot be null." });
        }
    }

    [Theory]
    [ClassData(typeof(TestRegisterData))]
    public async Task Register_AddsUser(SignOn signOn, bool isSuccess, string[] errors) {
        // Arrange
        var input = CreateInput();
        _repository.AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()).Returns(input);

        // Act
        var result = await _handler.RegisterAsync(signOn);

        // Assert
        result.IsSuccess.Should().Be(isSuccess);
        result.Errors.Select(i => i.Message).Should().BeEquivalentTo(errors);
    }


    [Fact]
    public async Task GetManyAsync_ReturnsUsers() {
        // Arrange
        var expected = new[] { CreateRow() };
        _repository.GetManyAsync(Arg.Any<CancellationToken>()).Returns(expected);

        // Act
        var result = await _handler.GetManyAsync();

        // Assert
        result.IsInvalid.Should().BeFalse();
        result.Value.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsUser() {
        // Arrange
        var id = Guid.NewGuid();
        var expected = CreateInput(id);
        _repository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(expected);

        // Act
        var result = await _handler.GetByIdAsync(id);

        // Assert
        result.IsNotFound.Should().BeFalse();
        result.IsInvalid.Should().BeFalse();
        result.Value.Should().Be(expected);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNotFound() {
        // Arrange
        var id = Guid.NewGuid();
        _repository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(default(User));

        // Act
        var result = await _handler.GetByIdAsync(id);

        // Assert
        result.IsNotFound.Should().BeTrue();
        result.IsInvalid.Should().BeFalse();
        result.Value.Should().BeNull();
    }

    [Fact]
    public async Task AddAsync_ReturnsUser() {
        // Arrange
        var input = CreateInput();
        _repository.AddAsync(input, Arg.Any<CancellationToken>()).Returns(input);

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
        result.IsInvalid.Should().BeFalse();
        result.IsConflict.Should().BeFalse();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task AddAsync_ForExistingId_ReturnsConflict() {
        // Arrange
        var input = CreateInput();
        _repository.AddAsync(input, Arg.Any<CancellationToken>()).Returns(default(User));

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
        result.IsInvalid.Should().BeFalse();
        result.IsConflict.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task AddAsync_WithErrors_ReturnsFailure() {
        // Arrange
        var input = new User {
            Id = Guid.NewGuid(),
            Email = null!,
            HashedPassword = null,
        };

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
        result.IsInvalid.Should().BeTrue();
        result.Invoking(x => x.IsConflict).Should().Throw<InvalidOperationException>();
        result.Value.Should().Be(input);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsUser() {
        // Arrange
        var id = Guid.NewGuid();
        var input = CreateInput(id);
        _repository.UpdateAsync(input, Arg.Any<CancellationToken>()).Returns(input);

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.IsInvalid.Should().BeFalse();
        result.IsNotFound.Should().BeFalse();
        result.Value.Should().Be(input);
    }

    [Fact]
    public async Task UpdateAsync_WithInvalidId_ReturnsNotFound() {
        // Arrange
        var id = Guid.NewGuid();
        var input = CreateInput(id);
        _repository.UpdateAsync(input, Arg.Any<CancellationToken>()).Returns(default(User));

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.IsInvalid.Should().BeFalse();
        result.IsNotFound.Should().BeTrue();
        result.Value.Should().Be(input);
    }

    [Fact]
    public async Task UpdateAsync_WithErrors_ReturnsFailure() {
        // Arrange
        var input = new User {
            Id = Guid.NewGuid(),
            Email = null!,
        };

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.IsInvalid.Should().BeTrue();
        result.Invoking(x => x.IsNotFound).Should().Throw<InvalidOperationException>();
        result.Value.Should().Be(input);
    }

    [Fact]
    public void Remove_ReturnsTrue() {
        // Arrange
        var id = Guid.NewGuid();
        _repository.Remove(id).Returns(true);

        // Act
        var result = _handler.Remove(id);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Remove_WithInvalidId_ReturnsNotFound() {
        // Arrange
        var id = Guid.NewGuid();
        _repository.Remove(id).Returns(false);

        // Act
        var result = _handler.Remove(id);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsNotFound.Should().BeTrue();
    }

    private static UserRow CreateRow(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Name = "Some User",
            Email = "some.user@email.com",
        };

    private static User CreateInput(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Email = "some.user@email.com",
            Name = "Some User",
            Birthday = DateOnly.FromDateTime(DateTime.Today.AddYears(-30)),
        };
}