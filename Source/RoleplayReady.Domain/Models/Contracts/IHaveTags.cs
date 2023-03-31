namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveTags {
    public IList<string> Tags { get; init; }
}