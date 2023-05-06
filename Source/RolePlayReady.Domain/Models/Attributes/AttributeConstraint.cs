﻿using System.Validation.Commands;
using System.Validation.Commands.Abstractions;

namespace RolePlayReady.Models.Attributes;

public sealed record AttributeConstraint : IAttributeConstraint {
    public AttributeConstraint(string validatorName, params object[] arguments) {
        ValidatorName = validatorName;
        Arguments = arguments;
    }
    public string ValidatorName { get; }
    public ICollection<object> Arguments { get; }

    public IValidationCommand Create<TValue>(TValue value, string definitionName)
        => ValidationCommandFactory
          .For(value, definitionName)
          .Create(ValidatorName, Arguments.ToArray());
}