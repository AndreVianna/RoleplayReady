namespace RoleplayReady.Domain.Models.Contracts;

public interface IEntity
    : IAmOwnedBy,
	IAmChildOf,
	IAmPartOf, 
    IAmKnownAs,
    IAmAlsoKnownAs,
    IMayHaveADescription,
    IAmTracked {
}