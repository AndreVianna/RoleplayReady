namespace RoleplayReady.Domain.Utilities;

public interface IElementFactory {
    IElement Create(string type, string name, string? description = null, Status? status = null, Usage? usage = null, ISource? source = null);
}