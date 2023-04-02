namespace RoleplayReady.Domain.Models.Contracts;

public interface IEntity
    : IAmTrackable {

    string Abbreviation { get; init; }
    string Name { get; init; }
    string Description { get; init; }
    Usage Usage { get; init; }
    ISource? Source { get; init; }

    IList<string> Tags { get; init; }

    IList<IEntityAttribute> Attributes { get; init; }

    IList<Func<IEntity, bool>> Requirements { get; init; }
    bool QualifiesFor(IEntity entity);

    IList<Func<IEntity, IEntity>> Changes { get; init; }
    IEntity ApplyTo(IEntity entity);

    IList<Func<IEntity, ValidationResult>> Validations { get; init; }
    bool IsValid { get; }
    ValidationResult Validate();
}