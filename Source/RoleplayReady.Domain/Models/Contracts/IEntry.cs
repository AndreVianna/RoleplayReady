namespace RoleplayReady.Domain.Models.Contracts;

public interface IEntry : IAmTracked, IUseSoftDelete {
    EntrySection Section { get; init; }
    string Title { get; init; }
    string Text { get; init; }
}