namespace RolePlayReady.DataAccess.Services;

public class RuleSetServiceTests {
    [Fact]
    public async Task LoadAsync_RuleSetExists_ReturnsRuleSet() {
        // Arrange
        var ruleSetRepository = Substitute.For<IRuleSetRepository>();
        var ruleSetService = new RuleSetService(ruleSetRepository);

        var testRuleSetId = "SM";
        var expectedRuleSet = new RuleSet {
            ShortName = testRuleSetId,
            Name = "Some Name",
            AttributeDefinitions = Array.Empty<IAttributeDefinition>(),
            Description = "Some description."
        };

        ruleSetRepository.GetByIdAsync(testRuleSetId, Arg.Any<CancellationToken>()).Returns(expectedRuleSet);

        // Act
        var result = await ruleSetService.LoadAsync(testRuleSetId);

        // Assert
        result.Should().BeEquivalentTo(expectedRuleSet);
    }

    [Fact]
    public async Task LoadAsync_RuleSetDoesNotExist_ThrowsInvalidOperationException() {
        // Arrange
        var ruleSetRepository = Substitute.For<IRuleSetRepository>();
        var ruleSetService = new RuleSetService(ruleSetRepository);

        var testRuleSetId = "nonexistent-rule-set-id";
        ruleSetRepository.GetByIdAsync(testRuleSetId, Arg.Any<CancellationToken>()).Returns(default(RuleSet));

        // Act
        Func<Task> act = async () => await ruleSetService.LoadAsync(testRuleSetId);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage($"Rule set for '{testRuleSetId}' was not found.");
    }
}