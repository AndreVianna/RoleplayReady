namespace RolePlayReady.Models;

public abstract record Entity : IEntity {
    protected Entity() {
        OwnerId = "System";
    }

    [SetsRequiredMembers]
    protected Entity(INode parent, string abbreviation, string name, string description, IDateTimeProvider? dateTime = null)
        : this() {
        Parent = parent;

        Abbreviation = Throw.IfNullOrWhiteSpaces(abbreviation);
        Name = Throw.IfNullOrWhiteSpaces(name);
        Description = Throw.IfNull(description);

        dateTime ??= new SystemDateTimeProvider();
        Timestamp = dateTime.Now;
        State = State.NotReady;
    }

    public string OwnerId { get; init; }

    public INode Root => Parent.Root;
    public required INode Parent { get; init; }
    public IList<INode> Children { get; init; } = new List<INode>();

    public string EntityType => GetType().Name;
    // RuleSet, OwnerId, and Abbreviation must be unique.
    public required string Abbreviation { get; init; }
    // RuleSet, OwnerId, and Name must be unique.
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string FullName => $"<{EntityType}> {Name} ({Abbreviation})";


    //public Usage Usage { get; init; }

    public IList<string> Tags { get; init; } = new List<string>();
    public IList<IEntityAttribute> Attributes { get; init; } = new List<IEntityAttribute>();

    public DateTime Timestamp { get; init; }
    public State State { get; init; }

    //public IList<Func<IEntity, bool>> Requirements { get; init; } = new List<Func<IEntity, bool>>();

    //public bool QualifiesFor(IEntity entity)
    //    => Requirements.All(checkFor => checkFor(entity));

    //public IList<Func<IEntity, IEntity>> Changes { get; init; } = new List<Func<IEntity, IEntity>>();

    //public IEntity ApplyTo(IEntity entity)
    //    => Changes.Aggregate(entity, (current, change) => change(current));

    //public IList<Func<IEntity, ValidationResult>> Validations { get; init; } = new List<Func<IEntity, ValidationResult>>();
    //public bool IsValid => Validations.All(validate => validate(this).IsValid);

    //public ValidationResult Validate() => Validations
    //    .Aggregate(ValidationResult.Valid,
    //        (current, validate) =>
    //            current + validate(this));

    //public virtual TSelf CloneUnder<TSelf>(IEntity? _)
    //    where TSelf : class, IEntity {
    //    var result = this with {
    //        State = State.NotReady,
    //        Timestamp = DateTime.Now,
    //        Tags = Tags.ToList(),
    //        Attributes = new List<IEntityAttribute>(),
    //        Requirements = new List<Func<IEntity, bool>>(Requirements),
    //        Changes = new List<Func<IEntity, IEntity>>(Changes),
    //        Validations = new List<Func<IEntity, ValidationResult>>(Validations),
    //    };
    //    foreach (var attribute in Attributes)
    //        result.Attributes.Add(attribute.CloneUnder(result));
    //    return (result as TSelf)!;
    //}
}
