using RolePlayReady.Models.Contracts;

namespace RolePlayReady.Models;

public abstract record ProcedureStepResult : IMayHaveAResult {
    public IProcedureStep? Next { get; init; }
    public object? Result { get; init; } = default!;
}