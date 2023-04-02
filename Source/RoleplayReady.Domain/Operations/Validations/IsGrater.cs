﻿namespace RoleplayReady.Domain.Operations.Validations;

public record IsGrater<TValue> : EntityValidation
    where TValue : IComparable<TValue> {
    [SetsRequiredMembers]
    public IsGrater(string attributeName, TValue minimum, string message)
        : base((e, errors) => {
            var value = e.GetValue<TValue>(attributeName);
            if (value is null || value.CompareTo(minimum) <= 0)
                errors.Insert(0, new ValidationError(message));

            return errors;
        }) {
    }
}