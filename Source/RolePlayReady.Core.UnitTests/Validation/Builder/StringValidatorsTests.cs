namespace System.Validation.Builder;

public class StringValidatorsTests {
    public record TestObject : IValidatable {
        public string? RequiredText { get; init; }
        public string? OptionalText { get; init; }

        public ValidationResult ValidateSelf(bool negate = false) {
            var result = ValidationResult.Success();
            result += RequiredText.IsRequired()
                          .And().IsNotEmptyOrWhiteSpace()
                          .And().MinimumLengthIs(3)
                          .And().MaximumLengthIs(10)
                          .And().LengthIs(5)
                          .And().IsIn("Text1", "Text2", "Text3").Result;
            result += OptionalText.IsOptional()
                                  .And().IsNotEmpty()
                                  .And().IsEmail().Result;
            return result;
        }
    }

    private class TestData : TheoryData<TestObject, int> {
        public TestData() {
            Add(new() { RequiredText = "Text1", }, 0);
            Add(new() { RequiredText = "Text1", OptionalText = "some@email.com" }, 0);
            Add(new() { RequiredText = "Text1", OptionalText = "" }, 2);
            Add(new() { RequiredText = "Text1", OptionalText = "NotEmail" }, 1);
            Add(new() { RequiredText = null }, 1);
            Add(new() { RequiredText = "" }, 4);
            Add(new() { RequiredText = "  " }, 4);
            Add(new() { RequiredText = "12" }, 3);
            Add(new() { RequiredText = "12345678901" }, 3);
            Add(new() { RequiredText = "Other" }, 1);
        }
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Validate_Validates(TestObject subject, int errorCount) {
        // Act
        var result = subject.ValidateSelf();

        // Assert
        result.Errors.Should().HaveCount(errorCount);
    }
}