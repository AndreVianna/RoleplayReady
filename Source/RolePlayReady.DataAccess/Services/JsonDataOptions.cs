namespace RolePlayReady.DataAccess.Services;

public class JsonDataOptions : RunnerOptions<JsonDataOptions> {
    public required string DataRootFolder { get; set; }
}