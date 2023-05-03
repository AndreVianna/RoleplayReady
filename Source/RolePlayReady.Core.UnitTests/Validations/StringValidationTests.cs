namespace System.Validations;

public class StringValidationTests {
    public record TestObject : IValidatable {
        public string? RequiredText { get; init; }
        public string? OptionalText { get; init; }
        public ValidationResult Validate() {
            var result = ValidationResult.Success();
            result += RequiredText.IsNotNull()
                          .And.IsNotEmptyOrWhiteSpace()
                          .And.MinimumLengthIs(3)
                          .And.MaximumLengthIs(10)
                          .And.LengthIs(5)
                          .And.IsIn("Text1", "Text2", "Text3").Result;
            result += OptionalText.IsNullOr()
                                  .IsNotEmptyOrWhiteSpace()
                                  .And.IsEmail().Result;
            return result;
        }
    }

    private class TestData : TheoryData<TestObject, bool, int> {
        public TestData() {
            Add(new() { RequiredText = "Text1", }, true, 0);
            Add(new() { RequiredText = "Text1", OptionalText = "some@email.com" }, true, 0);
            Add(new() { RequiredText = "Text1", OptionalText = "" }, false, 1);
            Add(new() { RequiredText = "Text1", OptionalText = "  " }, false, 2);
            Add(new() { RequiredText = "Text1", OptionalText = "NotEmail" }, false, 1);
            Add(new() { RequiredText = null }, false, 1);
            Add(new() { RequiredText = "" }, false, 4);
            Add(new() { RequiredText = "  " }, false, 4);
            Add(new() { RequiredText = "12" }, false, 3);
            Add(new() { RequiredText = "12345678901" }, false, 3);
            Add(new() { RequiredText = "Other" }, false, 1);
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