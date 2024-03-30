namespace System.Validation.Builder.Abstractions;

public interface IConnector<out TValidator>
    : ITerminator,
      IBinaryConnector<TValidator>,
      IBinaryOperator<TValidator>
    where TValidator : IValidator;
