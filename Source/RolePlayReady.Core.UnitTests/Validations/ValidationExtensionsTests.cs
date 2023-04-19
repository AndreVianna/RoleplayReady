namespace System.Validations;

public class ValidationExtensionsTests {
    private class TestObject : IValidatable {
        public string? Text { get; init; }
        public int Integer { get; init; }
        public int? Integer1 { get; init; }
        public int? Integer2 { get; init; }
        public decimal Decimal { get; init; }
        public decimal? Decimal1 { get; init; }
        public decimal? Decimal2 { get; init; }
        public DateTime DateTime { get; init; }
        public DateTime? DateTime1 { get; init; }
        public DateTime? DateTime2 { get; init; }
        public Type? Type { get; init; }
        public ICollection<TestObject> TestObjects { get; init; } = new List<TestObject>();
        public ICollection<int> Integers { get; init; } = new List<int>();
        public IDictionary<string, TestObject> Map { get; init; } = new Dictionary<string, TestObject>();
        public IDictionary<int, string> Links { get; init; } = new Dictionary<int, string>();
        public ValidationResult Validate() {
            var baseDate = DateTime.Parse("2023-04-01 12:34:56.789");
            var result = new ValidationResult();
            result += Text.IsNotNull()
                          .And.NotEmptyOrWhiteSpace()
                          .And.NoShorterThan(3)
                          .And.NoLongerThan(10).Result;
            result += Integer.Is().GreaterThan(10).And.LessThan(20).Result;
            result += Integer1.IsNullOr().GreaterThan(10).And.LessThan(20).Result;
            result += Integer2.IsNotNull().And.GreaterOrEqualTo(10).And.LessOrEqualTo(20).Result;
            result += Decimal.Is().GreaterThan(10).And.LessThan(20).Result;
            result += Decimal1.IsNullOr().GreaterThan(10).And.LessThan(20).Result;
            result += Decimal2.IsNotNull().And.GreaterOrEqualTo(10).And.LessOrEqualTo(20).Result;
            result += DateTime.Is().After(baseDate).And.Before(baseDate.AddDays(1)).Result;
            result += DateTime1.IsNullOr().After(baseDate).And.Before(baseDate.AddDays(1)).Result;
            result += DateTime2.IsNotNull().And.StartsOn(baseDate).And.EndsOn(baseDate.AddDays(1)).Result;
            result += Type.IsNotNull().Result;
            result += TestObjects.ForEach(item => item.IsNotNull().And.Valid()).Result;
            result += Integers.IsNotNull().And.NotEmpty().And.ForEach(entry => entry.Is().GreaterOrEqualTo(0)).Result;
            result += Map.IsNotNull().And.ForEach(entry => entry.Value.IsNotNull().And.Valid()).Result;
            result += Links.IsNotNull().And.NotSmallerThan(3).And.NotBiggerThan(5).Result;
            return result;
        }
    }

    private static TestObject GenerateGoodData() {
        var baseDate = DateTime.Parse("2023-04-01 12:34:56.789");
        var testObject1 = new TestObject() {
            Text = "TestData2",
            Integer = 11,
            Integer2 = 11,
            Decimal = 10.1m,
            Decimal2 = 10.1m,
            DateTime = baseDate.AddSeconds(1),
            DateTime2 = baseDate.AddSeconds(1),
            Type = typeof(int),
            Integers = new[] { 1, 2, 3 },
            Links = new Dictionary<int, string> { [1] = "A", [2] = "B", [3] = "C" }
        };
        var testObject2 = new TestObject() {
            Text = "TestData3",
            Integer = 19,
            Integer2 = 19,
            Decimal = 19.9m,
            Decimal2 = 19.9m,
            DateTime = baseDate.AddDays(1).AddSeconds(-1),
            DateTime2 = baseDate.AddDays(1).AddSeconds(-1),
            Type = typeof(DateTime),
            Integers = new[] { 1, 2, 3 },
            Links = new Dictionary<int, string> { [1] = "A", [2] = "B", [3] = "C" }
        };
        var children = new List<TestObject>() {
            testObject1,
            testObject2,
        };
        var map = new Dictionary<string, TestObject>() {
            { "Test1", testObject1 },
            { "Test2", testObject2 },
        };
        return new TestObject {
            Text = "TestData",
            Integer = 15,
            Integer2 = 15,
            Decimal = 15.0m,
            Decimal2 = 15.0m,
            DateTime = baseDate.AddDays(0.5),
            DateTime2 = baseDate.AddDays(0.5),
            Type = typeof(string),
            Integers = new[] { 1, 2, 3 },
            Links = new Dictionary<int, string> { [1] = "A", [2] = "B", [3] = "C" },
            TestObjects = children,
            Map = map,
        };
    }

    private static TestObject GenerateBadData() {
        var baseDate = DateTime.Parse("2023-04-01 12:34:56.789");
        var testObject0 = new TestObject();
        var testObject1 = new TestObject {
            Text = "12",
            Integer = 5,
            Integer2 = 1,
            Decimal = 5,
            Decimal2 = 1.0m,
            DateTime = baseDate.AddDays(-1),
            DateTime2 = baseDate.AddDays(-1),
            Type = typeof(string),
            TestObjects = new[] { testObject0 },
            Integers = Array.Empty<int>(),
            Map = new Dictionary<string, TestObject> { ["Test0"] = testObject0 },
            Links = new Dictionary<int, string> { [1] = "A", [2] = "B" }
        };
        var testObject2 = new TestObject() {
            Text = "12345678901",
            Integer = 30,
            Integer2 = 30,
            Decimal = 30,
            Decimal2 = 30.0m,
            DateTime = baseDate.AddDays(3),
            DateTime2 = baseDate.AddDays(3),
            Type = typeof(int),
            TestObjects = new [] { testObject0 },
            Integers = new[] { 1, 2, 3 },
            Map = new Dictionary<string, TestObject> { ["Test0"] = testObject0 },
            Links = new Dictionary<int, string> { [1] = "A", [2] = "B", [3] = "C", [4] = "D", [5] = "E", [6] = "F" }
        };
        var testObject3 = new TestObject() {
            Text = null,
            Integer2 = null,
            Decimal2 = null,
            DateTime2 = null,
            Type = null,
            TestObjects = null!,
            Integers = null!,
            Map = null!,
            Links = null!,
        };
        var testObject4 = new TestObject() {
            Text = "   ",
            Integer2 = 4,
            Decimal2 = 1000.0m,
            DateTime2 = baseDate,
            Type = typeof(decimal),
            Integers = Array.Empty<int>(),
            Links = new Dictionary<int, string>(),
        };
        var children = new List<TestObject>() {
            testObject1,
            null!,
            testObject2,
        };
        var map = new Dictionary<string, TestObject>() {
            ["Test1"] = testObject3,
            ["Test2"] = null!,
            ["Test3"] = testObject4,
        };
        return new TestObject() {
            Text = "",
            Integer2 = 1,
            Decimal2 = null,
            DateTime2 = baseDate,
            Type = typeof(string),
            TestObjects = children,
            Map = map,
        };
    }

    [Fact]
    public void Validators_WithGoodData_ReturnsValid() {
        // Arrange
        var subject = GenerateGoodData();

        // Act
        var result = subject.Validate();

        // Assert
        //result.IsSuccessful.Should().BeTrue();
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
        result.Errors.Should().HaveCount(88);
    }
}
