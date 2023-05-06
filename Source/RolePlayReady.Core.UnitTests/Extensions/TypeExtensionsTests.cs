namespace System.Extensions;

public class TypeExtensionsTests {
    [Theory]
    [InlineData(typeof(int), "Integer")]
    [InlineData(typeof(string), "String")]
    [InlineData(typeof(decimal), "Decimal")]
    [InlineData(typeof(long), "Int64")]
    [InlineData(typeof(List<string>), "List<String>")]
    [InlineData(typeof(List<int>), "List<Integer>")]
    [InlineData(typeof(List<decimal>), "List<Decimal>")]
    [InlineData(typeof(Dictionary<int, int>), "Dictionary<Integer,Integer>")]
    [InlineData(typeof(Dictionary<int, string>), "Dictionary<Integer,String>")]
    [InlineData(typeof(Dictionary<int, decimal>), "Dictionary<Integer,Decimal>")]
    [InlineData(typeof(Dictionary<string, int>), "Dictionary<String,Integer>")]
    [InlineData(typeof(Dictionary<string, string>), "Dictionary<String,String>")]
    [InlineData(typeof(Dictionary<string, decimal>), "Dictionary<String,Decimal>")]
    [InlineData(typeof(Dictionary<decimal, int>), "Dictionary<Decimal,Integer>")]
    [InlineData(typeof(Dictionary<decimal, string>), "Dictionary<Decimal,String>")]
    [InlineData(typeof(Dictionary<decimal, decimal>), "Dictionary<Decimal,Decimal>")]
    [InlineData(typeof(int[]), "Integer[]")]
    [InlineData(typeof(string[]), "String[]")]
    [InlineData(typeof(decimal[]), "Decimal[]")]
    [InlineData(typeof(long[]), "Int64[]")]
    public void ToFriendlyName_CorrectlyConvertsInput(Type input, string expectedResult) {
        // Act
        var name = input.GetName();

        // Assert
        name.Should().Be(expectedResult);
    }
}