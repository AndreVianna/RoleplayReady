using CommandNames = System.Constants.Constants.Commands;

namespace System.Validation.Commands;

public class ValidationCommandFactoryTests {
    private static object?[] Args(params object?[] args) => args;

    private class TestDataForCommands : TheoryData<string, Type, object?[], Type> {
        public TestDataForCommands() {
            Add(CommandNames.ContainsCommand, typeof(string), Args("cDe"), typeof(ContainsCommand));
            Add(CommandNames.ContainsCommand, typeof(List<int>), Args(20), typeof(ContainsCommand<int>));
            Add(CommandNames.CountIsCommand, typeof(List<int>), Args(3), typeof(CountIsCommand<int>));
            Add(CommandNames.CountIsCommand, typeof(Dictionary<string, decimal>), Args(2), typeof(CountIsCommand<KeyValuePair<string, decimal>>));
            Add(CommandNames.CountIsCommand, typeof(Dictionary<string, int>), Args(2), typeof(CountIsCommand<KeyValuePair<string, int>>));
            Add(CommandNames.CountIsCommand, typeof(Dictionary<string, string>), Args(2), typeof(CountIsCommand<KeyValuePair<string, string>>));
            Add(CommandNames.CountIsCommand, typeof(List<string>), Args(3), typeof(CountIsCommand<string>));
            Add(CommandNames.IsEqualToCommand, typeof(int), Args(20), typeof(IsEqualToCommand<int>));
            Add(CommandNames.IsGreaterThanCommand, typeof(int), Args(20), typeof(IsGreaterThanCommand<int>));
            Add(CommandNames.IsLessThanCommand, typeof(decimal), Args(20.0m), typeof(IsLessThanCommand<decimal>));
            Add(CommandNames.IsLessThanCommand, typeof(int), Args(20), typeof(IsLessThanCommand<int>));
            Add(CommandNames.IsOneOfCommand, typeof(string), Args("Abc", "Def"), typeof(IsOneOfCommand<string>));
            Add(CommandNames.LengthIsCommand, typeof(string), Args(5), typeof(LengthIsCommand));
            Add(CommandNames.MaximumCountIsCommand, typeof(Dictionary<string, string>), Args(2), typeof(MaximumCountIsCommand<KeyValuePair<string, string>>));
            Add(CommandNames.MaximumCountIsCommand, typeof(List<string>), Args(2), typeof(MaximumCountIsCommand<string>));
            Add(CommandNames.MaximumLengthIsCommand, typeof(string), Args(5), typeof(MaximumLengthIsCommand));
            Add(CommandNames.MinimumCountIsCommand, typeof(Dictionary<string, string>), Args(2), typeof(MinimumCountIsCommand<KeyValuePair<string, string>>));
            Add(CommandNames.MinimumCountIsCommand, typeof(List<string>), Args(2), typeof(MinimumCountIsCommand<string>));
            Add(CommandNames.MinimumLengthIsCommand, typeof(string), Args(5), typeof(MinimumLengthIsCommand));
        }
    }

    [Theory]
    [ClassData(typeof(TestDataForCommands))]
    public void Create_ReturnsCommand(string commandName, Type subjectType, object?[] args, Type coommandType) {
        var validator = ValidationCommandFactory.For(subjectType, "Attribute").Create(commandName, args);

        validator.Should().BeOfType(coommandType);
    }

    [Theory]
    [InlineData(typeof(decimal))]
    [InlineData(typeof(int))]
    [InlineData(typeof(string))]
    [InlineData(typeof(List<int>))]
    [InlineData(typeof(List<string>))]
    [InlineData(typeof(Dictionary<string, int>))]
    [InlineData(typeof(Dictionary<string, decimal>))]
    [InlineData(typeof(Dictionary<string, string>))]
    [InlineData(typeof(Dictionary<double, double>))]
    public void Create_ForUnsupportedValidator_Throws(Type subjectType) {
        //Act
        var action = () => ValidationCommandFactory.For(subjectType, "Attribute").Create("Anything", Array.Empty<object>());

        //Assert
        action.Should().Throw<InvalidOperationException>();
    }

    private class TestDataForValidateSuccess : TheoryData<string, object?[], object> {
        public TestDataForValidateSuccess() {
            Add(CommandNames.ContainsCommand, Args("cDe"), "AbcDef");
            Add(CommandNames.ContainsCommand, Args(20), new List<int> { 20 });
            Add(CommandNames.CountIsCommand, Args(3), new List<int> { 1, 2, 3 });
            Add(CommandNames.CountIsCommand, Args(2), new Dictionary<string, decimal> { ["A"] = 1m, ["B"] = 2m });
            Add(CommandNames.CountIsCommand, Args(2), new Dictionary<string, int> { ["A"] = 1, ["B"] = 2 });
            Add(CommandNames.CountIsCommand, Args(2), new Dictionary<string, string> { ["A"] = "1", ["B"] = "2" });
            Add(CommandNames.CountIsCommand, Args(2), new List<string> { "A", "B" });
            Add(CommandNames.IsEqualToCommand, Args(20), 20);
            Add(CommandNames.IsGreaterThanCommand, Args(20), 30);
            Add(CommandNames.IsLessThanCommand, Args(20.0m), 10m);
            Add(CommandNames.IsLessThanCommand, Args(20), 10);
            Add(CommandNames.IsOneOfCommand, Args("Abc", "Def"), "Abc");
            Add(CommandNames.LengthIsCommand, Args(6), "AbcDef");
            Add(CommandNames.MaximumCountIsCommand, Args(2), new Dictionary<string, string> { ["A"] = "1", ["B"] = "2" });
            Add(CommandNames.MaximumCountIsCommand, Args(2), new List<string> { "A", "B" });
            Add(CommandNames.MaximumLengthIsCommand, Args(5), "Abc");
            Add(CommandNames.MinimumCountIsCommand, Args(2), new Dictionary<string, string> { ["A"] = "1", ["B"] = "2" });
            Add(CommandNames.MinimumCountIsCommand, Args(2), new List<string> { "A", "B" });
            Add(CommandNames.MinimumLengthIsCommand, Args(5), "AbcDef");
        }
    }

    [Theory]
    [ClassData(typeof(TestDataForValidateSuccess))]
    public void Validate_WithValidSubject_ReturnsSuccess(string validatorName, object?[] args, object validValue) {
        var validator = ValidationCommandFactory.For(validValue.GetType(), "Attribute").Create(validatorName, args);
        var validResult = validator.Validate(validValue);

        validResult.IsSuccess.Should().BeTrue();
    }

    private class TestDataForValidateFailure : TheoryData<string, object?[], object> {
        public TestDataForValidateFailure() {
            Add(CommandNames.ContainsCommand, Args("cDe"), "Abc");
            Add(CommandNames.ContainsCommand, Args(20), new List<int> { 10 });
            Add(CommandNames.CountIsCommand, Args(3), new List<int> { 1 });
            Add(CommandNames.CountIsCommand, Args(2), new Dictionary<string, decimal> { ["A"] = 1m });
            Add(CommandNames.CountIsCommand, Args(2), new Dictionary<string, int> { ["A"] = 1 });
            Add(CommandNames.CountIsCommand, Args(2), new Dictionary<string, string> { ["A"] = "1" });
            Add(CommandNames.CountIsCommand, Args(2), new List<string> { "A" });
            Add(CommandNames.IsEqualToCommand, Args(20), 10);
            Add(CommandNames.IsGreaterThanCommand, Args(20), 10);
            Add(CommandNames.IsLessThanCommand, Args(20.0m), 30m);
            Add(CommandNames.IsLessThanCommand, Args(20), 30);
            Add(CommandNames.IsOneOfCommand, Args("Abc", "Def"), "Xyz");
            Add(CommandNames.LengthIsCommand, Args(5), "Abc");
            Add(CommandNames.MaximumCountIsCommand, Args(2), new Dictionary<string, string> { ["A"] = "1", ["B"] = "2", ["C"] = "3" });
            Add(CommandNames.MaximumCountIsCommand, Args(2), new List<string> { "A", "B", "C" });
            Add(CommandNames.MaximumLengthIsCommand, Args(5), "AbcDef");
            Add(CommandNames.MinimumCountIsCommand, Args(2), new Dictionary<string, string> { ["A"] = "1" });
            Add(CommandNames.MinimumCountIsCommand, Args(2), new List<string> { "A" });
            Add(CommandNames.MinimumLengthIsCommand, Args(5), "Abc");
        }
    }

    [Theory]
    [ClassData(typeof(TestDataForValidateFailure))]
    public void Validate_WithInvalidSubject_ReturnsFailure(
        string validatorName,
        object?[] args,
        object invalidValue) {
        var validator = ValidationCommandFactory.For(invalidValue.GetType(), "Attribute").Create(validatorName, args);
        var invalidResult = validator.Validate(invalidValue);

        invalidResult.IsInvalid.Should().BeTrue();
    }
}