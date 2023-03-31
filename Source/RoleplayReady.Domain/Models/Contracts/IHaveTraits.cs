namespace RoleplayReady.Domain.Models.Contracts;

public interface IHavePowerSources {
    IList<IPowerSource> PowerSources { get; init; }
}

public interface IHaveTraits {
    IList<ITrait> Traits { get; init; }
}