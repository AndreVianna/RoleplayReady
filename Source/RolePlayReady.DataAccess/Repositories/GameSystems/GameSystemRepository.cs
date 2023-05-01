﻿namespace RolePlayReady.DataAccess.Repositories.GameSystems;

public class GameSystemRepository : Repository<GameSystem, Row, GameSystemData>, IGameSystemRepository {
    public GameSystemRepository(IJsonFileHandler<GameSystemData> files, GameSystemMapper mapper, IUserAccessor owner)
        : base(files, mapper) {
        files.SetBasePath($"{owner.BaseFolder}/GameSystems");
    }
}
