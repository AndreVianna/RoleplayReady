namespace RoleplayReady.Domain.Rules.DnD5e;

// ReSharper disable once InconsistentNaming - Dnd5e is the official name of the system.
public record DnD5e : Models.System
{
    public DnD5e()
    {
        this.AddProperty<string>("Size");
        this.AddProperty<int>("Strength");
        this.AddProperty<int>("Dexterity");
        this.AddProperty<int>("Constitution");
        this.AddProperty<int>("Intelligence");
        this.AddProperty<int>("Wisdom");
        this.AddProperty<int>("Charisma");
        this.AddProperty<int>("ProficiencyBonus");
        this.AddProperty<string[]>("SavingThrowProficiencies");
        this.AddProperty<string[]>("SkillProficiencies");
        this.AddProperty<string>("PassivePerception");
        this.AddProperty<string>("PassiveInsight");
        this.AddProperty<int>("MaximumHitPoints");
        this.AddProperty<int>("CurrentHitPoints");
        this.AddProperty<int>("TemporaryHitPoints");
        this.AddProperty<int>("ArmorClass");
        this.AddProperty<Dictionary<string, int>>("Moviments");
        this.AddProperty<string>("Alignment");
        this.AddProperty<string[]>("Languages");
        this.AddProperty<string[]>("Senses");
        this.AddProperty<string[]>("Resistances");
        this.AddProperty<string[]>("Immunities");
        this.AddProperty<string[]>("Vulnerabilities");
        this.AddProperty<string[]>("Traits");
        this.AddProperty<string>("CreatureType");
        this.AddProperty<int>("ChallengeRating");
        this.AddProperty<string[]>("Actions");
        this.AddProperty<string[]>("LegendaryActions");
        this.AddProperty<string[]>("LairActions");
        this.AddProperty<string>("NPCType");
        this.AddProperty<string>("Race");
        this.AddProperty<Class[]>("Classes");
        this.AddProperty<string>("Background");
        this.AddProperty<int>("ChallengeRating");
        this.AddProperty<string[]>("Actions");
        this.AddProperty<string[]>("LegendaryActions");
        this.AddProperty<string[]>("LairActions");
        this.AddProperty<string>("Race");
        this.AddProperty<Class[]>("Classes");
        this.AddProperty<string[]>("Archetypes");
        this.AddProperty<string>("Background");
        this.AddProperty<Dictionary<string, int>>("AbilityCharges");
        this.AddProperty<string[]>("Feats");
        this.AddProperty<Dictionary<string, int>>("Currency");
        this.AddProperty<bool>("Inspiration");
        this.AddProperty<int>("ExhaustionLevel");
        this.AddProperty<int>("CarryingCapacity");
        this.AddProperty<int>("CurrentCarryingWeight");
        this.AddProperty<string>("EyeColor");
        this.AddProperty<string>("Gender");
        this.AddProperty<decimal>("Height");
        this.AddProperty<decimal>("Weight");
        this.AddProperty<string>("HairColor");
        this.AddProperty<string>("SkinColor");
        this.AddProperty<string>("Appearance");


        this.AddEntityTypeModifiers();
        this.AddRaceModifiers();
    }
}
