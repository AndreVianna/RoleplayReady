namespace RolePlayReady.Models.Abstractions;

public interface IBase : IDescribed, IPersistent {
    IList<string> Tags { get; }
}