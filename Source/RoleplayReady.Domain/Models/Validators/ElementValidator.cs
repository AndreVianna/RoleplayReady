namespace RoleplayReady.Domain.Models.Validators;

public abstract record ElementValidator : IElementValidator {
    protected ElementValidator() { }

    [SetsRequiredMembers]
    protected ElementValidator(Func<IElement, ICollection<ValidationError>, IElement> validate) {
        Validate = validate;
    }

    public required Func<IElement, ICollection<ValidationError>, IElement> Validate { get; init; }

    public IElementValidator? Next { get; init; }

    public IElement Execute(IElement original, ICollection<ValidationError> errors) {
        original = Validate(original, errors);
        return Next?.Execute(original, errors) ?? original;
    }
}