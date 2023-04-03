using RolePlayReady.Models.Contracts;

namespace RolePlayReady.Utilities.Contracts;

public interface ISectionBuilder {
    public interface IMainCommands {
        IConnector Add(string name, string description, Action<IComponentUpdater.IMain> configure);
        IConnector Add(string name, string description, Action<IEntity, IComponentUpdater.IMain> configure);

        IConnector Remove(string existing);
        IReplaceWith Replace(string existing);
        IAppendWith Append(string existing);
    }

    public interface IConnector {
        IMainCommands And();
    }

    public interface IReplaceWith {
        IConnector With(string name, string description, Action<IComponentUpdater.IMain> configure);
        IConnector With(string name, string description, Action<IEntity, IComponentUpdater.IMain> configure);
    }

    public interface IAppendWith {
        IConnector With(string additionalDescription, Action<IComponentUpdater.IMain> configure);
        IConnector With(string additionalDescription, Action<IEntity, IComponentUpdater.IMain> configure);
        IConnector With(Action<IComponentUpdater.IMain> configure);
        IConnector With(Action<IEntity, IComponentUpdater.IMain> configure);
    }
}
