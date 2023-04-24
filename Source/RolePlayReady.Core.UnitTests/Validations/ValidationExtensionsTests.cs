namespace System.Validations;

public class ValidationExtensionsTests {
    private record TestObject : IValidatable {
        public Type? Type { get; init; }
        public ICollection<int> Integers { get; init; } = new List<int>();
        public ICollection<TestObject> TestObjects { get; init; } = new List<TestObject>();
        public IDictionary<int, string> Pairs { get; init; } = new Dictionary<int, string>();
        public IDictionary<string, TestObject> Items { get; init; } = new Dictionary<string, TestObject>();
        public ValidationResult Validate() {
            var baseDate = DateTime.Parse("2023-04-01 12:34:56.789");
            var result = new ValidationResult();
            result += Type.IsNotNull().And.IsEqualTo(typeof(int)).Result;
            result += Integers.List().IsNotEmpty().And.ForEach(entry => entry.Value().MinimumIs(0)).Result;
            result += TestObjects.ForEach(item => item.IsNotNull().And.IsValid()).Result;
            result += Items.List().ForEach(entry => entry.Value.IsNotNull().And.IsValid()).Result;
            result += Pairs.List().MinimumCountIs(3).And.MaximumCountIs(5).And.CountIs(4).Result;
            return result;
        }
    }

    private static readonly DateTime _baseDate = DateTime.Parse("2023-04-01 12:34:56.789");

    private static readonly TestObject _testObject = new() {
        Type = typeof(int),
        Integers = new[] { 1, 2, 3 },
        Pairs = new Dictionary<int, string> { [1] = "A", [2] = "B", [3] = "C", [4] = "D" }
    };

    private static TestObject GenerateGoodData() {
        var children = new List<TestObject> {
            _testObject,
            _testObject,
        };
        var map = new Dictionary<string, TestObject> {
            { "Map1", _testObject },
            { "Map2", _testObject },
        };
        return _testObject with {
            TestObjects = children,
            Items = map,
        };
    }

    private static TestObject GenerateBadData() {
        var allNulls = new TestObject() {
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
            Type = typeof(string),
            TestObjects = new[] { allNulls },
            Integers = Array.Empty<int>(),
            Items = new Dictionary<string, TestObject> { ["Test0"] = allNulls },
            Pairs = new Dictionary<int, string> { [1] = "A", [2] = "B" }
        };
        var invalidUpperBoundaries = new TestObject {
            Type = typeof(int),
            TestObjects = new[] { allNulls },
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
        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact]
    public void Validators_WithBadData_ReturnsFailure() {
        // Arrange
        var subject = GenerateBadData();

        // Act
        var result = subject.Validate();

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().HaveCount(31);
    }
}
