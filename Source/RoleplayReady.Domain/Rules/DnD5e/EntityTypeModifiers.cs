namespace RoleplayReady.Domain.Rules.DnD5e;

public static partial class DnD5eDefinitions
{
    public static void AddEntityTypeModifiers(this DnD5e system)
    {
        system.Modifiers.Add(new Modifier
        {
            GameSystem = system,
            Name = "SetEntity",
            IsApplicable = _ => true,
            Apply = e =>
            {
                e.Properties.Add(CreateEntityProperty<string>(e, "Size"));
                e.Properties.Add(CreateEntityProperty<int>(e, "Strength"));
                e.Properties.Add(CreateEntityProperty<int>(e, "Dexterity"));
                e.Properties.Add(CreateEntityProperty<int>(e, "Constitution"));
                e.Properties.Add(CreateEntityProperty<int>(e, "Intelligence"));
                e.Properties.Add(CreateEntityProperty<int>(e, "Wisdom"));
                e.Properties.Add(CreateEntityProperty<int>(e, "Charisma"));
                e.Properties.Add(CreateEntityProperty<int>(e, "ProficiencyBonus"));
                e.Properties.Add(CreateEntityProperty<string[]>(e, "SavingThrowProficiencies"));
                e.Properties.Add(CreateEntityProperty<string[]>(e, "SkillProficiencies"));
                e.Properties.Add(CreateEntityProperty<string>(e, "PassivePerception"));
                e.Properties.Add(CreateEntityProperty<string>(e, "PassiveInsight"));
                e.Properties.Add(CreateEntityProperty<int>(e, "MaximumHitPoints"));
                e.Properties.Add(CreateEntityProperty<int>(e, "CurrentHitPoints"));
                e.Properties.Add(CreateEntityProperty<int>(e, "TemporaryHitPoints"));
                e.Properties.Add(CreateEntityProperty<int>(e, "ArmorClass"));
                e.Properties.Add(CreateEntityProperty<Dictionary<string, int>>(e, "Moviments"));
                e.Properties.Add(CreateEntityProperty<string>(e, "Alignment"));
                e.Properties.Add(CreateEntityProperty<string[]>(e, "Languages"));
                e.Properties.Add(CreateEntityProperty<string[]>(e, "Senses"));
                e.Properties.Add(CreateEntityProperty<string[]>(e, "Resistances"));
                e.Properties.Add(CreateEntityProperty<string[]>(e, "Immunities"));
                e.Properties.Add(CreateEntityProperty<string[]>(e, "Vulnerabilities"));
                e.Properties.Add(CreateEntityProperty<string[]>(e, "Traits"));
                return e;
            },
            Validations = new List<Validation>
            {
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "Strength", strength => strength is < 0 or > 30, "Invalid strength."),
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "Dexterity", strength => strength is < 0 or > 30, "Invalid dexterity."),
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "Constitution", strength => strength is < 0 or > 30, "Invalid constitution."),
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "Intelligence", strength => strength is < 0 or > 30, "Invalid intelligence."),
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "Wisdom", strength => strength is < 0 or > 30, "Invalid wisdom."),
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "Charisma", strength => strength is < 0 or > 30, "Invalid charisma."),
                CreatePropertyValidation<Dictionary<string, int>>(ValidationSeverityLevel.Error, "Moviments", moviments => (moviments ?? new Dictionary<string, int>()).All(move => move.Value >= 0), "Invalid speed."),
                CreatePropertyValidation<string[]>(ValidationSeverityLevel.Error, "SavingThrowProficiencies", savingThrows =>
                {
                    var allowedAbilities = new[] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };
                    if (savingThrows is null || savingThrows.Length == 0) return true;
                    return savingThrows.All(x => allowedAbilities.Contains(x));
                }, "At least one of the listed saving throw proficiencies is invalid."),
                CreatePropertyValidation<string[]>(ValidationSeverityLevel.Error, "SkillProficiencies", skills =>
                {
                    var allowedSkillProficiencies = new[]
                    {
                        "Acrobatics", "Animal Handling", "Arcana", "Athletics", "Deception", "History", "Insight", "Intimidation", "Investigation",
                        "Medicine", "Nature", "Perception", "Performance", "Persuasion", "Religion", "Sleight of Hand", "Stealth", "Survival"
                    };
                    if (skills is null || skills.Length == 0) return true;
                    return skills.All(x => allowedSkillProficiencies.Contains(x));
                }, "At least one of the skill throw proficiencies is invalid."),
                CreatePropertyValidation<string>(ValidationSeverityLevel.Error, "Size", size =>
                {
                    var allowedSizes = new[] { "Tiny", "Small", "Medium", "Large", "Huge", "Gargantuan" };
                    return !string.IsNullOrWhiteSpace(size) && allowedSizes.Contains(size);
                }, "Invalid size."),
                CreatePropertyValidation<string>(ValidationSeverityLevel.Error, "Alignment", alignment =>
                {
                    var allowedAlignments = new[]
                    {
                        "Lawful Good", "Neutral Good", "Chaotic Good",
                        "Lawful Neutral", "True Neutral", "Chaotic Neutral",
                        "Lawful Evil", "Neutral Evil", "Chaotic Evil",
                        "Unaligned"
                    };
                    return !string.IsNullOrWhiteSpace(alignment) && allowedAlignments.Contains(alignment);
                }, "Invalid alignment."),
            }
        });
        system.Modifiers.Add(new Modifier
        {
            GameSystem = system,
            Name = $"Set{EntityType.Creature}",
            IsApplicable = e => e.Type == EntityType.Creature,
            Apply = e =>
            {
                e.Properties.Add(CreateEntityProperty<string>(e, "CreatureType"));
                e.Properties.Add(CreateEntityProperty<int>(e, "ChallengeRating"));
                e.Properties.Add(CreateEntityProperty<string[]>(e, "Actions"));
                e.Properties.Add(CreateEntityProperty<string[]>(e, "LegendaryActions"));
                e.Properties.Add(CreateEntityProperty<string[]>(e, "LairActions"));
                return e;
            },
            Validations = new List<Validation>
            {
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "ChallengeRating", cr => cr is < 0, "Invalid challenge rating."),
            }
        });
        system.Modifiers.Add(new Modifier
        {
            GameSystem = system,
            Name = $"Set{EntityType.NPC}",
            IsApplicable = e => e.Type == EntityType.NPC,
            Apply = e =>
            {
                e.Properties.Add(CreateEntityProperty<string>(e, "NPCType"));
                e.Properties.Add(CreateEntityProperty<string>(e, "Race"));
                e.Properties.Add(CreateEntityProperty<Class[]>(e, "Classes"));
                e.Properties.Add(CreateEntityProperty<string>(e, "Background"));
                e.Properties.Add(CreateEntityProperty<int>(e, "ChallengeRating"));
                e.Properties.Add(CreateEntityProperty<string[]>(e, "Actions"));
                e.Properties.Add(CreateEntityProperty<string[]>(e, "LegendaryActions"));
                e.Properties.Add(CreateEntityProperty<string[]>(e, "LairActions"));
                return e;
            },
            Validations = new List<Validation>
            {
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "ChallengeRating", cr => cr is < 0, "Invalid challenge rating."),
                CreatePropertyValidation<Class[]>(ValidationSeverityLevel.Error, "Classes", classes => (classes ?? Array.Empty<Class>()).All(@class => @class.Level > 0), "Invalid speed."),
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "Level", cr => cr is < 0, "Invalid level."),
            }
        });
        system.Modifiers.Add(new Modifier
        {
            GameSystem = system,
            Name = $"Set{EntityType.Character}",
            IsApplicable = e => e.Type == EntityType.Character,
            Apply = e =>
            {
                e.Properties.Add(CreateEntityProperty<string>(e, "Race"));
                e.Properties.Add(CreateEntityProperty<Class[]>(e, "Classes"));
                e.Properties.Add(CreateEntityProperty<string[]>(e, "Archetypes"));
                e.Properties.Add(CreateEntityProperty<string>(e, "Background"));
                e.Properties.Add(CreateEntityProperty<Dictionary<string, int>>(e, "AbilityCharges"));
                e.Properties.Add(CreateEntityProperty<string[]>(e, "Feats"));
                e.Properties.Add(CreateEntityProperty<Dictionary<string, int>>(e, "Currency"));
                e.Properties.Add(CreateEntityProperty<bool>(e, "Inspiration"));
                e.Properties.Add(CreateEntityProperty<int>(e, "ExhaustionLevel"));
                e.Properties.Add(CreateEntityProperty<int>(e, "CarryingCapacity"));
                e.Properties.Add(CreateEntityProperty<int>(e, "CurrentCarryingWeight"));
                e.Properties.Add(CreateEntityProperty<string>(e, "EyeColor"));
                e.Properties.Add(CreateEntityProperty<string>(e, "Gender"));
                e.Properties.Add(CreateEntityProperty<decimal>(e, "Height"));
                e.Properties.Add(CreateEntityProperty<decimal>(e, "Weight"));
                e.Properties.Add(CreateEntityProperty<string>(e, "HairColor"));
                e.Properties.Add(CreateEntityProperty<string>(e, "SkinColor"));
                e.Properties.Add(CreateEntityProperty<string>(e, "Appearance"));

                return e;
            },
            Validations = new List<Validation>
            {
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "ChallengeRating", cr => cr is < 0, "Invalid challenge rating."),
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "Level", cr => cr is < 0, "Invalid level."),
            }
        });
    }
}

