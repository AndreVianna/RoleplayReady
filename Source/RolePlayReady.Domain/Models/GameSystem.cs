namespace RolePlayReady.Models;

public record GameSystem : Base<Guid>, IGameSystem {
    public GameSystem(IDateTime? dateTime = null)
        : base(dateTime) {
    }
}