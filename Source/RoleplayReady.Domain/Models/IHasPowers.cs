namespace RoleplayReady.Domain.Models;

public interface IHasPowers {
    IList<Power> Powers { get; }
}