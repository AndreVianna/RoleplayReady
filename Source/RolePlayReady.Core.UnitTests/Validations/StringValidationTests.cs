namespace System.Validations;

public class StringValidationTests {
    public record TestObject : IValidatable {
        public string? Text { get; init; }
        public Result Validate() {
            var result = Result.AsSuccess();
            result += Text.IsNotNull()
                          .And.IsNotEmptyOrWhiteSpace()
                          .And.MinimumLengthIs(3)
                          .And.MaximumLengthIs(10)
                          .And.LengthIs(5)
                          .And.IsIn("Text1", "Text2", "Text3").Result;
            return result;
        }
    }

    private class TestData : TheoryData<TestObject, bool, int> {
        public TestData() {
            Add(new() { Text = "Text1" }, true, 0);
            Add(new() { Text = null }, false, 1);
            Add(new() { Text = "" }, false, 4);
            Add(new() { Text = "  " }, false, 4);
            Add(new() { Text = "12" }, false, 3);
            Add(new() { Text = "12345678901" }, false, 3);
            Add(new() { Text = "Other" }, false, 1);
        }
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Validate_Validates(TestObject subject, bool isSuccess, int errorCount) {
        // Act
        var result = subject.Validate();

        // Assert
        result.IsSuccess.Should().Be(isSuccess);
        result.Errors.Should().HaveCount(errorCount);
    }
}