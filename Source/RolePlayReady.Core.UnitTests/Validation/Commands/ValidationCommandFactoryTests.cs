namespace System.Validation.Commands;

public class ValidationCommandFactoryTests {
    private class TestDataForValidValidators : TheoryData<Type, string, Type, object[], object, object> {
        public TestDataForValidValidators() {
            Add(typeof(decimal), nameof(IsLessThanCommand<decimal>), typeof(IsLessThanCommand<decimal>), new object[] { 20.0m }, 10m, 30m);
            Add(typeof(int), nameof(IsLessThanCommand<int>), typeof(IsLessThanCommand<int>), new object[] { 20 }, 10, 30);
            Add(typeof(int), nameof(IsGreaterThanCommand<int>), typeof(IsGreaterThanCommand<int>), new object[] { 20 }, 30, 10);
            Add(typeof(int), nameof(MinimumIs<int>), typeof(MinimumIs<int>), new object[] { 20 }, 30, 10);
            Add(typeof(int), nameof(MaximumIs<int>), typeof(MaximumIs<int>), new object[] { 20 }, 10, 30);
            Add(typeof(int), nameof(IsEqualToCommand<int>), typeof(IsEqualToCommand<int>), new object[] { 20 }, 20, 10);

            Add(typeof(string), nameof(IsOneOfCommand<>), typeof(IsOneOfCommand<>), new object[] { "Abc", "Def" }, "Abc", "Xyz");
            Add(typeof(string), nameof(LengthIsCommand), typeof(LengthIsCommand), new object[] { 5 }, "AbcDe", "Abc");
            Add(typeof(string), nameof(MaximumLengthIsCommand), typeof(MaximumLengthIsCommand), new object[] { 5 }, "Abc", "AbcDef");
            Add(typeof(string), nameof(MinimumLengthIsCommand), typeof(MinimumLengthIsCommand), new object[] { 5 }, "AbcDef", "Abc");

            Add(typeof(List<int>), nameof(NotContains<int>), typeof(NotContains<int>), new object[] { 20 }, new List<int> { 10 }, new List<int> { 20 });
            Add(typeof(List<int>), nameof(ContainsCommand<int>), typeof(ContainsCommand<int>), new object[] { 20 }, new List<int> { 20 }, new List<int> { 10 });
            Add(typeof(List<int>), nameof(CountIsCommand<int>), typeof(CountIsCommand<int>), new object[] { 3 }, new List<int> { 1, 2, 3 }, new List<int> { 1 });
            Add(typeof(List<string>), nameof(CountIsCommand<string>), typeof(CountIsCommand<string>), new object[] { 3 }, new List<string> { "A", "B", "C" }, new List<string> { "A" });
            Add(typeof(List<string>), nameof(MaximumCountIsCommand<string>), typeof(MaximumCountIsCommand<string>), new object[] { 2 }, new List<string> { "A", "B" }, new List<string> { "A", "B", "C" });
            Add(typeof(List<string>), nameof(MinimumCountIsCommand<string>), typeof(MinimumCountIsCommand<string>), new object[] { 2 }, new List<string> { "A", "B" }, new List<string> { "A" });

            Add(typeof(Dictionary<string, int>), nameof(CountIsCommand<KeyValuePair<string, int>>), typeof(CountIsCommand<KeyValuePair<string, int>>), new object[] { 2 }, new Dictionary<string, int> { ["A"] = 1, ["B"] = 2 }, new Dictionary<string, int> { ["A"] = 1 });
            Add(typeof(Dictionary<string, decimal>), nameof(CountIsCommand<KeyValuePair<string, decimal>>), typeof(CountIsCommand<KeyValuePair<string, decimal>>), new object[] { 2 }, new Dictionary<string, decimal> { ["A"] = 1m, ["B"] = 2m }, new Dictionary<string, decimal> { ["A"] = 1m });
            Add(typeof(Dictionary<string, string>), nameof(CountIsCommand<KeyValuePair<string, string>>), typeof(CountIsCommand<KeyValuePair<string, string>>), new object[] { 2 }, new Dictionary<string, string> { ["A"] = "1", ["B"] = "2" }, new Dictionary<string, string> { ["A"] = "1" });
            Add(typeof(Dictionary<string, string>), nameof(MaximumCountIsCommand<KeyValuePair<string, int>>), typeof(MaximumCountIsCommand<KeyValuePair<string, string>>), new object[] { 2 }, new Dictionary<string, string> { ["A"] = "1", ["B"] = "2" }, new Dictionary<string, string> { ["A"] = "1", ["B"] = "2", ["C"] = "3" });
            Add(typeof(Dictionary<string, string>), nameof(MinimumCountIsCommand<KeyValuePair<string, int>>), typeof(MinimumCountIsCommand<KeyValuePair<string, string>>), new object[] { 2 }, new Dictionary<string, string> { ["A"] = "1", ["B"] = "2" }, new Dictionary<string, string> { ["A"] = "1" });
        }
    }

    [Theory]
    [ClassData(typeof(TestDataForValidValidators))]
    public void Create_ForType_ReturnsValidator(
        Type valueType,
        string validatorName,
        Type validatorType,
        object[] args,
        object validValue,
        object invalidValue) {

        var validator = ValidationCommandFactory.For("Attribute").Create(valueType, validatorName, args);
        var validResult = validator.Validate(validValue);
        var invalidResult = validator.Validate(invalidValue);

        validator.Should().BeOfType(validatorType);
        validResult.IsSuccess.Should().BeTrue();
        invalidResult.IsInvalid.Should().BeTrue();
    }

    private class TestDataForInvalidValidators : TheoryData<Type> {
        public TestDataForInvalidValidators() {
            Add(typeof(decimal));
            Add(typeof(int));
            Add(typeof(string));
            Add(typeof(List<int>));
            Add(typeof(List<string>));
            Add(typeof(Dictionary<string, int>));
            Add(typeof(Dictionary<string, decimal>));
            Add(typeof(Dictionary<string, string>));
            Add(typeof(Dictionary<double, double>));
        }
    }

    [Theory]
    [ClassData(typeof(TestDataForInvalidValidators))]
    public void Create_ForUnsupportedValidator_Throws(Type valueType) {
        //Act
        var action = () => ValidationCommandFactory.For("Attribute").Create(valueType, "Anything", Array.Empty<object>());

        //Assert
        action.Should().Throw<InvalidOperationException>();
    }
}