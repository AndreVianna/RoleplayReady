namespace RolePlayReady.Models.Contracts;

public interface IEntity : IIdentification, IMayHaveASource, IVersion {

    Usage Usage { get; init; }

    IList<string> Tags { get; init; }
    IList<IEntityAttribute> Attributes { get; init; }

    //IList<Func<IEntity, bool>> Requirements { get; init; }
    //bool QualifiesFor(IEntity entity);

    //IList<Func<IEntity, IEntity>> Changes { get; init; }
    //IEntity ApplyTo(IEntity entity);

    //IList<Func<IEntity, ValidationResult>> Validations { get; init; }
    //bool IsValid { get; }
    //ValidationResult Validate();
}