namespace RoleplayReady.Domain.Models.Contracts;

public interface IUseSoftDelete {
    public bool IsDeleted { get; init; }
}