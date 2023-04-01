namespace RoleplayReady.Domain.Utilities;

public interface IElementFactory {
    TElement Create<TElement>(string name, string description, State? state = null, Usage? usage = null, ISource? source = null)
        where TElement : IElement;

    IElement Create(string type, string name, string description, State? state = null, Usage? usage = null, ISource? source = null);
}