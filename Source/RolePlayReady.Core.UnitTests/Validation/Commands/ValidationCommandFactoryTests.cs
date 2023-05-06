namespace System.Validation.Commands;

public class ValidationCommandFactoryTests {
    private static object?[] Args(params object?[] args) => args;

    private class TestDataForValidValidators : TheoryData<string, Type, object?[], object, object> {
        public TestDataForValidValidators() {
            Add(nameof(IsLessThanCommand<int>), typeof(IsLessThanCommand<decimal>), Args(20.0m), 10m, 30m);
            Add(nameof(IsLessThanCommand<int>), typeof(IsLessThanCommand<int>), Args(20), 10, 30);
            Add(nameof(IsGreaterThanCommand<int>), typeof(IsGreaterThanCommand<int>), Args(20), 30, 10);
            Add(nameof(IsEqualToCommand<int>), typeof(IsEqualToCommand<int>), Args(20), 20, 10);

            Add(nameof(IsOneOfCommand<int>), typeof(IsOneOfCommand<string>), Args("Abc", "Def"), "Abc", "Xyz");
            Add(nameof(LengthIsCommand), typeof(LengthIsCommand), Args(5), "AbcDe", "Abc");
            Add(nameof(MaximumLengthIsCommand), typeof(MaximumLengthIsCommand), Args(5), "Abc", "AbcDef");
            Add(nameof(MinimumLengthIsCommand), typeof(MinimumLengthIsCommand), Args(5), "AbcDef", "Abc");

            Add(nameof(ContainsCommand), typeof(ContainsCommand), Args( "cDe" ), "AbcDef", "Abc");
            Add(nameof(ContainsCommand<int>), typeof(ContainsCommand<int>), Args(20), new List<int> { 20 }, new List<int> { 10 });
            Add(nameof(CountIsCommand<int>), typeof(CountIsCommand<int>), Args(3), new List<int> { 1, 2, 3 }, new List<int> { 1 });
            Add(nameof(CountIsCommand<int>), typeof(CountIsCommand<string>), Args(3), new List<string> { "A", "B", "C" }, new List<string> { "A" });
            Add(nameof(MaximumCountIsCommand<int>), typeof(MaximumCountIsCommand<string>), Args(2), new List<string> { "A", "B" }, new List<string> { "A", "B", "C" });
            Add(nameof(MinimumCountIsCommand<int>), typeof(MinimumCountIsCommand<string>), Args(2), new List<string> { "A", "B" }, new List<string> { "A" });

            Add(nameof(CountIsCommand<int>), typeof(CountIsCommand<KeyValuePair<string, int>>), Args(2), new Dictionary<string, int> { ["A"] = 1, ["B"] = 2 }, new Dictionary<string, int> { ["A"] = 1 });
            Add(nameof(CountIsCommand<int>), typeof(CountIsCommand<KeyValuePair<string, decimal>>), Args(2), new Dictionary<string, decimal> { ["A"] = 1m, ["B"] = 2m }, new Dictionary<string, decimal> { ["A"] = 1m });
            Add(nameof(CountIsCommand<int>), typeof(CountIsCommand<KeyValuePair<string, string>>), Args(2), new Dictionary<string, string> { ["A"] = "1", ["B"] = "2" }, new Dictionary<string, string> { ["A"] = "1" });
            Add(nameof(MaximumCountIsCommand<int>), typeof(MaximumCountIsCommand<KeyValuePair<string, string>>), Args(2), new Dictionary<string, string> { ["A"] = "1", ["B"] = "2" }, new Dictionary<string, string> { ["A"] = "1", ["B"] = "2", ["C"] = "3" });
            Add(nameof(MinimumCountIsCommand<int>), typeof(MinimumCountIsCommand<KeyValuePair<string, string>>), Args(2), new Dictionary<string, string> { ["A"] = "1", ["B"] = "2" }, new Dictionary<string, string> { ["A"] = "1" });
        }
    }

    [Theory]
    [ClassData(typeof(TestDataForValidValidators))]
    public void Create_ForType_ReturnsValidator(
        string validatorName,
        Type validatorType,
        object?[] args,
        object validValue,
        object invalidValue) {

        var valid = ValidationCommandFactory.For(validValue, "Attribute").Create(validatorName, args);
        var validResult = valid.Validate();
        var invalid = ValidationCommandFactory.For(invalidValue, "Attribute").Create(validatorName, args);
        var invalidResult = invalid.Validate();

        valid.Should().BeOfType(validatorType);
        validResult.IsSuccess.Should().BeTrue();
        invalidResult.IsInvalid.Should().BeTrue();
    }

    private class TestDataForInvalidValidators : TheoryData<object> {
        public TestDataForInvalidValidators() {
            Add(42.0m);
            Add(42);
            Add("42");
            Add(new List<int> { 42 });
            Add(new List<string> { "42" });
            Add(new Dictionary<string, int> { ["42"] = 42 });
            Add(new Dictionary<string, decimal> { ["42"] = 42.0m });
            Add(new Dictionary<string, string> { ["42"] = "42" });
            Add(new Dictionary<double, double> { [42.0] = 42.0 });
        }
    }

    [Theory]
    [ClassData(typeof(TestDataForInvalidValidators))]
    public void Create_ForUnsupportedValidator_Throws(object value) {
        //Act
        var action = () => ValidationCommandFactory.For(value, "Attribute").Create("Anything", Array.Empty<object>());

        //Assert
        action.Should().Throw<InvalidOperationException>();
    }
}