namespace System.Validations.Abstractions;

public interface IStringCollectionValidators {
    IStringValidationConnector<IStringCollectionValidators> Required { get; }

    IStringValidationConnector<IStringCollectionValidators> AllAre(Func<IStringValidators, IStringValidationConnector<IStringValidators>> validate);
}