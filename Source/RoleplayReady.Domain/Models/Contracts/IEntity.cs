namespace RoleplayReady.Domain.Models.Contracts;

public interface IEntity : IAmOwnedBy,
    IAmKnownAs,
    IMayHaveADescription,
    IAmTracked {
}