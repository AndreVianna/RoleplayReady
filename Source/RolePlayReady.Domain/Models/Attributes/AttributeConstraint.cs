using System.Validators;
using System.Validators.Abstractions;

namespace RolePlayReady.Models.Attributes;

public sealed record AttributeConstraint : IAttributeConstraint {
    public AttributeConstraint(string validatorName, params object[] arguments) {
        ValidatorName = validatorName;
        Arguments = arguments;
    }
    public string ValidatorName { get; }
    public ICollection<object> Arguments { get; }

    public IValidator Create<TType>(string definitionName)
        => ValidatorFactory
          .For(definitionName)
          .Create(typeof(TType), ValidatorName, Arguments.ToArray());
}