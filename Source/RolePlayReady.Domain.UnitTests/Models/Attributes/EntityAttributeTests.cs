namespace RolePlayReady.Models.Attributes;

public class EntityFlagAttributeTests {
    [Fact]
    public void Constructor_InitializesProperties() {
        var attribute = new AttributeDefinition {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(bool)
        };

        var entityFlagAttribute = new EntityFlagAttribute {
            Attribute = attribute,
            Value = true
        };

        entityFlagAttribute.Attribute.Should().Be(attribute);
        entityFlagAttribute.Value.Should().Be(true);
    }
}

//public class EntitySimpleAttributeTests {
//    [Fact]
//    public void Constructor_InitializesProperties() {
//        var attribute = new AttributeDefinition {
//            Name = "TestName",
//            Description = "TestDescription",
//            DataType = typeof(int),
//        };
//        attribute.Constraints.Add(new GreaterThan(10));

//        var entitySimpleAttribute = new EntityIntegerAttribute {
//            Attribute = attribute,
//            Value = 42,
//        };

//        ((IEntityAttribute)entitySimpleAttribute).Value.Should().Be(42);
//        entitySimpleAttribute.Attribute.Should().Be(attribute);
//        entitySimpleAttribute.Value.Should().Be(42);
//        entitySimpleAttribute.IsValid.Should().BeTrue();
//    }
//}

public class EntitySetAttributeTests {
    [Fact]
    public void Constructor_InitializesProperties() {
        var attribute = new AttributeDefinition {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(HashSet<int>)
        };

        var entitySetAttribute = new EntitySetAttribute<int> {
            Attribute = attribute,
            Value = new() { 1, 2, 3 }
        };

        entitySetAttribute.Attribute.Should().Be(attribute);
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
            Attribute = attribute,
            Value = new() { 1, 2, 3 }
        };

        entityListAttribute.Attribute.Should().Be(attribute);
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
            Attribute = attribute,
            Value = new() { { "one", 1 }, { "two", 2 }, { "three", 3 } }
        };

        entityDictionaryAttribute.Attribute.Should().Be(attribute);
        entityDictionaryAttribute.Value.Should().BeEquivalentTo(
            new Dictionary<string, int> { { "one", 1 }, { "two", 2 }, { "three", 3 } }
        );
    }
}
