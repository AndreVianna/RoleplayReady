namespace RolePlayReady.Models.Abstractions;

public interface IBase<out TKey> : IDescribed, IPersistent<TKey>, IValidatable {
    ICollection<string> Tags { get; }
}