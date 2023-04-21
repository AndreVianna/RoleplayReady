namespace System;

public class MakeTests {
    [Theory]
    [InlineData("String", typeof(string))]
    [InlineData("Integer", typeof(int))]
    [InlineData("Decimal", typeof(decimal))]
    [InlineData("List<String>", typeof(List<string>))]
    [InlineData("List<Integer>", typeof(List<int>))]
    [InlineData("Dictionary<String,Integer>", typeof(Dictionary<string, int>))]
    [InlineData("Dictionary<Integer,String>", typeof(Dictionary<int, string>))]
    [InlineData("Dictionary<String,String>", typeof(Dictionary<string, string>))]
    public void TypeFrom_ReturnsType(string name, Type expectedType) {
        var result = Make.TypeFrom(name);

        result.Should().Be(expectedType);
    }

    [Fact]
    public void TypeFrom_ForInvalidType_Throws() {
        var action = () => Make.TypeFrom("Long");

        action.Should().Throw<InvalidOperationException>().WithMessage("Unsupported type 'Long'.");
    }
}