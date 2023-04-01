using static RoleplayReady.Domain.Models.EntrySection;

namespace RoleplayReady.Domain.RuleSets;

// ReSharper disable once InconsistentNaming - DnD5e is the official name of the system.
public static partial class DnD5eFactory {
    // ReSharper disable once InconsistentNaming - DnD5e is the official name of the system.
    private static void SetRaceModifiers(IRuleSet dnd5e) {
        var elf = dnd5e.GetComponent("Elf");
        elf.Configure(nameof(Element.Traits)).As(traits => traits
            .Add("Ability Score Increase", "[FeatureDescription]", x => x.Let("Dexterity").IncreaseBy(2))
            .Add("Age", "[FeatureDescription]", x => x.CheckIf("Age").IsBetween(100, 750, "Elves only mature at the age of 100 years and should live only up to 750 years. Please, consider changing your age."))
            .Add("Size", "[FeatureDescription]", x => x.Let("Size").Be("Medium").And.CheckIf("Height").IsBetween(5m, 6m, "Elves range from under 5 to over 6 feet tall. Please, consider changing your height."))
            .Add("Speed", "[FeatureDescription]", x => x.Let("Movements").Have("Walk", 30))
            .Add("Darkvision", "[FeatureDescription]", x => x.Let("Senses").Have("Darkvision"))
            .Add("Keen Senses", "[FeatureDescription]", x => x.Let("SkillChecks").Have("Perception", "Advantage"))
            .Add("Fey Ancestry", "[FeatureDescription]", x => x.Let("Immunities").Have("Magical Sleep").And.Let("SavingThrows").Have("Charmed", "Advantage"))
            .Add("Trance", "[FeatureDescription]", x => x.AddJournalEntry(Traits, "Elves don't need to sleep. Instead, they meditate deeply for 4 hours a day."))
            .Add("Languages", "[FeatureDescription]", x => x.Let("MaximumLanguagesKnown").Be(2).And.Let("Languages").Have("Common", "Elvish")));

        var drow = dnd5e.GetComponent("Drow (Dark Elf)").CopyTraitsFrom(elf, excluding: "Darkvision");
        drow.Configure(nameof(Element.Traits)).As(traits => traits
            .Replace("Darkvision").With("Superior Darkvision", "[FeatureDescription]", x => x.Let("Senses").Have("Superior Darkvision"))
            .And.Add("Drow Ability Score Increase", "[FeatureDescription]", x => x.Let("Charisma").IncreaseBy(1))
            .And.Add("Drow Weapon Training", "[FeatureDescription]", x => x.Let("Weapons").Have("Rapiers", "Shortswords", "Hand Crossbows"))
            .And.Add("Drow Magic", "[FeatureDescription]", x => x.AddPowerSource("Drow Innate Magic", "[Add description here]", (e, b) => {
                var level = e.GetAttribute<int>("Level").Value;
                var spellsKnown = level >= 5 ? 2 : level >= 3 ? 1 : 0;
                b.AddTag("Innate")
                    .And.Let("SpellCastingAbility").Be("Charisma")
                    .And.Let("CantripsKnown").Be(1)
                    .And.Let("SpellsKnown").Be(spellsKnown);
                if (level >= 3) {
                    b.Let("SlotsPerLevel").Have(1, 1)
                        .And.Let("SpellList").Have("Faerie Fire");
                }
                if (level >= 5) {
                    b.Let("SlotsPerLevel").Have(2, 1)
                        .And.Let("SpellList").Have("Faerie Fire");
                }
            })));

        var woodElf = dnd5e.GetComponent("Elf (Wood)").CopyTraitsFrom(elf, excluding: "Speed");
        woodElf.Configure(nameof(Element.Traits)).As(traits => traits
            .Add("Drow Ability Score Increase", "[FeatureDescription]", x => x.Let("Wisdom").IncreaseBy(1))
            .Add("Fleet of Foot", "[FeatureDescription]", x => x.Let("Movements").Have("Walk", 35))
            .Add("Wood Weapon Training", "[FeatureDescription]", x => x.Let("Weapons").Have("Longswords", "Shortswords", "Shortbows", "Longbows"))
            .Add("Mask of the Wild", "[FeatureDescription]", x => x.AddJournalEntry(Traits, "You can attempt to hide even when you are only lightly obscured by foliage, heavy rain, falling snow, mist, and other natural phenomena.")));

        var highElf = dnd5e.GetComponent("Elf (High)").CopyTraitsFrom(elf);
        highElf.Configure(nameof(Element.Traits)).As(traits => traits
            .Add("High Elf Ability Score Increase", "[FeatureDescription]", x => x.Let("Intelligence").IncreaseBy(1))
            .Add("Extra Language", "[FeatureDescription]", x => x.Let("MaximumLanguagesKnown").IncreaseBy(1))
            .Add("High Elf Weapon Training", "[FeatureDescription]", x => x.Let("Weapons").Have("Longswords", "Shortswords", "Shortbows", "Longbows"))
            .Add("Cantrip", "[FeatureDescription]", x => x.AddPowerSource("High Elf Innate Magic", "[Add description here]", b => b
                .AddTag("Innate")
                .And.Let("SpellCastingAbility").Be("Intelligence")
                .And.Let("CantripsKnown").Be(1)
                .And.Let("SpellList").Have("Wizard")
            )));
    }
}