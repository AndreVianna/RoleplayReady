namespace System.Validations.Abstractions;

public class Connects<TValidation> : IConnects<TValidation>
    where TValidation : IValidations {
    public Connects(TValidation validation) {
        And = validation;
    }

    public TValidation And { get; }

    public List<ValidationError> Errors => And.Errors;
}

public interface IConnects<out TValidation> {
    TValidation And { get; }
    List<ValidationError> Errors { get; }
}