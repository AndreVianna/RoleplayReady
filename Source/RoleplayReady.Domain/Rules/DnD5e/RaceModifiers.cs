namespace RoleplayReady.Domain.Rules.DnD5e;

public static partial class DnD5eDefinitions
{
    public static void AddRaceModifiers(this DnD5e system)
    {
        system.Modifiers.Add(new Modifier
        {
            GameSystem = system,
            Name = "SetElfRace",
            IsApplicable = e => e.Type == EntityType.Character,
            Apply = e =>
            {
                var race = GetEntityProperty<string>(e, "Race");
                race.Value = "Elf";
                e.Properties.Add(CreateEntityProperty<string>(e, "Subrace"));
                return e;
            },
            Validations = Array.Empty<Validation>()
        });
        system.Modifiers.Add(new Modifier
        {
            GameSystem = system,
            Name = "SetElfAbilityScoreChange",
            IsApplicable = e =>
            {
                var race = GetEntityProperty<string>(e, "Race").Value;
                var dexterity = GetEntityProperty<int?>(e, "Dexterity").Value;
                return race == "Elf" && dexterity != null;
            },
            Apply = e =>
            {
                var dexterity = GetEntityProperty<int?>(e, "Dexterity");
                dexterity.Value += 2;
                return e;
            },
            Validations = Array.Empty<Validation>()
        });
        system.Modifiers.Add(new Modifier
        {
            GameSystem = system,
            Name = "SetElfSpeed",
            IsApplicable = e =>
            {
                var race = GetEntityProperty<string>(e, "Race").Value;
                return race == "Elf";
            },
            Apply = e =>
            {
                e.Properties.OfType<EntityProperty<Dictionary<string, int>>>().First(p => p.Property.Name == "Movements").Value!["Move"] = 30;
                return e;
            },
            Validations = Array.Empty<Validation>()
        });
        system.Modifiers.Add(new Modifier
        {
            GameSystem = system,
            Name = "SetElfLifespan",
            IsApplicable = e =>
            {
                var race = GetEntityProperty<string>(e, "Race").Value;
                return race == "Elf";
            },
            Validations = new List<Validation>
            {
                new Validation
                {
                    Severity = ValidationSeverityLevel.Hint,
                    Validate = e =>
                    {
                        var age = GetEntityProperty<int?>(e, "Age").Value;
                        return age is > 750;
                    },
                    Message = "Elves should live only up to 750 years. Please, consider changing your age.",
                },
                new Validation
                {
                    Severity = ValidationSeverityLevel.Hint,
                    Validate = e =>
                    {
                        var age = GetEntityProperty<int?>(e, "Age").Value;
                        return age is < 100;
                    },
                    Message = "Elves only mature at the age of 100 years. Please, consider changing your age.",
                },
            }
        });
        system.Modifiers.Add(new Modifier
        {
            GameSystem = system,
            Name = "SetElfTrance",
            IsApplicable = e =>
            {
                var race = GetEntityProperty<string>(e, "Race").Value;
                return race == "Elf";
            },
            Apply = e =>
            {
                e.Entries.Add(new Entry
                {
                    Type = EntryType.Trait,
                    Title = "Trance",
                    Text = "Elves don't need to sleep. Instead, they meditate deeply for 4 hours a day."
                });
                return e;
            },
            Validations = Array.Empty<Validation>()
        });
        system.Modifiers.Add(new Modifier
        {
            GameSystem = system,
            Name = "AddElfSubrace",
            IsApplicable = e =>
            {
                var race = GetEntityProperty<string>(e, "Race").Value;
                return race == "Elf";
            },
            Apply = e => e,
            Validations = new List<Validation>
            {
                new Validation
                {
                    Severity = ValidationSeverityLevel.Error,
                    Validate = e =>
                    {
                        var allowedSubraces = new[] { "High Elf", "Wood Elf", "Dark Elf (Drow)" };
                        var subrace = GetEntityProperty<string>(e, "Subrace").Value;
                        return allowedSubraces.Contains(subrace);
                    },
                    Message = "Subrace not set.",
                }
            }
        });
    }
}
