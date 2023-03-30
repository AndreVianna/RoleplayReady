using Attribute = RoleplayReady.Domain.Models.Attribute;

namespace RoleplayReady.Domain.RuleSets;

// ReSharper disable once InconsistentNaming - DnD5e is the official name of the system.
public static partial class DnD5eFactory {

    public static RuleSet Create() {
        // ReSharper disable once InconsistentNaming - DnD5e is the official name of the system.
        var dnd5e = new RuleSet {
            OwnerId = "System",
            Name = "Dungeons & Dragons 5th Edition",
            Abbreviation = "DnD5e",
            Description = "Dungeons & Dragons 5th Edition is a tabletop role-playing game system.",
            Publisher = "Wizards of the Coast",
        };

        var elementTypes = new (string Name, string Description)[]
        {
            // ReSharper disable StringLiteralTypo
            ("Character", "[Pending]"),
            ("Non-Player Character", "[Pending]"),
            ("Creature", "[Pending]"),
            ("Race", "Represents the character's race in Dungeons & Dragons 5th Edition."),
            ("Class", "Represents the character's class, which determines their abilities and progression."),
            ("Subclass", "Represents a specialization within a class, granting additional abilities and options."),
            ("Background", "Represents the character's backstory and personal history, which can provide additional proficiencies and roleplaying hooks."),
            ("Feat", "Represents special abilities and talents that a character can acquire, often providing unique options or enhancements."),
            ("Spell", "Represents magical abilities and effects that characters can learn and cast, with various effects and power levels."),
            ("Equipment", "Represents items, weapons, armor, and other gear that characters can acquire and use throughout their adventures."),
            // ReSharper restore StringLiteralTypo
        };
        foreach (var elementType in elementTypes) {
            dnd5e.ElementTypes.Add(new() {
                RuleSet = dnd5e,
                Name = elementType.Name,
                Description = elementType.Description,
            });
        }

        var sources = new (string Name, string Abbreviation, string Description, string Publisher)[]
        {
            // ReSharper disable StringLiteralTypo
            ("Player's Handbook", "PHB",
                "The Player's Handbook is the essential reference for every Dungeons & Dragons roleplayer. It contains rules for character creation and advancement, backgrounds and skills, exploration and combat, equipment, spells, and much more.",
                "Wizards of the Coast"),
            // ReSharper restore StringLiteralTypo
        };
        foreach (var source in sources) {
            dnd5e.Sources.Add(new() {
                OwnerId = "System",
                RuleSet = dnd5e,
                Name = source.Name,
                Abbreviation = source.Abbreviation,
                Description = source.Description,
                Publisher = source.Publisher,
            });
        }

        var attributes = new (Type Type, string Name, string Description)[]
        {
            // ReSharper disable StringLiteralTypo
            (typeof(bool), "Inspiration", ""),
            (typeof(decimal), "Height", ""),
            (typeof(decimal), "Weight", ""),
            (typeof(Dictionary<string, int>), "Classes", ""),
            (typeof(Dictionary<string, int>), "Currency", ""),
            (typeof(Dictionary<string, int>), "Moviments", ""),
            (typeof(Dictionary<string, MagicSource>), "MagicSources", ""),
            (typeof(Dictionary<string, int>), "AbilityCharges", ""),
            (typeof(Dictionary<string, string>), "SavingThrows", ""),
            (typeof(Dictionary<string, string>), "SkillChecks", ""),
            (typeof(int), "ArmorClass", ""),
            (typeof(int), "CarryingCapacity", ""),
            (typeof(int), "ChallengeRating", ""),
            (typeof(int), "Charisma", ""),
            (typeof(int), "Constitution", ""),
            (typeof(int), "CurrentCarryingWeight", ""),
            (typeof(int), "CurrentHitPoints", ""),
            (typeof(int), "Dexterity", ""),
            (typeof(int), "ExhaustionLevel", ""),
            (typeof(int), "Intelligence", ""),
            (typeof(int), "MaximumLanguagesKnown", ""),
            (typeof(int), "MaximumHitPoints", ""),
            (typeof(int), "ProficiencyBonus", ""),
            (typeof(int), "Strength", ""),
            (typeof(int), "TemporaryHitPoints", ""),
            (typeof(int), "Wisdom", ""),
            (typeof(string), "Alignment", ""),
            (typeof(string), "Appearance", ""),
            (typeof(string), "Background", ""),
            (typeof(string), "CreatureType", ""),
            (typeof(string), "EyeColor", ""),
            (typeof(string), "Gender", ""),
            (typeof(string), "HairColor", ""),
            (typeof(string), "NPCType", ""),
            (typeof(string), "PassiveInsight", ""),
            (typeof(string), "PassivePerception", ""),
            (typeof(string), "Race", ""),
            (typeof(string), "Size", ""),
            (typeof(string), "SkinColor", ""),
            (typeof(HashSet<string>), "Actions", ""),
            (typeof(HashSet<string>), "Archetypes", ""),
            (typeof(HashSet<string>), "Armors", ""),
            (typeof(HashSet<string>), "Feats", ""),
            (typeof(HashSet<string>), "Immunities", ""),
            (typeof(HashSet<string>), "LairActions", ""),
            (typeof(HashSet<string>), "Languages", ""),
            (typeof(HashSet<string>), "LegendaryActions", ""),
            (typeof(HashSet<string>), "Resistances", ""),
            (typeof(HashSet<string>), "Senses", ""),
            (typeof(HashSet<string>), "Traits", ""),
            (typeof(HashSet<string>), "Tools", ""),
            (typeof(HashSet<string>), "Vulnerabilities", ""),
            (typeof(HashSet<string>), "Weapons", ""),
            // ReSharper restore StringLiteralTypo
    };
        foreach (var attribute in attributes) {
            dnd5e.Attributes.Add(new Attribute(attribute.Type) {
                RuleSet = dnd5e,
                Name = attribute.Name,
                Description = attribute.Description,
            });
        }

        var raceElementType = dnd5e.ElementTypes.First(et => et.Name == "Race");
        var playersHandbook = dnd5e.Sources.First(et => et.Abbreviation == "PHB");
        var races = new (string Name, Usage Usage, string Description)[]
        {
            // ReSharper disable StringLiteralTypo
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
                "Tieflings bear the mark of an infernal heritage, granting them dark powers and a connection to the planes of Hell."), // ReSharper restore StringLiteralTypo
            // ReSharper restore StringLiteralTypo
        };
        foreach (var race in races) {
            dnd5e.Elements.Add(new Element {
                OwnerId = "System",
                RuleSet = dnd5e,
                Type = raceElementType,
                Name = race.Name,
                Description = race.Description,
                Usage = race.Usage,
                Source = playersHandbook,
                Status = Status.Public,
            });
        }

        SetRaceModifiers(dnd5e);

        return dnd5e;
    }

    // ReSharper disable once InconsistentNaming - DnD5e is the official name of the system.
}