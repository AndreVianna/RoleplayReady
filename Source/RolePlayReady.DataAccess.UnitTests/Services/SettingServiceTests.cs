namespace RolePlayReady.DataAccess.Services;

public class SettingServiceTests {
    [Fact]
    public async Task LoadAsync_SettingExists_ReturnsSetting() {
        // Arrange
        var settingRepository = Substitute.For<ISettingRepository>();
        var settingService = new SettingService(settingRepository);

        var testSettingId = "SM";
        var expectedSetting = new Setting {
            ShortName = testSettingId,
            Name = "Some Name",
            AttributeDefinitions = Array.Empty<IAttributeDefinition>(),
            Description = "Some description."
        };

        settingRepository.GetByIdAsync(testSettingId, Arg.Any<CancellationToken>()).Returns(expectedSetting);

        // Act
        var result = await settingService.LoadAsync(testSettingId);

        // Assert
        result.Should().BeEquivalentTo(expectedSetting);
    }

    [Fact]
    public async Task LoadAsync_SettingDoesNotExist_ThrowsInvalidOperationException() {
        // Arrange
        var settingRepository = Substitute.For<ISettingRepository>();
        var settingService = new SettingService(settingRepository);

        var testSettingId = "nonexistent-rule-set-id";
        settingRepository.GetByIdAsync(testSettingId, Arg.Any<CancellationToken>()).Returns(default(Setting));

        // Act
        Func<Task> act = async () => await settingService.LoadAsync(testSettingId);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage($"Rule set for '{testSettingId}' was not found.");
    }
}