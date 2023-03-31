namespace RoleplayReady.Domain.Models.Contracts;

public interface IHavePowers {
    IList<IPower> Powers { get; init; }
}