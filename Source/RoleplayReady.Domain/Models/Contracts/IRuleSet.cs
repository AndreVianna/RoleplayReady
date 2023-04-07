namespace RolePlayReady.Models.Contracts;

public interface IRuleSet : INode, IIdentification, ITrackable {
    IList<string> Tags { get; }
    IList<IAttribute> Attributes { get; }
}