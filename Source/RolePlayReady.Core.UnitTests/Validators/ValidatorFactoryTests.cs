namespace System.Validators;

public class ValidatorFactoryTests {
    [Fact]
    public void Create_ForCollection_ReturnsValidator() {
        var validator = ValidatorFactory.For("Dexterity").Create("int", "LowerThan", 20);

        validator.Should().NotBeNull();
    }
}