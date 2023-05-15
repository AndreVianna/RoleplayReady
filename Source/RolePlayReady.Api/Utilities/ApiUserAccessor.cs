namespace RolePlayReady.Api.Utilities;

[ExcludeFromCodeCoverage]
public class ApiUserAccessor : IUserAccessor {
    private readonly HttpContext _httpContext;

    public ApiUserAccessor(IHttpContextAccessor accessor) {
        _httpContext = Ensure.IsNotNull(accessor.HttpContext);
    }

    public string Id => _httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                     ?? throw new InvalidOperationException("User id not found.");
    public string Email => _httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
                        ?? throw new InvalidOperationException("User email not found.");
    public string BaseFolder => _httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value
                           ?? throw new InvalidOperationException("FolderName not found.");
    public string Name => _httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value
                       ?? throw new InvalidOperationException("User name not found.");

    public ICollection<Role> Roles
        => _httpContext.User.Claims
                       .Where(c => c.Type == ClaimTypes.Role)
                       .Select(c => Enum.TryParse<Role>(c.Value, true, out var role) ? role : (Role?)null)
                       .Where(c => c != null)
                       .Select(c => c!.Value)
                       .ToArray();
}