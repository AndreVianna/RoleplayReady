namespace RoleplayReady.Domain.Models;

public interface IHasTags {
    IList<string> Tags { get; }
}