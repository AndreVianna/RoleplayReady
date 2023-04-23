namespace RolePlayReady.Models;

public class DomainTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var agent = new Domain {
            Name = "TestName",
            Description = "TestDescription"
        };

        agent.Should().NotBeNull();
    }

    [Fact]
    public void Validate_Validates() {
        var attributeDefinitions = new List<IAttributeDefinition> {
            new AttributeDefinition {
                Name = "TestAttribute1",
                Description = "TestDescription1",
                ShortName = "TA1",
                DataType = typeof(string)
            },
            new AttributeDefinition {
                Name = "TestAttribute2",
                Description = "TestDescription2",
                ShortName = "TA2",
                DataType = typeof(int)
            },
        };
        var attributes = new List<IEntityAttribute> {
            new EntityTextAttribute {
                Attribute = attributeDefinitions[0],
                Value = "TestValue",
            },
            new EntityNumberAttribute<int> {
                Attribute = attributeDefinitions[1],
                Value = 42,
            },
        };
        var testBase = new Domain {
            Name = "TestName",
            Description = "TestDescription",
            ShortName = "GSS",
            Tags = new[] { "Tag1", "Tag2" },
            AttributeDefinitions = attributeDefinitions,
            Attributes = attributes,
        };

        var result = testBase.Validate();

        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact]
    public void Validate_WithErrors_Validates() {
        var subject = GenerateTestDomain();

        var result = subject.Validate();

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().HaveCount(2);
        result.Errors.First().Message.Should().Be("'AttributeDefinitions[0].Description' cannot be null.");
        result.Errors.Skip(1).First().Message.Should().Be("'AttributeDefinitions[1]' cannot be null.");
    }

    private static Domain GenerateTestDomain() {
        var attributeDefinitions = new List<IAttributeDefinition> {
            new AttributeDefinition {
                Name = "TestAttribute1",
                Description = null!,
                ShortName = "TA1",
                DataType = typeof(string)
            },
            null!,
        };
        var attributes = new List<IEntityAttribute> {
            new EntityTextAttribute {
                Attribute = attributeDefinitions[0],
                Value = "TestValue",
            },
            new EntityNumberAttribute<int> {
                Attribute = null!,
                Value = 42,
            },
        };
        var testBase = new Domain {
            Name = "TestName",
            Description = "TestDescription",
            ShortName = "GSS",
            Tags = new[] { "Tag1", "Tag2" },
            AttributeDefinitions = attributeDefinitions,
            Attributes = attributes,
        };
        return testBase;
    }
}