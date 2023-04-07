namespace RolePlayReady.Models;

public abstract record Node : INode {
    protected Node() { }

    [SetsRequiredMembers]
    protected Node(INode parent) {
        Parent = parent;
    }

    public INode Root => Parent.Root;
    public required INode Parent { get; init; }
    public IList<INode> Children { get; init; } = new List<INode>();
}