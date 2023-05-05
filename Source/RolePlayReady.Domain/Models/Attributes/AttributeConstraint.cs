using System.Validation.Commands;

namespace RolePlayReady.Models.Attributes;

public sealed record AttributeConstraint : IAttributeConstraint {
    public AttributeConstraint(string validatorName, params object[] arguments) {
        ValidatorName = validatorName;
        Arguments = arguments;
    }
    public string ValidatorName { get; }
    public ICollection<object> Arguments { get; }

    public IValidationCommand Create<TType>(string definitionName)
        => ValidationCommandFactory
          .For(definitionName)
          .Create(typeof(TType), ValidatorName, Arguments.ToArray());
}