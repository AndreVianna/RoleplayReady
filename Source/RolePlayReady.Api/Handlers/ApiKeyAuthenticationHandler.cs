namespace RolePlayReady.Api.Handlers;

internal class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions> {
    private const string _apiKeyHeaderName = "X-Api-Key";

    public ApiKeyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock) {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync() {
        var claims = new List<Claim> { new(ClaimTypes.NameIdentifier, "DummyUser") };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}