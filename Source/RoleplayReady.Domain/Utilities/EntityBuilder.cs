using RolePlayReady.Utilities.Contracts;

namespace RolePlayReady.Utilities;

public class EntityBuilder {
    private readonly IEntity _entity;
    private readonly string _section;

    private EntityBuilder(IEntity entity, string section) {
        _entity = entity;
        _section = section;
    }

    public static EntityBuilder For(IEntity element, string section)
        => new EntityBuilder(element, section);

    public IEntity As(Action<ISectionBuilder.IMainCommands> configure) {
        configure(SectionBuilder.For(_entity, _section));
        return _entity;
    }
}