using static System.Security.Claims.ClaimTypes;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace RolePlayReady.Api.Utilities;

[ExcludeFromCodeCoverage]
internal class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions> {
    private readonly ILogger<ApiKeyAuthenticationHandler> _logger;
    private readonly byte[] _issuerSigningKey;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private const string _authHeader = "Authorization";
    private const string _tokenPrefix = "Bearer ";

    public ApiKeyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, IConfiguration configuration, ILoggerFactory loggerFactory, UrlEncoder encoder, ISystemClock clock)
        : base(options, loggerFactory, encoder, clock) {
        _logger = loggerFactory.CreateLogger<ApiKeyAuthenticationHandler>();
        _issuerSigningKey = Encoding.ASCII.GetBytes(Ensure.IsNotNullOrWhiteSpace(configuration["Security:IssuerSigningKey"], "Configuration[Security:IssuerSigningKey]"));
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync() {
        try {
            if (!RequiresAuth()) return Task.FromResult(NoResult());

            if (!Request.Headers.TryGetValue(_authHeader, out var authorizationHeader)) {
                _logger.LogDebug("Request missing '{header}' header.", _authHeader);
                return Task.FromResult(Fail($"Request missing '{_authHeader}' header."));
            }

            var token = authorizationHeader.FirstOrDefault();
            if (token?.StartsWith(_tokenPrefix) != true) {
                _logger.LogDebug("'{tokenType}' token is missing from {header} header.", _tokenPrefix.Trim(), _authHeader);
                return Task.FromResult(Fail($"'{_tokenPrefix.Trim()}' token is missing from '{_authHeader}' header."));
            }

            var claims = GetClaims(token);
            var nameIdentifier = claims.FindFirst(NameIdentifier)?.Value;
            if (nameIdentifier is null) {
                _logger.LogDebug("'{claim}' claim is missing.", NameIdentifier);
                return Task.FromResult(Fail($"'{NameIdentifier}' claim is missing."));
            }

            if (!UserHasRequiredRoles(claims)) {
                _logger.LogDebug("User {userId} does not have required role(s).", NameIdentifier);
                return Task.FromResult(Fail("User does not have required role(s)."));
            }

            SetAuthenticatedUser(claims);

            var ticket = new AuthenticationTicket(claims, Scheme.Name);
            return Task.FromResult(Success(ticket));
        }
        catch (Exception ex) {
            return Task.FromResult(Fail(ex.Message));
        }
    }

    private bool RequiresAuth() {
        var anonAttribute = Context.GetEndpoint()!.Metadata.GetMetadata<AllowAnonymousAttribute>();
        if (anonAttribute is not null) return false;
        var authAttribute = Context.GetEndpoint()!.Metadata.GetMetadata<AuthorizeAttribute>();
        if (authAttribute is null) return false;
        return true;
    }

    private void SetAuthenticatedUser(ClaimsPrincipal claims) {
        var identity = new ClaimsIdentity(claims.Identity);
        Context.User = new ClaimsPrincipal(identity);
    }

    private bool UserHasRequiredRoles(ClaimsPrincipal claims) {
        var userRoles = claims.FindAll(ClaimTypes.Role).Select(i => i.Value).ToArray();
        var authAttribute = Context.GetEndpoint()!.Metadata.GetMetadata<AuthorizeAttribute>();
        if (authAttribute is not { Roles: { } roleList }) return true;
        var roles = roleList.Split(',').Select(r => r.Trim()).ToArray();
        return roles.Intersect(userRoles).Any();
    }

    private ClaimsPrincipal GetClaims(string token) {
        var apiKey = token[_tokenPrefix.Length..].Trim();
        var validationParameters = new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(_issuerSigningKey),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ClockSkew = TimeSpan.Zero
        };
        var principal = _tokenHandler.ValidateToken(apiKey, validationParameters, out _);
        return principal;
    }
}