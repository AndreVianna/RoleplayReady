﻿namespace RoleplayReady.Domain.Models;

public record ElementType
{
    // System and Name must be unique.
    public required GameSystem System { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}