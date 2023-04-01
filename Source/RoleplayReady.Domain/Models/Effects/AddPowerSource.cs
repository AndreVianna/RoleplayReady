namespace RoleplayReady.Domain.Models.Effects;

public record AddPowerSource : Effect {
    [SetsRequiredMembers]
    public AddPowerSource(string name, string description, Action<IElement, IElementUpdater.IElementUpdaterMain> configure)
        : base(e => e.Configure(nameof(Element.PowerSources)).As(ps => ps.Add(name, description, configure))) { }
}
