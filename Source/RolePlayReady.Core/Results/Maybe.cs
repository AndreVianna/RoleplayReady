using OneOf.Types;

namespace System.Results;

public class Maybe<TObject> : Result<Maybe<TObject>, TObject>
{
    public Maybe() : base(default(TObject)) {
    }

    public Maybe(TObject value) : base(value) {
    }

    public Maybe(ICollection<ValidationError> errors) : base(errors) {
    }

    public Maybe(Exception exception) : base(exception) {
    }

    public override bool IsNull => base.IsSuccess && base.ObjectValue is null;

    public override TObject? Default => IsNull
        ? default
        : throw (IsException ? Exception : new InvalidCastException(ResultIsNotNull));

    public static implicit operator Maybe<TObject>(Object<TObject> @object) => CreateFor(@object);
    public static implicit operator Maybe<TObject>(TObject? value) => CreateFor(value);
    public static implicit operator Maybe<TObject>(Exception exception) => CreateFor(exception);
    public static implicit operator Maybe<TObject>(Failure failure) => CreateFor(failure.Errors);
    public static implicit operator Maybe<TObject>(List<ValidationError> errors) => CreateFor(errors);
    public static implicit operator Maybe<TObject>(ValidationError[] errors) => CreateFor(errors);
    public static implicit operator Maybe<TObject>(ValidationError error) => CreateFor(new[] { error });

    public static implicit operator TObject?(Maybe<TObject> input) => input.ObjectValue;

    public static Maybe<TObject> operator +(Maybe<TObject> left, Success _) => left;
    public static Maybe<TObject> operator +(Maybe<TObject> left, ValidationError right) => left.AddErrors(new[] { right });
    public static Maybe<TObject> operator +(Maybe<TObject> left, ICollection<ValidationError> right) => left.AddErrors(right);
}