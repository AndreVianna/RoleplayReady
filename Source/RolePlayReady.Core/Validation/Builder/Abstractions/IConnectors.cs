namespace System.Validation.Builder.Abstractions;

public interface ITerminator {
    ValidationResult Result { get; }
}

public interface IInitiator<out TValidators> {
    TValidators Is();
}

public interface IConnectors<out TSubject, out TValidators>
    : ITerminator,
      IBinaryConnectors<TValidators>,
      IBinaryOperators<TSubject, TValidators>,
      IUnaryConnectors<TValidators>,
      IUnaryOperators<TSubject, TValidators>
    where TValidators : IValidators {
}

public interface IBinaryOperators<out TSubject, out TValidators>
    where TValidators : IValidators {
    TValidators And(Func<TSubject, ITerminator> validateRight);
    TValidators Or(Func<TSubject, ITerminator> validateRight);
}

public interface IBinaryConnectors<out TValidators>
    where TValidators : IValidators {
    TValidators And();
    TValidators Or();
}

public interface IUnaryOperators<out TSubject, out TValidators>
    where TValidators : IValidators {
    IConnectors<TSubject, TValidators> Not(Func<TSubject, ITerminator> validateRight);
}

public interface IUnaryConnectors<out TValidators>
    where TValidators : IValidators {
    TValidators Not();
}
