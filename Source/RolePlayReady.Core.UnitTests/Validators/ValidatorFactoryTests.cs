namespace System.Validators;

public class ValidatorFactoryTests {
    private class TestDataForValidValidators : TheoryData<Type, string, Type, object[], object, object> {
        public TestDataForValidValidators() {
            Add(typeof(decimal), nameof(IsLessThan<decimal>), typeof(IsLessThan<decimal>), new object[] { 20.0m }, 10m, 30m);
            Add(typeof(int), nameof(IsLessThan<int>), typeof(IsLessThan<int>), new object[] { 20 }, 10, 30);
            Add(typeof(int), nameof(IsGreaterThan<int>), typeof(IsGreaterThan<int>), new object[] { 20 }, 30, 10);
            Add(typeof(int), nameof(MinimumIs<int>), typeof(MinimumIs<int>), new object[] { 20 }, 30, 10);
            Add(typeof(int), nameof(MaximumIs<int>), typeof(MaximumIs<int>), new object[] { 20 }, 10, 30);
            Add(typeof(int), nameof(IsEqualTo<int>), typeof(IsEqualTo<int>), new object[] { 20 }, 20, 10);

            Add(typeof(string), nameof(IsOneOf), typeof(IsOneOf), new object[] { "Abc", "Def" }, "Abc", "Xyz");
            Add(typeof(string), nameof(LengthIs), typeof(LengthIs), new object[] { 5 }, "AbcDe", "Abc");
            Add(typeof(string), nameof(MaximumLengthIs), typeof(MaximumLengthIs), new object[] { 5 }, "Abc", "AbcDef");
            Add(typeof(string), nameof(MinimumLengthIs), typeof(MinimumLengthIs), new object[] { 5 }, "AbcDef", "Abc");

            Add(typeof(List<int>), nameof(NotContains<int>), typeof(NotContains<int>), new object[] { 20 }, new List<int> { 10 }, new List<int> { 20 });
            Add(typeof(List<int>), nameof(Contains<int>), typeof(Contains<int>), new object[] { 20 }, new List<int> { 20 }, new List<int> { 10 });
            Add(typeof(List<int>), nameof(CountIs<int>), typeof(CountIs<int>), new object[] { 3 }, new List<int> { 1, 2, 3 }, new List<int> { 1 });
            Add(typeof(List<string>), nameof(CountIs<string>), typeof(CountIs<string>), new object[] { 3 }, new List<string> { "A", "B", "C" }, new List<string> { "A" });
            Add(typeof(List<string>), nameof(MaximumCountIs<string>), typeof(MaximumCountIs<string>), new object[] { 2 }, new List<string> { "A", "B" }, new List<string> { "A", "B", "C" });
            Add(typeof(List<string>), nameof(MinimumCountIs<string>), typeof(MinimumCountIs<string>), new object[] { 2 }, new List<string> { "A", "B" }, new List<string> { "A" });

            Add(typeof(Dictionary<string, int>), nameof(CountIs<KeyValuePair<string, int>>), typeof(CountIs<KeyValuePair<string, int>>), new object[] { 2 }, new Dictionary<string, int> { ["A"] = 1, ["B"] = 2 }, new Dictionary<string, int> { ["A"] = 1 });
            Add(typeof(Dictionary<string, decimal>), nameof(CountIs<KeyValuePair<string, decimal>>), typeof(CountIs<KeyValuePair<string, decimal>>), new object[] { 2 }, new Dictionary<string, decimal> { ["A"] = 1m, ["B"] = 2m }, new Dictionary<string, decimal> { ["A"] = 1m });
            Add(typeof(Dictionary<string, string>), nameof(CountIs<KeyValuePair<string, string>>), typeof(CountIs<KeyValuePair<string, string>>), new object[] { 2 }, new Dictionary<string, string> { ["A"] = "1", ["B"] = "2" }, new Dictionary<string, string> { ["A"] = "1" });
            Add(typeof(Dictionary<string, string>), nameof(MaximumCountIs<KeyValuePair<string, int>>), typeof(MaximumCountIs<KeyValuePair<string, string>>), new object[] { 2 }, new Dictionary<string, string> { ["A"] = "1", ["B"] = "2" }, new Dictionary<string, string> { ["A"] = "1", ["B"] = "2", ["C"] = "3" });
            Add(typeof(Dictionary<string, string>), nameof(MinimumCountIs<KeyValuePair<string, int>>), typeof(MinimumCountIs<KeyValuePair<string, string>>), new object[] { 2 }, new Dictionary<string, string> { ["A"] = "1", ["B"] = "2" }, new Dictionary<string, string> { ["A"] = "1" });
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

        var validator = ValidatorFactory.For("Attribute").Create(valueType, validatorName, args);
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
        var action = () => ValidatorFactory.For("Attribute").Create(valueType, "Anything", Array.Empty<object>());

        //Assert
        action.Should().Throw<InvalidOperationException>();
    }
}