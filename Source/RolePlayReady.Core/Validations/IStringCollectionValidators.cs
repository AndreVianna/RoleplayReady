namespace System.Validations;

public interface IStringCollectionValidators {
    IStringValidationConnector<IStringCollectionValidators> Required { get; }

    IStringValidationConnector<IStringCollectionValidators> AreAll(Func<IStringValidators, IStringValidationConnector<IStringValidators>> validate);
}