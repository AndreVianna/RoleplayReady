using System.Results;

namespace System.Validators;

public class ValidatorExtensionsTests {
    private class TestObject : IValidatable {
        public string Text { get; init; } = string.Empty;
        public int Integer1 { get; init; }
        public int? Integer2 { get; init; }
        public decimal Decimal1 { get; init; }
        public decimal? Decimal2 { get; init; }
        public DateTime DateTime1 { get; init; }
        public DateTime? DateTime2 { get; init; }
        public Type Type1 { get; init; } = default!;
        public ICollection<TestObject> Children { get; init; } = new List<TestObject>();
        public IDictionary<string, TestObject> Maps { get; init; } = new Dictionary<string, TestObject>();
        public Validation Validate() {
            var result = new Validation();
            result += Text.ValueIs().NotNull().And.NotEmptyOrWhiteSpace().Result;
            result += Integer1.ValueIs().NotNull().Result;
            result += Integer2.ValueIs().NotNull().Result;
            result += Decimal1.ValueIs().NotNull().Result;
            result += Decimal2.ValueIs().NotNull().Result;
            result += DateTime1.ValueIs().NotNull().Result;
            result += DateTime2.ValueIs().NotNull().Result;
            result += Type1.ValueIs().NotNull().Result;
            result += Children.CollectionIs().NotNull().And.EachItem(i => i.ValueIs().NotNull().And.Valid()).Result;
            result += Maps.DictionaryIs().NotNull().And.EachItem(i => i.ValueIs().NotNull().And.Valid()).Result;
            return result;
        }
    }

    private static TestObject GenerateGoodData() {
        var testObject1 = new TestObject() {
            Text = "TestData1",
            Integer1 = 1,
            Integer2 = 1,
            Decimal1 = 1.0m,
            Decimal2 = 1.0m,
            DateTime1 = DateTime.Now,
            DateTime2 = DateTime.Now,
            Type1 = typeof(string),
        };
        var testObject2 = new TestObject() {
            Text = "TestData2",
            Integer2 = 2,
            Decimal2 = 10.0m,
            DateTime2 = DateTime.Now,
            Type1 = typeof(int),
        };
        var testObject3 = new TestObject() {
            Text = "TestData3",
            Integer2 = 3,
            Decimal2 = 100.0m,
            DateTime2 = DateTime.Now,
            Type1 = typeof(DateTime),
        };
        var testObject4 = new TestObject() {
            Text = "TestData4",
            Integer2 = 4,
            Decimal2 = 1000.0m,
            DateTime2 = DateTime.Now,
            Type1 = typeof(decimal),
        };
        var children = new List<TestObject>() {
            testObject1,
            testObject2,
        };
        var map = new Dictionary<string, TestObject>() {
            { "Test1", testObject3 },
            { "Test2", testObject4 },
        };
        return new TestObject() {
            Text = "TestData",
            Integer2 = 1,
            Decimal2 = 1.0m,
            DateTime2 = DateTime.Now,
            Type1 = typeof(string),
            Children = children,
            Maps = map,
        };
    }

    private static TestObject GenerateBadData() {
        var testObject0 = new TestObject();
        var testObject1 = new TestObject() {
            Text = null!,
            Integer2 = 1,
            Decimal2 = 1.0m,
            DateTime2 = DateTime.Now,
            Type1 = typeof(string),
            Children = null!,
            Maps = null!,
        };
        var testObject2 = new TestObject() {
            Text = "TestData2",
            Integer2 = null,
            Decimal2 = 10.0m,
            DateTime2 = DateTime.Now,
            Type1 = typeof(int),
            Children = new[] { testObject0 },
            Maps = new Dictionary<string, TestObject> {
                ["Test0"] = testObject0,
            },
        };
        var testObject3 = new TestObject() {
            Text = "TestData3",
            Integer2 = 3,
            Decimal2 = 100.0m,
            DateTime2 = null,
            Type1 = typeof(DateTime),
            Children = new HashSet<TestObject>() { testObject1 },
        };
        var testObject4 = new TestObject() {
            Text = "   ",
            Integer2 = 4,
            Decimal2 = 1000.0m,
            DateTime2 = DateTime.Now,
            Type1 = typeof(decimal),
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
            DateTime2 = DateTime.Now,
            Type1 = typeof(string),
            Children = children,
            Maps = map,
        };
    }

    [Fact]
    public void Validators_WithGoodData_ReturnsValid() {
        // Arrange
        var subject = GenerateGoodData();

        // Act
        var result = subject.Validate();

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact]
    public void Validators_WithBadData_ReturnsFailure() {
        // Arrange
        var subject = GenerateBadData();

        // Act
        var result = subject.Validate();

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Select(i => i.Message).Should().BeEquivalentTo(
            "'Text' cannot be empty or whitespace.",
            "'Decimal2' is required.",
            "'Children[0].Text' is required.",
            "'Children[0].Children' is required.",
            "'Children[0].Maps' is required.",
            "'Children[1]' is required.",
            "'Children[2].Integer2' is required.",
            "'Children[2].Children[0].Text' cannot be empty or whitespace.",
            "'Children[2].Children[0].Integer2' is required.",
            "'Children[2].Children[0].Decimal2' is required.",
            "'Children[2].Children[0].DateTime2' is required.",
            "'Children[2].Children[0].Type1' is required.",
            "'Children[2].Maps[Test0].Text' cannot be empty or whitespace.",
            "'Children[2].Maps[Test0].Integer2' is required.",
            "'Children[2].Maps[Test0].Decimal2' is required.",
            "'Children[2].Maps[Test0].DateTime2' is required.",
            "'Children[2].Maps[Test0].Type1' is required.",
            "'Maps[Test1].DateTime2' is required.",
            "'Maps[Test1].Children[0].Text' is required.",
            "'Maps[Test1].Children[0].Children' is required.",
            "'Maps[Test1].Children[0].Maps' is required.",
            "'Maps[Test2]' is required.",
            "'Maps[Test3].Text' cannot be empty or whitespace."
            );
    }
}
