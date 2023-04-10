namespace RolePlayReady.Models;

public class EntityFlagAttributeTests {
    [Fact]
    public void Constructor_InitializesProperties() {
        var attribute = new AttributeDefinition {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(bool)
        };

        var entityFlagAttribute = new EntityFlagAttribute {
            AttributeDefinition = attribute,
            Value = true
        };

        entityFlagAttribute.AttributeDefinition.Should().Be(attribute);
        entityFlagAttribute.Value.Should().Be(true);
    }
}

public class EntitySimpleAttributeTests {
    [Fact]
    public void Constructor_InitializesProperties() {
        var attribute = new AttributeDefinition {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(int)
        };

        var entitySimpleAttribute = new EntitySimpleAttribute<int> {
            AttributeDefinition = attribute,
            Value = 42
        };

        ((IEntityAttribute)entitySimpleAttribute).Value.Should().Be(42);
        entitySimpleAttribute.AttributeDefinition.Should().Be(attribute);
        entitySimpleAttribute.Value.Should().Be(42);
    }
}

public class EntitySetAttributeTests {
    [Fact]
    public void Constructor_InitializesProperties() {
        var attribute = new AttributeDefinition {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(HashSet<int>)
        };

        var entitySetAttribute = new EntitySetAttribute<int> {
            AttributeDefinition = attribute,
            Value = new HashSet<int> { 1, 2, 3 }
        };

        entitySetAttribute.AttributeDefinition.Should().Be(attribute);
        entitySetAttribute.Value.Should().BeEquivalentTo(new HashSet<int> { 1, 2, 3 });
    }
}

public class EntityListAttributeTests {
    [Fact]
    public void Constructor_InitializesProperties() {
        var attribute = new AttributeDefinition {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(List<int>)
        };

        var entityListAttribute = new EntityListAttribute<int> {
            AttributeDefinition = attribute,
            Value = new List<int> { 1, 2, 3 }
        };

        entityListAttribute.AttributeDefinition.Should().Be(attribute);
        entityListAttribute.Value.Should().BeEquivalentTo(new List<int> { 1, 2, 3 });
    }
}

public class EntityDictionaryAttributeTests {
    [Fact]
    public void Constructor_InitializesProperties() {
        var attribute = new AttributeDefinition {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(Dictionary<string, int>)
        };

        var entityDictionaryAttribute = new EntityDictionaryAttribute<string, int> {
            AttributeDefinition = attribute,
            Value = new Dictionary<string, int> { { "one", 1 }, { "two", 2 }, { "three", 3 } }
        };

        entityDictionaryAttribute.AttributeDefinition.Should().Be(attribute);
        entityDictionaryAttribute.Value.Should().BeEquivalentTo(
            new Dictionary<string, int> { { "one", 1 }, { "two", 2 }, { "three", 3 } }
        );
    }
}
