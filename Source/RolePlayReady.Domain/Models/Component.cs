using System.Diagnostics;

namespace RolePlayReady.Models;

public abstract record Component : Persisted {
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public ICollection<IAttribute> Attributes { get; init; } = new List<IAttribute>();
}