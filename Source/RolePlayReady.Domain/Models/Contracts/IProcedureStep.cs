namespace RolePlayReady.Models.Contracts;

public interface IProcedureStep : IIdentification, ITransferable<IProcedureStep, IProcedure> {
    IProcedure Procedure { get; set; }
    Func<IEntity, ProcedureStepResult?> Execute { get; init; }
}

public interface IProcedureStep<TResult> : IProcedureStep {
    new Func<IEntity, ProcedureStepResult<TResult>> Execute { get; init; }
}