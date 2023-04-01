namespace RoleplayReady.Domain.Models.Contracts;

public interface IElementValidator {
    Func<IElement, ICollection<ValidationError>, IElement> Validate { get; init; }

    IElementValidator? Next { get; init; }

    IElement Execute(IElement original, ICollection<ValidationError> errors);
}