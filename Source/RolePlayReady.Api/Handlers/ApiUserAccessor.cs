using RolePlayReady.Security.Abstractions;

namespace RolePlayReady.Api.Handlers;

public class ApiUserAccessor : IUserAccessor {
    private readonly HttpContext _httpContext;

    public ApiUserAccessor(IHttpContextAccessor accessor) {
        _httpContext = Ensure.IsNotNull(accessor.HttpContext);
    }
    public string Id => _httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                     ?? throw new InvalidOperationException("User id not found.");
    public string Email => _httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
                        ?? throw new InvalidOperationException("User email not found.");
}