namespace RoleplayReady.Domain.Utilities;

public interface ISectionBuilder {
    ISectionBuilder Add(string name, Action<IFluentBuilder> configure);
    ISectionBuilder Add(string name, Action<IElement, IFluentBuilder> configure);
    ISectionBuilder Add(string name, string description, Action<IFluentBuilder> configure);
    ISectionBuilder Add(string name, string description, Action<IElement, IFluentBuilder> configure);
}