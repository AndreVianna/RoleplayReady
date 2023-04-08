namespace RolePlayReady.Models.Contracts;

public interface IEntity : IIdentification, ITrackable {
    IList<string> Tags { get; init; }
    IList<IAttribute> Attributes { get; init; }
}