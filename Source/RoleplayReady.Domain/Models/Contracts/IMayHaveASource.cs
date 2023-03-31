namespace RoleplayReady.Domain.Models.Contracts;

public interface IMayHaveASource {
    public ISource? Source { get; init; }
}