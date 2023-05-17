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
            var endpoint = Context.GetEndpoint();
            if (endpoint is null)
                return Task.FromResult(NoResult());

            if (!RequiresAuth(endpoint)) return Task.FromResult(NoResult());

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

            if (!UserHasRequiredRoles(endpoint, claims)) {
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

    private bool RequiresAuth(Endpoint endpoint) {
        var anonAttribute = endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>();
        if (anonAttribute is not null) return false;
        var authAttribute = endpoint.Metadata.GetMetadata<AuthorizeAttribute>();
        if (authAttribute is null) return false;
        return true;
    }

    private void SetAuthenticatedUser(ClaimsPrincipal claims) {
        var identity = new ClaimsIdentity(claims.Identity);
        Context.User = new ClaimsPrincipal(identity);
    }

    private bool UserHasRequiredRoles(Endpoint endpoint, ClaimsPrincipal claims) {
        var authAttribute = endpoint.Metadata.GetMetadata<AuthorizeAttribute>();
        if (authAttribute is not { Roles: { } roles }) return true;
        var requiredRoles = roles.Split(',').Select(r => r.Trim()).ToArray();

        var userRoles = claims.FindAll(ClaimTypes.Role).Select(i => i.Value).ToArray();
        return requiredRoles.Intersect(userRoles).Any();
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