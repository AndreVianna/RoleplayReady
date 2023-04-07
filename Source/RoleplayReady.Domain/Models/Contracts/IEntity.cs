namespace RolePlayReady.Models.Contracts;

public interface IEntity : INode, IIdentification, ITrackable {

    //Usage Usage { get; init; }

    IList<string> Tags { get; init; }
    IList<IEntityAttribute> Attributes { get; init; }
}