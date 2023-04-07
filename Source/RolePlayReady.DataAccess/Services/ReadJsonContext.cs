namespace RolePlayReady.DataAccess.Services;

public class ReadJsonContext<TResult> : Context {
    public ReadJsonContext(IServiceProvider services, string fileName)
        : base(services) {
        FileName = fileName;
    }

    public string FileName { get; }
    public TResult Result { get; set; } = default!;
}