namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveSources {
    IList<ISource> Sources { get; }
}