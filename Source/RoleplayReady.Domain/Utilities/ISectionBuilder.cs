namespace RoleplayReady.Domain.Utilities;

public interface ISectionBuilderMainCommands {
    ISectionBuilderCommandConnector Add(string name, string description, Action<IElementUpdater.IElementUpdaterMain> configure);
    ISectionBuilderCommandConnector Add(string name, string description, Action<IElement, IElementUpdater.IElementUpdaterMain> configure);

    ISectionBuilderCommandConnector Remove(string existing);
    ISectionBuilderReplaceContinuation Replace(string existing);
    ISectionBuilderAppendContinuation Append(string existing);
}

public interface ISectionBuilderCommandConnector {
    ISectionBuilderMainCommands And { get; }
}

public interface ISectionBuilderReplaceContinuation {
    ISectionBuilderCommandConnector With(string name, string description, Action<IElementUpdater.IElementUpdaterMain> configure);
    ISectionBuilderCommandConnector With(string name, string description, Action<IElement, IElementUpdater.IElementUpdaterMain> configure);
}

public interface ISectionBuilderAppendContinuation {
    ISectionBuilderCommandConnector With(Action<IElementUpdater.IElementUpdaterMain> configure);
    ISectionBuilderCommandConnector With(Action<IElement, IElementUpdater.IElementUpdaterMain> configure);
    ISectionBuilderCommandConnector With(string additionalDescription, Action<IElementUpdater.IElementUpdaterMain> configure);
    ISectionBuilderCommandConnector With(string additionalDescription, Action<IElement, IElementUpdater.IElementUpdaterMain> configure);
}
