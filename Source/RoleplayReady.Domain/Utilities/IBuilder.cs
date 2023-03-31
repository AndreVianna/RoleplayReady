namespace RoleplayReady.Domain.Utilities;

public interface IBuilder {
    IBuilder Add(string name, string description, Action<IFluentBuilder> build);
}
