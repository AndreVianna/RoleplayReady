namespace RolePlayReady.Handlers.Setting;

public record SettingRow : Row {
    public required string Name { get; init; }
}