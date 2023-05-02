namespace RolePlayReady.Handlers.GameSystem;

public class GameSystemHandler
    : CrudHandler<GameSystem, IGameSystemRepository>,
      IGameSystemHandler {
    public GameSystemHandler(IGameSystemRepository repository)
        : base(repository) {
    }
}
