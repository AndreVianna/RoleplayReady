namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveAStart {
    IProcessStep Start { get; init; }
}