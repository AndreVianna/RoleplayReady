using RoleplayReady.Domain.Models.Contracts;

namespace RoleplayReady.Domain.Models.Effects;

public record AddTag : Effect {
    [SetsRequiredMembers]
    public AddTag(string tag)
        : base(e => {
            e.Tags.Add(tag);
            return e;
        }) { }
}