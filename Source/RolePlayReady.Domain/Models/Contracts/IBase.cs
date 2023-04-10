namespace RolePlayReady.Models.Contracts;

public interface IBase : IDescribed, IPersistent {
    IList<string> Tags { get; }
}