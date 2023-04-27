namespace RolePlayReady.Models;

public record Base : IBase, IValidatable {
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string? ShortName { get; init; }

    public ICollection<string> Tags { get; init; } = new List<string>();

    public virtual Result Validate() {
        var result = new Result();
        result += Name.IsNotNull()
                      .And.IsNotEmptyOrWhiteSpace()
                      .And.MaximumLengthIs(Validation.Name.MaximumLength).Result;
        result += Description.IsNotNull()
                             .And.IsNotEmptyOrWhiteSpace()
                             .And.MaximumLengthIs(Validation.Description.MaximumLength).Result;
        result += ShortName.IsNullOr()
                           .IsNotEmptyOrWhiteSpace()
                           .And.MaximumLengthIs(Validation.ShortName.MaximumLength).Result;
        result += Tags.ForEach(item => item.IsNotNull()
                                           .And.IsNotEmptyOrWhiteSpace()
                                           .And.MaximumLengthIs(Validation.Tag.MaximumLength)).Result;
        return result;
    }
}