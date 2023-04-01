namespace RoleplayReady.Domain.Utilities;

public interface ISectionBuilder {
    ISectionBuilder Add(string name, string description, Action<IFluentBuilder> configure);
    ISectionBuilder Add(string name, string description, Action<IElement, IFluentBuilder> configure);

    ISectionBuilder Remove(string existing);

    IReplaceContinuation Replace(string existing, string name, string description, Action<IFluentBuilder> configure);
    ISectionBuilder Replace(string existing, string name, string description, Action<IElement, IFluentBuilder> configure);

    ISectionBuilder IncludeIn(string existing, string description, Action<IFluentBuilder> configure);
    ISectionBuilder IncludeIn(string existing, string description, Action<IElement, IFluentBuilder> configure);

    public interface IReplaceContinuation {
        ISectionBuilder With(string name, string description, Action<IFluentBuilder> configure);
        ISectionBuilder With(string name, string description, Action<IElement, IFluentBuilder> configure);
    }
}