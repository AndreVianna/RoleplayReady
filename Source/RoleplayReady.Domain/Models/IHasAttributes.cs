namespace RoleplayReady.Domain.Models;

public interface IHasAttributes
{
    IList<IElementAttribute> Attributes { get; init; }
}