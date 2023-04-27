using System.Utilities;

namespace RolePlayReady.Api.Handlers;

internal class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions> {
    private readonly IConfiguration _configuration;
    private const string _authHeader = "Authorization";
    private const string _tokenPrefix = "Bearer ";

    public ApiKeyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, IConfiguration configuration, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock) {
        _configuration = configuration;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync() {
        try {
            if (!Request.Headers.TryGetValue(_authHeader, out var authorizationHeaderValues)) {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var token = authorizationHeaderValues.FirstOrDefault();
            if (token == null || !token.StartsWith(_tokenPrefix)) {
                return Task.FromResult(AuthenticateResult.Fail("Missing or malformed 'Bearer' token in 'Authorization' header."));
            }

            var apiKey = token[_tokenPrefix.Length..].Trim();
            var tokenHandler = new JwtSecurityTokenHandler();
            var issuerSigningKey = Encoding.ASCII.GetBytes(Ensure.IsNotNullOrWhiteSpace(_configuration["Security:IssuerSigningKey"], "Configuration[\"Security:IssuerSigningKey\"]"));
            var validationParameters = new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(issuerSigningKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            };
            var principal = tokenHandler.ValidateToken(apiKey, validationParameters, out _);
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) {
                return Task.FromResult(AuthenticateResult.Fail("User ID claim is missing."));
            }

            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch (Exception ex) {
            return Task.FromResult(AuthenticateResult.Fail(ex.Message));
        }
    }
}