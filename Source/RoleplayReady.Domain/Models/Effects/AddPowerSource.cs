namespace RoleplayReady.Domain.Models.Effects;

public record AddPowerSource : Effect {
    [SetsRequiredMembers]
    public AddPowerSource(string name, string description, Action<IFluentBuilder> build, Usage usage = Usage.Closed, Status status = Status.Public,
        ISource? source = null)
        : base(e => {
            e.Configure(nameof(Element.PowerSources)).For(e.OwnerId).WithUsage(usage).WithStatus(status).WithSource(source).As(powerSources => powerSources
                .Add(name, description, build));
            return e;
        }) { }
}
