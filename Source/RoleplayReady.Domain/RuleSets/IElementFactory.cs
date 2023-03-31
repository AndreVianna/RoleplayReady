namespace RoleplayReady.Domain.RuleSets;

public interface IElementFactory {
    IElement Create(string type, string name, string description);
}