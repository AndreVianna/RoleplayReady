namespace RoleplayReady.Domain.Models;

public interface IHasFeatures {
    IList<Feature> Features { get; }
}