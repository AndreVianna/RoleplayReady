namespace RoleplayReady.Domain.Models.Contracts;

public interface IAmOwnedBy {
    string OwnerId { get; init; }
}