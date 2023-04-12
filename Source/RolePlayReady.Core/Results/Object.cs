namespace System.Results;

public sealed class Object<TObject> : Result<Object<TObject>, TObject> {

    public Object() {
    }

    public Object(TObject value) : base(Ensure.NotNull(value)) {
    }

    public Object(ICollection<ValidationError> errors) : base(errors) {
    }

    public Object(Exception exception) : base(exception) {
    }

    public override bool IsNull => false;

    public override TObject Default => throw (IsException ? Exception : new InvalidCastException(ResultIsNotNull));

    //public static implicit operator Result<TObject>(Validation result) => CreateFor<Result<TObject>, TObject>(result);
    public static implicit operator Object<TObject>(Maybe<TObject> result) => CreateFor(result);
    public static implicit operator Object<TObject>(TObject? value) => CreateFor(value);
    public static implicit operator Object<TObject>(Exception exception) => CreateFor(exception);
    public static implicit operator Object<TObject>(Failure failure) => CreateFor(failure.Errors);
    public static implicit operator Object<TObject>(List<ValidationError> errors) => CreateFor(errors);
    public static implicit operator Object<TObject>(ValidationError[] errors) => CreateFor(errors);
    public static implicit operator Object<TObject>(ValidationError error) => CreateFor(new[] { error });

    public static implicit operator TObject(Object<TObject> input) => input.Value;

    public static Object<TObject> operator +(Object<TObject> left, Success _) => left;
    public static Object<TObject> operator +(Object<TObject> left, ValidationError right) => left.AddErrors(new[] { right });
    public static Object<TObject> operator +(Object<TObject> left, ICollection<ValidationError> right) => left.AddErrors(right);
}