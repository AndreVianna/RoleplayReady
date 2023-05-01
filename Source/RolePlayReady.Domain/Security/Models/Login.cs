﻿namespace RolePlayReady.Security.Models;

public record Login : IValidatable {
    public required string Email { get; set; }
    public required string Password { get; set; }

    public ValidationResult Validate() {
        var result = ValidationResult.AsSuccess();
        result += Email.IsNotNull()
                       .And.IsNotEmptyOrWhiteSpace()
                       .And.IsEmail().Result;
        result += Password.IsNotNull()
                          .And.IsNotEmptyOrWhiteSpace()
                          .And.MaximumLengthIs(Validation.Password.MaximumLength).Result;
        return result;
    }
}
