namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveActors {
    public IList<IActor> Actors { get; }
}