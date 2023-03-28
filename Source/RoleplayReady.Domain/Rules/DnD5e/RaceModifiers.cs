namespace RoleplayReady.Domain.Rules.DnD5e;

// ReSharper disable once InconsistentNaming - Dnd5e is the official name of the system.
public static partial class DnD5eDefinitions
{
    public static void AddRaceModifiers(this DnD5e system)
    {
        system.Modifiers.Add(new Modifier
        {
            System = system,
            Name = "SetElfRace",
            IsApplicable = e => e.Type.Name == "Character",
            Apply = e =>
            {
                var race = GetElementProperty<string>(e, "Race");
                race.Value = "Elf";
                e.Features.Add(CreateElementProperty<string>(e, "Subrace"));
                return e;
            },
            Validations = Array.Empty<ElementValidation>()
        });
        system.Modifiers.Add(new Modifier
        {
            System = system,
            Name = "SetElfAbilityScoreChange",
            IsApplicable = e =>
            {
                var race = GetElementProperty<string>(e, "Race").Value;
                var dexterity = GetElementProperty<int?>(e, "Dexterity").Value;
                return race == "Elf" && dexterity != null;
            },
            Apply = e =>
            {
                var dexterity = GetElementProperty<int?>(e, "Dexterity");
                dexterity.Value += 2;
                return e;
            },
            Validations = Array.Empty<ElementValidation>()
        });
        system.Modifiers.Add(new Modifier
        {
            System = system,
            Name = "SetElfSpeed",
            IsApplicable = e =>
            {
                var race = GetElementProperty<string>(e, "Race").Value;
                return race == "Elf";
            },
            Apply = e =>
            {
                var movements = GetElementProperty<Dictionary<string, int>>(e, "Movements").Value!;
                movements["Move"] = 30;
                return e;
            },
            Validations = Array.Empty<ElementValidation>()
        });
        system.Modifiers.Add(new Modifier
        {
            System = system,
            Name = "SetElfLifespan",
            IsApplicable = e =>
            {
                var race = GetElementProperty<string>(e, "Race").Value;
                return race == "Elf";
            },
            Validations = new List<ElementValidation>
            {
                new ElementValidation
                {
                    Severity = ValidationSeverityLevel.Hint,
                    Validate = e =>
                    {
                        var age = GetElementProperty<int?>(e, "Age").Value;
                        return age is > 750;
                    },
                    Message = "Elves should live only up to 750 years. Please, consider changing your age.",
                },
                new ElementValidation
                {
                    Severity = ValidationSeverityLevel.Hint,
                    Validate = e =>
                    {
                        var age = GetElementProperty<int?>(e, "Age").Value;
                        return age is < 100;
                    },
                    Message = "Elves only mature at the age of 100 years. Please, consider changing your age.",
                },
            }
        });
        system.Modifiers.Add(new Modifier
        {
            System = system,
            Name = "SetElfTrance",
            IsApplicable = e =>
            {
                var race = GetElementProperty<string>(e, "Race").Value;
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
            Validations = Array.Empty<ElementValidation>()
        });
        system.Modifiers.Add(new Modifier
        {
            System = system,
            Name = "AddElfSubrace",
            IsApplicable = e =>
            {
                var race = GetElementProperty<string>(e, "Race").Value;
                return race == "Elf";
            },
            Apply = e => e,
            Validations = new List<ElementValidation>
            {
                new ElementValidation
                {
                    Severity = ValidationSeverityLevel.Error,
                    Validate = e =>
                    {
                        var allowedSubraces = new[] { "High Elf", "Wood Elf", "Dark Elf (Drow)" };
                        var subrace = GetElementProperty<string>(e, "Subrace").Value;
                        return allowedSubraces.Contains(subrace);
                    },
                    Message = "Subrace not set.",
                }
            }
        });
    }
}
