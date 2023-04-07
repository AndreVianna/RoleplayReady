namespace RolePlayReady.Models.Contracts;

public interface INode {
    INode Root { get; }
    INode? Parent { get; }
    IList<INode> Children { get; }
}
