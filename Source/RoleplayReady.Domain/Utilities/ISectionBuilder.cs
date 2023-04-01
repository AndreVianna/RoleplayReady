namespace RoleplayReady.Domain.Utilities;

public interface ISectionBuilder {
    public interface IMainCommands {
        IConnector Add(string name, string description, Action<IElementUpdater.IMain> configure);
        IConnector Add(string name, string description, Action<IEntity, IElementUpdater.IMain> configure);

        IConnector Remove(string existing);
        IReplaceWith Replace(string existing);
        IAppendWith Append(string existing);
    }

    public interface IConnector {
        IMainCommands And();
    }

    public interface IReplaceWith {
        IConnector With(string name, string description, Action<IElementUpdater.IMain> configure);
        IConnector With(string name, string description, Action<IEntity, IElementUpdater.IMain> configure);
    }

    public interface IAppendWith {
        IConnector With(string additionalDescription, Action<IElementUpdater.IMain> configure);
        IConnector With(string additionalDescription, Action<IEntity, IElementUpdater.IMain> configure);
        IConnector With(Action<IElementUpdater.IMain> configure);
        IConnector With(Action<IEntity, IElementUpdater.IMain> configure);
    }
}
