using static RolePlayReady.Constants.Common;

namespace RolePlayReady.DataAccess.Services;

public class GameSettingServiceTests {
    private readonly SettingService _settingService;
    private readonly IGameSystemSettingsRepository _systemSettingRepository;
    private const string _dataFileName = "SM";

    public GameSettingServiceTests() {
        _systemSettingRepository = Substitute.For<IGameSystemSettingsRepository>();
        var userAccessor = Substitute.For<IUserAccessor>();
        userAccessor.Id.Returns(InternalUser);
        _settingService = new SettingService(_systemSettingRepository, userAccessor);
    }

    [Fact]
    public async Task LoadAsync_SettingExists_ReturnsSetting() {
        // Arrange
        var id = Guid.NewGuid();
        var expectedSetting = new GameSystemSetting {
            Id = id,
            ShortName = "SM",
            Name = "Some Name",
            AttributeDefinitions = Array.Empty<AttributeDefinition>(),
            Description = "Some description."
        };

        _systemSettingRepository.GetByIdAsync(InternalUser, id, Arg.Any<CancellationToken>()).Returns(expectedSetting);

        // Act
        var result = await _settingService.LoadAsync(id);

        // Assert
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public async Task LoadAsync_SettingDoesNotExist_ThrowsInvalidOperationException() {
        // Arrange
        var id = Guid.NewGuid();
        _systemSettingRepository.GetByIdAsync(InternalUser, id, Arg.Any<CancellationToken>()).Returns(default(GameSystemSetting));

        // Act
        var result = await _settingService.LoadAsync(id);

        // Assert
        result.Exception.Should().NotBeNull();
    }
}