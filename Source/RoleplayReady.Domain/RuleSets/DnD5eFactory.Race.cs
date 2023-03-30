namespace RoleplayReady.Domain.RuleSets;

public static partial class DnD5eFactory {
    private static void SetRaceModifiers(RuleSet dnd5e) {
        var elf = dnd5e.Get<Aspect>("Elf");
        elf.ConfigureFeatures(features => {
            features.Add("Ability Score Increase", "[FeatureDescription]", modifiers => modifiers.Let("Dexterity").IncreaseBy(2))
                .Add("Age", "[FeatureDescription]",
                    modifiers => modifiers.CheckIf("Age").IsBetween(100, 750,
                        "Elves only mature at the age of 100 years and should live only up to 750 years. Please, consider changing your age."))
                .Add("Size", "[FeatureDescription]",
                    modifiers => modifiers.Let("Size").Be("Medium").And.CheckIf("Height").IsBetween(5m, 6m,
                        "Elves range from under 5 to over 6 feet tall. Please, consider changing your height."))
                .Add("Speed", "[FeatureDescription]", modifiers => modifiers.Let("Movements").Have("Walk", 30))
                .Add("Darkvision", "[FeatureDescription]", modifiers => modifiers.Let("Senses").Have("Darkvision"))
                .Add("Keen Senses", "[FeatureDescription]", modifiers => modifiers.Let("SkillChecks").Have("Perception", "Advantage"))
                .Add("Fey Ancestry", "[FeatureDescription]",
                    modifiers => modifiers.Let("Immunities").Have("Magical Sleep").And.Let("SavingThrows").Have("Charmed", "Advantage"))
                .Add("Trance", "[FeatureDescription]",
                    modifiers => modifiers.AddJournalEntry("Elves don't need to sleep. Instead, they meditate deeply for 4 hours a day."))
                .Add("Languages", "[FeatureDescription]",
                    modifiers => modifiers.Let("MaximumLanguagesKnown").Be(2).And.Let("Languages").Have("Common", "Elvish"));
        });

        var drow = dnd5e.Get<Aspect>("Drow (Dark Elf)").CloneFeaturesFrom(dnd5e.Get<Aspect>("Elf"), excluding: "Darkvision");
        drow.ConfigureFeatures(features => {
            features.Add("Drow Ability Score Increase", "[FeatureDescription]", modifiers => modifiers.Let("Charisma").IncreaseBy(1))
                .Add("Superior Darkvision", "[FeatureDescription]", modifiers => modifiers.Let("Senses").Have("Superior Darkvision"))
                .Add("Drow Weapon Training", "[FeatureDescription]", modifiers => modifiers.Let("Weapons").Have("Rapiers", "Shortswords", "Hand Crossbows"))
                .Add("Drow Magic", "[FeatureDescription]", modifiers => modifiers.Let("MagicSources").Have(e => {
                    var level = e.GetAttribute<int>("Level")?.Value;
                    if (level is null) return null;

                    var drowMagic = new MagicSource {
                        Name = "Drow Magic",
                        Type = "Innate",
                        SpellcastingAbility = "Charisma",
                        CantripsKnown = 1,
                        SpellsKnown = level >= 5 ? 2 : level >= 3 ? 1 : 0,
                        SpellList = { "Dancing Lights" },
                    };

                    if (level >= 3) {
                        drowMagic.SlotsPerLevel[1] = 1;
                        drowMagic.SpellList.Add("Faerie Fire");
                    }

                    if (level >= 5) {
                        drowMagic.SlotsPerLevel[2] = 1;
                        drowMagic.SpellList.Add("Darkness");
                    }

                    return drowMagic;
                }));
        });

        var woodElf = dnd5e.Get<Aspect>("Elf (Wood)").CloneFeaturesFrom(dnd5e.Get<Aspect>("Elf"), excluding: "Speed");
        woodElf.ConfigureFeatures(features => {
            features.Add("Drow Ability Score Increase", "[FeatureDescription]", modifiers => modifiers.Let("Wisdom").IncreaseBy(1));
            features.Add("Fleet of Foot", "[FeatureDescription]", modifiers => modifiers.Let("Movements").Have("Walk", 35));
            features.Add("Wood Weapon Training", "[FeatureDescription]", modifiers => modifiers.Let("Weapons").Have("Longswords", "Shortswords", "Shortbows", "Longbows"));
            features.Add("Mask of the Wild", "[FeatureDescription]",
                modifiers => modifiers.AddJournalEntry(
                    "You can attempt to hide even when you are only lightly obscured by foliage, heavy rain, falling snow, mist, and other natural phenomena."));
        });

        var highElf = dnd5e.Get<Aspect>("Elf (High)").CloneFeaturesFrom(dnd5e.Get<Aspect>("Elf"));
        highElf.ConfigureFeatures(features => {
            features.Add("High Elf Ability Score Increase", "[FeatureDescription]", modifiers => modifiers.Let("Intelligence").IncreaseBy(1))
                .Add("Cantrip", "[FeatureDescription]", modifiers => modifiers.Let("MagicSources").Have(e => {
                    var level = e.GetAttribute<int>("Level")?.Value;
                    if (level is null) return null;
                    return new MagicSource {
                        Name = "High Elf Magic",
                        Type = "Innate",
                        SpellcastingAbility = "Intelligence",
                        CantripsKnown = 1,
                        SpellList = { "Wizard" }
                    };
                }))
            .Add("Extra Language", "[FeatureDescription]", modifiers => modifiers.Let("MaximumLanguagesKnown").IncreaseBy(1))
            .Add("High Elf Weapon Training", "[FeatureDescription]", modifiers => modifiers.Let("Weapons").Have("Longswords", "Shortswords", "Shortbows", "Longbows"));
        });
    }
}