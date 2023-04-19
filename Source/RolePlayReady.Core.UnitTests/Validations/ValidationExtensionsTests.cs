using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace System.Validations;

public class ValidationExtensionsTests {
    private record TestObject : IValidatable {
        public string? Text1 { get; init; }
        public string? Text2 { get; init; }
        public int Integer { get; init; }
        private static int? Integer1 => null;
        public int? Integer2 { get; init; }
        public decimal Decimal { get; init; }
        private static decimal? Decimal1 => null;
        public decimal? Decimal2 { get; init; }
        public DateTime DateTime { get; init; }
        private static DateTime? DateTime1 => null;
        public DateTime? DateTime2 { get; init; }
        public Type? Type { get; init; }
        public ICollection<int> Integers { get; init; } = new List<int>();
        public ICollection<TestObject> TestObjects { get; init; } = new List<TestObject>();
        public IDictionary<int, string> Pairs { get; init; } = new Dictionary<int, string>();
        public IDictionary<string, TestObject> Items { get; init; } = new Dictionary<string, TestObject>();
        public ValidationResult Validate() {
            var baseDate = DateTime.Parse("2023-04-01 12:34:56.789");
            var result = new ValidationResult();
            result += Text1.IsNotNull()
                .And.IsNotEmptyOrWhiteSpace()
                .And.MinimumLengthIs(3)
                .And.MaximumLengthIs(10)
                .And.LengthIs(9).Result;
            result += Text2.IsNullOr()
                .IsNotEmptyOrWhiteSpace()
                .And.MinimumLengthIs(3)
                .And.MaximumLengthIs(10)
                .And.LengthIs(9).Result;
            result += Integer.Value().IsGreaterThan(10).And.IsLessThan(20).And.IsEqualTo(15).Result;
            result += Integer1.ValueIsNullOr().IsGreaterThan(10).And.IsLessThan(20).And.IsEqualTo(15).Result;
            result += Integer2.IsNotNull().And.MinimumIs(10).And.MaximumIs(20).Result;
            result += Decimal.Value().IsGreaterThan(10).And.IsLessThan(20).Result;
            result += Decimal1.ValueIsNullOr().IsGreaterThan(10).And.IsLessThan(20).Result;
            result += Decimal2.IsNotNull().And.MinimumIs(10).And.MaximumIs(20).Result;
            result += DateTime.Value().IsAfter(baseDate).And.IsBefore(baseDate.AddDays(1)).Result;
            result += DateTime1.ValueIsNullOr().IsAfter(baseDate).And.IsBefore(baseDate.AddDays(1)).Result;
            result += DateTime2.IsNotNull().And.StartsOn(baseDate).And.EndsOn(baseDate.AddDays(1)).Result;
            result += Type.IsNotNull().And.IsEqualTo(typeof(int)).Result;
            result += TestObjects.ForEach(item => item.IsNotNull().And.IsValid()).Result;
            result += Integers.List().IsNotEmpty().And.ForEach(entry => entry.Value().MinimumIs(0)).Result;
            result += Items.List().ForEach(entry => entry.Value.IsNotNull().And.IsValid()).Result;
            result += Pairs.List().MinimumCountIs(3).And.MaximumCountIs(5).And.CountIs(4).Result;
            return result;
        }
    }

    private static readonly DateTime _baseDate = DateTime.Parse("2023-04-01 12:34:56.789");

    private static readonly TestObject _testObject = new() {
        Text1 = "SomeText1",
        Text2 = "SomeText2",
        Integer = 15,
        Integer2 = 15,
        Decimal = 15m,
        Decimal2 = 15m,
        DateTime = _baseDate.AddSeconds(1),
        DateTime2 = _baseDate.AddSeconds(1),
        Type = typeof(int),
        Integers = new[] { 1, 2, 3 },
        Pairs = new Dictionary<int, string> { [1] = "A", [2] = "B", [3] = "C", [4] = "D" }
    };

    private static TestObject GenerateGoodData() {
        var children = new List<TestObject> {
            _testObject with { Text1 = "TestText1" },
            _testObject with { Text2 = "TestText2" },
        };
        var map = new Dictionary<string, TestObject> {
            { "Map1", _testObject with { Text1 = "TestItem1" } },
            { "Map2", _testObject with { Text2 = "TestItem2" } },
        };
        return _testObject with {
            TestObjects = children,
            Items = map,
        };
    }

    private static TestObject GenerateBadData() {
        var allNulls = new TestObject() {
            Text1 = null,
            Text2 = null,
            Integer2 = null,
            Decimal2 = null,
            DateTime2 = null,
            Type = null,
            TestObjects = null!,
            Integers = null!,
            Items = null!,
            Pairs = null!,
        };
        var emptyLists = _testObject with {
            Integers = Array.Empty<int>(),
            Pairs = new Dictionary<int, string>(),
        };
        var invalidLowerBoundaries = new TestObject {
            Text1 = "12",
            Text2 = "12",
            Integer = 5,
            Integer2 = 1,
            Decimal = 5,
            Decimal2 = 1.0m,
            DateTime = _baseDate.AddDays(-1),
            DateTime2 = _baseDate.AddDays(-1),
            Type = typeof(string),
            TestObjects = new[] { allNulls },
            Integers = Array.Empty<int>(),
            Items = new Dictionary<string, TestObject> { ["Test0"] = allNulls },
            Pairs = new Dictionary<int, string> { [1] = "A", [2] = "B" }
        };
        var invalidUpperBoundaries = new TestObject {
            Text1 = "12345678901",
            Text2 = "12345678901",
            Integer = 30,
            Integer2 = 30,
            Decimal = 30,
            Decimal2 = 30.0m,
            DateTime = _baseDate.AddDays(3),
            DateTime2 = _baseDate.AddDays(3),
            Type = typeof(int),
            TestObjects = new [] { allNulls },
            Integers = new[] { 1, 2, 3 },
            Items = new Dictionary<string, TestObject> { ["Test0"] = null!, ["Test1"] = allNulls, ["Test2"] = emptyLists },
            Pairs = new Dictionary<int, string> { [0] = null!, [1] = "A", [2] = "B", [3] = "C", [4] = "D", [5] = "E", [6] = "F" }
        };
        var invalidChildren = new List<TestObject>() {
            null!,
            invalidLowerBoundaries,
            invalidUpperBoundaries,
        };
        return _testObject with {
            Text1 = "",
            Text2 = "   ",
            TestObjects = invalidChildren,
        };
    }

    [Fact]
    public void Validators_WithGoodData_ReturnsValid() {
        // Arrange
        var subject = GenerateGoodData();

        // Act
        var result = subject.Validate();

        // Assert
        result.IsSuccessful.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact]
    public void Validators_WithBadData_ReturnsFailure() {
        // Arrange
        var subject = GenerateBadData();

        // Act
        var result = subject.Validate();

        // Assert
        result.IsSuccessful.Should().BeFalse();
        result.Errors.Should().HaveCount(90);
    }
}
