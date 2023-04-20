namespace RolePlayReady.Models;

public class GameSystemSettingTests {
    [Fact]
    public void Constructor_WithDateTime_InitializesProperties() {
        var dateTime = Substitute.For<IDateTime>();
        dateTime.Now.Returns(DateTime.Parse("2001-01-01 00:00:00"));

        var agent = new GameSystemSetting(dateTime) {
            Id = Guid.NewGuid(),
            Name = "TestName",
            Description = "TestDescription",
            AttributeDefinitions = new List<IAttributeDefinition>(),
        };

        agent.AttributeDefinitions.Should().BeEmpty();
    }

    [Fact]
    public void Constructor_CreatesInstance() {
        var agent = new GameSystemSetting {
            Id = Guid.NewGuid(),
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
            new EntityStringAttribute {
                Attribute = attributeDefinitions[0],
                Value = "TestValue",
            },
            new EntityNumberAttribute<int> {
                Attribute = attributeDefinitions[1],
                Value = 42,
            },
        };
        var testBase = new GameSystemSetting {
            Id = Guid.NewGuid(),
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
        var subject = GenerateTestGameSystemSetting();

        var result = subject.Validate();

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().HaveCount(2);
        result.Errors.First().Message.Should().Be("'AttributeDefinitions[0].Description' cannot be null.");
        result.Errors.Skip(1).First().Message.Should().Be("'AttributeDefinitions[1]' cannot be null.");
    }

    private static GameSystemSetting GenerateTestGameSystemSetting() {
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
            new EntityStringAttribute {
                Attribute = attributeDefinitions[0],
                Value = "TestValue",
            },
            new EntityNumberAttribute<int> {
                Attribute = null!,
                Value = 42,
            },
        };
        var testBase = new GameSystemSetting {
            Id = Guid.NewGuid(),
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