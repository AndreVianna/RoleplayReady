namespace RolePlayReady.Models.Contracts;

public interface IMayHaveASource {
    ISource? Source { get; init; }
}