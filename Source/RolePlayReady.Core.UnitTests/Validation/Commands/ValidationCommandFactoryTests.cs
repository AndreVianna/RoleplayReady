using CommandNames = System.Constants.Constants.Commands;

namespace System.Validation.Commands;

public class ValidationCommandFactoryTests {
    private static object?[] Args(params object?[] args) => args;

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

    private const string _string = "AbcDef";
    private const int _integer = 42;
    private const decimal _decimal = 42.0m;
    private static readonly DateTime _dateTime = DateTime.Parse("2020-01-01 10:10:10.12345");
    private static readonly Type _type = typeof(string);
    private static readonly List<int> _integers = new() { 1, 2, 3 };
    private static readonly List<string> _strings = new() { "A", "B", "C" };
    private static readonly Dictionary<string, int> _strings2Integers = new() { ["A"] = 1, ["B"] = 2, ["C"] = 3 };
    private static readonly Dictionary<string, decimal> _strings2Decimals = new() { ["A"] = 1m, ["B"] = 2m, ["C"] = 3m };
    private static readonly Dictionary<string, string> _strings2Strings = new() { ["A"] = "1", ["B"] = "2", ["C"] = "3" };

    private class TestDataForValidateSuccess : TheoryData<string, object?[], object> {
        public TestDataForValidateSuccess() {
            Add(CommandNames.HasCommand, Args(_string[1..^1]), _string);
            Add(CommandNames.HasCommand, Args(_integers[1]), _integers);
            Add(CommandNames.ContainsKeyCommand, Args(_strings2Strings.Keys.First()), _strings2Strings);
            Add(CommandNames.ContainsValueCommand, Args(_strings2Integers.Values.Last()), _strings2Integers);
            Add(CommandNames.CountIsCommand, Args(_integers.Count), _integers);
            Add(CommandNames.CountIsCommand, Args(_strings2Decimals.Count), _strings2Decimals);
            Add(CommandNames.CountIsCommand, Args(_strings2Integers.Count), _strings2Integers);
            Add(CommandNames.CountIsCommand, Args(_strings2Strings.Count), _strings2Strings);
            Add(CommandNames.CountIsCommand, Args(_strings.Count), _strings);
            Add(CommandNames.IsGreaterThanDateCommand, Args(_dateTime), _dateTime.AddSeconds(1));
            Add(CommandNames.IsLessThanDateCommand, Args(_dateTime), _dateTime.AddSeconds(-1));
            Add(CommandNames.EqualsCommand, Args(_string), _string);
            Add(CommandNames.EqualsCommand, Args(_integer), _integer);
            Add(CommandNames.EqualsCommand, Args(_decimal), _decimal);
            Add(CommandNames.EqualsCommand, Args(_dateTime), _dateTime);
            Add(CommandNames.EqualsCommand, Args(_type), _type);
            Add(CommandNames.IsGreaterThanCommand, Args(_integer), _integer + 1);
            Add(CommandNames.IsGreaterThanCommand, Args(_decimal), _decimal + 0.01m);
            Add(CommandNames.IsLessThanCommand, Args(_decimal), _decimal - 0.01m);
            Add(CommandNames.IsLessThanCommand, Args(_integer), _integer - 1);
            Add(CommandNames.IsOneOfCommand, Args(_strings.OfType<object?>().ToArray()), _strings[1]);
            Add(CommandNames.SizeIsCommand, Args(_string.Length), _string);
            Add(CommandNames.MaximumCountIsCommand, Args(_strings2Strings.Count), _strings2Strings);
            Add(CommandNames.MaximumCountIsCommand, Args(_strings.Count), _strings);
            Add(CommandNames.SizeIsAtMostCommand, Args(_string.Length), _string);
            Add(CommandNames.MinimumCountIsCommand, Args(_strings2Strings.Count), _strings2Strings);
            Add(CommandNames.MinimumCountIsCommand, Args(_strings.Count), _strings);
            Add(CommandNames.SizeIsAtLeastCommand, Args(_string.Length), _string);
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
            Add(CommandNames.HasCommand, Args("cDe"), "Abc");
            Add(CommandNames.HasCommand, Args(20), new List<int> { 10 });
            Add(CommandNames.CountIsCommand, Args(3), new List<int> { 1 });
            Add(CommandNames.CountIsCommand, Args(2), new Dictionary<string, decimal> { ["A"] = 1m });
            Add(CommandNames.CountIsCommand, Args(2), new Dictionary<string, int> { ["A"] = 1 });
            Add(CommandNames.CountIsCommand, Args(2), new Dictionary<string, string> { ["A"] = "1" });
            Add(CommandNames.CountIsCommand, Args(2), new List<string> { "A" });
            Add(CommandNames.EqualsCommand, Args(20), 10);
            Add(CommandNames.IsGreaterThanCommand, Args(20), 10);
            Add(CommandNames.IsLessThanCommand, Args(20.0m), 30m);
            Add(CommandNames.IsLessThanCommand, Args(20), 30);
            Add(CommandNames.IsOneOfCommand, Args("Abc", "Def"), "Xyz");
            Add(CommandNames.SizeIsCommand, Args(5), "Abc");
            Add(CommandNames.MaximumCountIsCommand, Args(2), new Dictionary<string, string> { ["A"] = "1", ["B"] = "2", ["C"] = "3" });
            Add(CommandNames.MaximumCountIsCommand, Args(2), new List<string> { "A", "B", "C" });
            Add(CommandNames.SizeIsAtMostCommand, Args(5), "AbcDef");
            Add(CommandNames.MinimumCountIsCommand, Args(2), new Dictionary<string, string> { ["A"] = "1" });
            Add(CommandNames.MinimumCountIsCommand, Args(2), new List<string> { "A" });
            Add(CommandNames.SizeIsAtLeastCommand, Args(5), "Abc");
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