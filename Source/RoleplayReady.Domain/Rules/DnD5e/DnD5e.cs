namespace RoleplayReady.Domain.Rules.DnD5e;

public record DnD5e : GameSystem
{
    public DnD5e()
    {
        this.AddEntityTypeModifiers();
        this.AddRaceModifiers();
    }
}
