using Attribute = RoleplayReady.Domain.Models.Attribute;

namespace RoleplayReady.Domain.RuleSets;

// ReSharper disable StringLiteralTypo
// ReSharper disable once InconsistentNaming - DnD5e is the official name of the system.
public static partial class DnD5eFactory {

    public static RuleSet Create() {
        // ReSharper disable once InconsistentNaming - DnD5e is the official name of the system.
        var dnd5e = new RuleSet {
            OwnerId = "System",
            Name = "Dungeons & Dragons 5th Edition",
            Abbreviation = "DnD5e",
            Description = "Dungeons & Dragons 5th Edition is a tabletop role-playing game system.",
        };

        var sources = new[] { new Source {
                Abbreviation = "PHB",
                Name = "Player's Handbook",
                Description = "The Player's Handbook is the essential reference for every Dungeons & Dragons roleplayer. It contains rules for character creation and advancement, backgrounds and skills, exploration and combat, equipment, spells, and much more.",
                Publisher = "Wizards of the Coast",
            }
        };
        foreach (var source in sources) {
            dnd5e.Sources.Add(source);
        }

        var sections = new (string Type, string Name, string Description)[]
        {
            (nameof(Actor), "Character", "[Add description here]"),
            (nameof(Actor), "Non-Player Character", "[Add description here]"),
            (nameof(Actor), "Creature", "[Add description here]"),
            (nameof(Component), "Race", "Represents the character's race in Dungeons & Dragons 5th Edition."),
            (nameof(Component), "Class", "Represents the character's class, which determines their abilities and progression."),
            (nameof(Component), "Subclass", "Represents a specialization within a class, granting additional abilities and options."),
            (nameof(Component), "Background", "Represents the character's backstory and personal history, which can provide additional proficiencies and roleplaying hooks."),
            (nameof(Component), "Feat", "Represents special abilities and talents that a character can acquire, often providing unique options or enhancements."),
            (nameof(Power), "Spell", "Represents magical abilities and effects that characters can learn and cast, with various effects and power levels."),
            (nameof(Power), "Cantrip", "[Add description here]"),
            (nameof(Power), "Equipment", "Represents items, weapons, armor, and other gear that characters can acquire and use throughout their adventures."),
        };
        foreach (var (type, name, description) in sections) {
            dnd5e.Elements.Add(ElementFactory.For(dnd5e, "System").Create(type, name, description));
        }

        var attributes = new (Type Type, string Name, string Description)[] {
            (typeof(bool), "Inspiration", "[Add description here]"),
            (typeof(decimal), "Height", "[Add description here]"),
            (typeof(decimal), "Weight", "[Add description here]"),
            (typeof(Dictionary<string, int>), "Classes", "[Add description here]"),
            (typeof(Dictionary<string, int>), "Currency", "[Add description here]"),
            (typeof(Dictionary<string, int>), "Moviments", "[Add description here]"),
            (typeof(Dictionary<string, int>), "AbilityCharges", "[Add description here]"),
            (typeof(Dictionary<string, string>), "SavingThrows", "[Add description here]"),
            (typeof(Dictionary<string, string>), "SkillChecks", "[Add description here]"),
            (typeof(int), "ArmorClass", "[Add description here]"),
            (typeof(int), "CarryingCapacity", "[Add description here]"),
            (typeof(int), "ChallengeRating", "[Add description here]"),
            (typeof(int), "Charisma", "[Add description here]"),
            (typeof(int), "Constitution", "[Add description here]"),
            (typeof(int), "CurrentCarryingWeight", "[Add description here]"),
            (typeof(int), "CurrentHitPoints", "[Add description here]"),
            (typeof(int), "Dexterity", "[Add description here]"),
            (typeof(int), "ExhaustionLevel", "[Add description here]"),
            (typeof(int), "Intelligence", "[Add description here]"),
            (typeof(int), "MaximumLanguagesKnown", "[Add description here]"),
            (typeof(int), "MaximumHitPoints", "[Add description here]"),
            (typeof(int), "ProficiencyBonus", "[Add description here]"),
            (typeof(int), "Strength", "[Add description here]"),
            (typeof(int), "TemporaryHitPoints", "[Add description here]"),
            (typeof(int), "Wisdom", "[Add description here]"),
            (typeof(string), "Alignment", "[Add description here]"),
            (typeof(string), "Appearance", "[Add description here]"),
            (typeof(string), "Background", "[Add description here]"),
            (typeof(string), "CreatureType", "[Add description here]"),
            (typeof(string), "EyeColor", "[Add description here]"),
            (typeof(string), "Gender", "[Add description here]"),
            (typeof(string), "HairColor", "[Add description here]"),
            (typeof(string), "NPCType", "[Add description here]"),
            (typeof(string), "PassiveInsight", "[Add description here]"),
            (typeof(string), "PassivePerception", "[Add description here]"),
            (typeof(string), "Race", "[Add description here]"),
            (typeof(string), "Size", "[Add description here]"),
            (typeof(string), "SkinColor", "[Add description here]"),
            (typeof(string), "SpellCastingAbility", "[Add description here]"),
            (typeof(HashSet<string>), "Actions", "[Add description here]"),
            (typeof(HashSet<string>), "Archetypes", "[Add description here]"),
            (typeof(HashSet<string>), "Armors", "[Add description here]"),
            (typeof(HashSet<string>), "Feats", "[Add description here]"),
            (typeof(HashSet<string>), "Immunities", "[Add description here]"),
            (typeof(HashSet<string>), "LairActions", "[Add description here]"),
            (typeof(HashSet<string>), "Languages", "[Add description here]"),
            (typeof(HashSet<string>), "LegendaryActions", "[Add description here]"),
            (typeof(HashSet<string>), "Resistances", "[Add description here]"),
            (typeof(HashSet<string>), "Senses", "[Add description here]"),
            (typeof(HashSet<string>), "Traits", "[Add description here]"),
            (typeof(HashSet<string>), "Tools", "[Add description here]"),
            (typeof(HashSet<string>), "Vulnerabilities", "[Add description here]"),
            (typeof(HashSet<string>), "Weapons", "[Add description here]"),
        };
        foreach (var attribute in attributes) {
            dnd5e.Attributes.Add(new Attribute {
                Parent = dnd5e,
                RuleSet = dnd5e,
                OwnerId = "System",
                Name = attribute.Name,
                DataType = attribute.Type,
                Description = attribute.Description,
            });
        }

        var raceSection = dnd5e.GetElement("Race");
        var playersHandbook = dnd5e.GetSource("PHB");
        var races = new (string Name, Usage Usage, string Description)[]
        {
            ("Dragonborn", Usage.Standard, "Dragonborn are proud and self-assured, drawing upon their draconic ancestry for strength and power."),
            ("Dwarf", Usage.Template, "Dwarves are a stout and hardy folk, known for their resilience and craftsmanship."),
            ("Dwarf (Hill)", Usage.Standard, "Hill Dwarves are known for their toughness and resistance, as well as their deep connection to the earth."),
            ("Dwarf (Mountain)", Usage.Standard,
                "Mountain Dwarves are strong and skilled warriors, at home in the mountains and proficient in the use of armor."),
            ("Elf", Usage.Template, "Elves are a graceful and long-lived race, with a deep connection to the natural world."),
            ("Elf (High)", Usage.Standard, "High Elves are skilled in magic and intellect, with an affinity for the arcane arts."),
            ("Elf (Wood)", Usage.Standard, "Wood Elves are nimble and stealthy, living in harmony with the forests and excelling at archery."),
            ("Elf (Eladrin)", Usage.Standard, "Eladrin are attuned to the Feywild and can manifest the power of the seasons through their emotions."),
            ("Drow (Dark Elf)", Usage.Standard, "Drow, or Dark Elves, are known for their cunning and exceptional skill in the use of magic and stealth."),
            ("Gnome", Usage.Template, "Gnomes are small, curious, and inventive beings, with an innate affinity for illusion and enchantment."),
            ("Gnome (Forest)", Usage.Standard, "Forest Gnomes are stealthy and in tune with nature, having a natural ability to speak with small animals."),
            ("Gnome (Rock)", Usage.Standard, "Rock Gnomes are skilled in the crafting of machines and mechanical devices, with a keen interest in technology."),
            ("Half-Elf", Usage.Standard, "Half-Elves combine the best of both human and elven traits, exhibiting versatility and adaptability."),
            ("Half-Elf (Variant)", Usage.Standard,
                "Half-Elf Variants display traits from their non-human parent's lineage, granting them additional abilities."),
            ("Half-Orc", Usage.Standard, "Half-Orcs are strong and resilient, possessing a fierce determination and indomitable spirit."),
            ("Halfling", Usage.Template, "Halflings are small, nimble, and good-natured beings, known for their luck and resourcefulness."),
            ("Halfling (Lightfoot)", Usage.Standard, "Lightfoot Halflings are stealthy and elusive, easily able to blend into their surroundings."),
            ("Halfling (Stout)", Usage.Standard, "Stout Halflings are hardy and resistant to poison, with a strong connection to their dwarven ancestry."),
            ("Human", Usage.Standard, "Humans are a versatile and adaptable race, capable of excelling in a wide variety of roles."),
            ("Human (Variant)", Usage.Standard, "Variant Humans possess unique talents and abilities, demonstrating a diverse range of skills."),
            ("Tiefling", Usage.Standard,
                "Tieflings bear the mark of an infernal heritage, granting them dark powers and a connection to the planes of Hell."),
        };
        foreach (var race in races) {
            var element = (Element)ElementFactory.For(raceSection, "System").Create(nameof(Component), race.Name, race.Description) with {
                Usage = race.Usage,
                Source = playersHandbook,
                Status = Status.Public,
            };
            dnd5e.Elements.Add(element);
        }

        SetRaceModifiers(dnd5e);

        return dnd5e;
    }
}