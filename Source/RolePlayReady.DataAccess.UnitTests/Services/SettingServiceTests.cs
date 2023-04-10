using RolePlayReady.Models.Abstractions;

using static RolePlayReady.Constants.Common;

namespace RolePlayReady.DataAccess.Services;

public class SettingServiceTests {
    private readonly SettingService _settingService;
    private readonly IGameSettingsRepository _settingRepository;
    private const string _dataFileName = "SM";

    public SettingServiceTests() {
        _settingRepository = Substitute.For<IGameSettingsRepository>();
        var userAccessor = Substitute.For<IUserAccessor>();
        userAccessor.Id.Returns(InternalUser);
        _settingService = new SettingService(_settingRepository, userAccessor);
    }

    [Fact]
    public async Task LoadAsync_SettingExists_ReturnsSetting() {
        // Arrange
        var expectedSetting = new GameSystemSetting {
            ShortName = "SM",
            Name = "Some Name",
            AttributeDefinitions = Array.Empty<IAttributeDefinition>(),
            Description = "Some description."
        };

        _settingRepository.GetByIdAsync(InternalUser, _dataFileName, Arg.Any<CancellationToken>()).Returns(expectedSetting);

        // Act
        var result = await _settingService.LoadAsync(_dataFileName);

        // Assert
        result.Should().BeEquivalentTo(expectedSetting);
    }

    [Fact]
    public async Task LoadAsync_SettingDoesNotExist_ThrowsInvalidOperationException() {
        // Arrange
        _settingRepository.GetByIdAsync(InternalUser, _dataFileName, Arg.Any<CancellationToken>()).Returns(default(GameSystemSetting));

        // Act
        Func<Task> act = async () => await _settingService.LoadAsync(_dataFileName);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage($"GameSystem setting '{_dataFileName}' was not found.");
    }
}