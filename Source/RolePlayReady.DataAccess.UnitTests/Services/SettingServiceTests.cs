using RolePlayReady.Repositories;

using GameSetting = RolePlayReady.Models.GameSetting;

namespace RolePlayReady.DataAccess.Services;

public class SettingServiceTests {
    private readonly SettingService _settingService;
    private readonly IGameSettingsRepository _settingRepository;
    private const string _owner = "System";
    private const string _dataFileName = "SM";

    public SettingServiceTests() {
        _settingRepository = Substitute.For<IGameSettingsRepository>();
        var userAccessor = Substitute.For<IUserAccessor>();
        userAccessor.Id.Returns(_owner);
        _settingService = new SettingService(_settingRepository, userAccessor);
    }

    [Fact]
    public async Task LoadAsync_SettingExists_ReturnsSetting() {
        // Arrange
        var expectedSetting = new GameSetting {
            ShortName = "SM",
            Name = "Some Name",
            AttributeDefinitions = Array.Empty<IAttribute>(),
            Description = "Some description."
        };

        _settingRepository.GetByIdAsync(_owner, _dataFileName, Arg.Any<CancellationToken>()).Returns(expectedSetting);

        // Act
        var result = await _settingService.LoadAsync(_dataFileName);

        // Assert
        result.Should().BeEquivalentTo(expectedSetting);
    }

    [Fact]
    public async Task LoadAsync_SettingDoesNotExist_ThrowsInvalidOperationException() {
        // Arrange
        _settingRepository.GetByIdAsync(_owner, _dataFileName, Arg.Any<CancellationToken>()).Returns(default(GameSetting));

        // Act
        Func<Task> act = async () => await _settingService.LoadAsync(_dataFileName);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage($"Game setting '{_dataFileName}' was not found.");
    }
}