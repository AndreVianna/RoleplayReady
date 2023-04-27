namespace RolePlayReady.Security.Models;

public class LoginTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var login = new Login {
            Email = "user.name@email.com",
            Password = "SomePassword",
        };

        login.Should().NotBeNull();
        login.Email.Should().Be("user.name@email.com");
        login.Password.Should().Be("SomePassword");
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Validate_Validates(Login subject, bool isSuccess, int errorCount) {
        // Act
        var result = subject.Validate();

        // Assert
        result.IsSuccess.Should().Be(isSuccess);
        result.Errors.Should().HaveCount(errorCount);
    }

    private class TestData : TheoryData<Login, bool, int> {
        public TestData() {
            Add(new() { Email = "user.name@email.com", Password = "SomePassword" }, true, 0);
            Add(new() { Email = null!, Password = null! }, false, 2);
            Add(new() { Email = "", Password = "" }, false, 2);
            Add(new() { Email = "   ", Password = "   " }, false, 3);
            Add(new() { Email = "invalid", Password = new('X', Validation.Password.MaximumLength + 1) }, false, 2);
        }
    }
}