namespace RolePlayReady.Models;

public abstract record Base<TKey> : Persistent<TKey>, IBase<TKey> {

    protected Base(IDateTime? dateTime = null)
        : base(dateTime) {
    }

    private const int _maxNameSize = 100;
    public required string Name { get; init; }
    private const int _maxDescriptionSize = 1000;
    public required string Description { get; init; }

    private const int _maxShortNameSize = 10;
    public string? ShortName { get; init; }

    private const int _maxTagSize = 20;
    public IList<string> Tags { get; init; } = new List<string>();

    public virtual ValidationResult Validate() => Validate<object>();

    public virtual ValidationResult Validate<TContext>(TContext? context = null)
        where TContext : class {
        var result = ValidationResult.Valid;
        result += Name.Is().Required.And.NoLongerThan(_maxNameSize).Errors;
        result += Description.Is().Required.And.NoLongerThan(_maxDescriptionSize).Errors;
        result += ShortName.Is().NoLongerThan(_maxShortNameSize).Errors;
        result += Tags.AreAll(t => t.Required.And.NoLongerThan(_maxTagSize)).Errors;
        return result;
    }

    public sealed override string ToString() => $"[{GetType().Name}] {Name}{(ShortName is not null ? $" ({ShortName})" : string.Empty)}";
}
