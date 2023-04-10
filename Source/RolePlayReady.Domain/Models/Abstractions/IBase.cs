namespace RolePlayReady.Models.Abstractions;

public interface IBase<out TKey> : IDescribed, IPersistent<TKey> {
    IList<string> Tags { get; }
}