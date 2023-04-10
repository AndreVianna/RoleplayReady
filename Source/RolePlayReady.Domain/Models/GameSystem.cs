namespace RolePlayReady.Models;

public record GameSystem : Base, IGame {
    public GameSystem(IDateTime? dateTime = null)
        : base(dateTime) {
    }
}