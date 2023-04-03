﻿using RolePlayReady.Models.Contracts;

namespace RolePlayReady.Operations.Changes;

public interface IEntityChange
    : IEntityOperation<IEntityChange, IEntity> {
    Func<IEntity, IEntity> Apply { get; init; }
}