namespace RolePlayReady.Models.Abstractions;

public interface IBase<out TKey> : IDescribed, IPersistent<TKey>, IValidatable {
    IList<string> Tags { get; }
}