﻿namespace RolePlayReady.Models;

public record Row : IKey {
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}