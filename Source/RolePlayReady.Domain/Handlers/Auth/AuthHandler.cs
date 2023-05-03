using static System.Results.SignInResult;

namespace RolePlayReady.Handlers.Auth;

public class AuthHandler : IAuthHandler {
    private readonly IConfiguration _configuration;
    private readonly IDateTime _dateTime;
    private readonly ILogger<AuthHandler> _logger;

    public AuthHandler(IConfiguration configuration, IDateTime dateTime, ILogger<AuthHandler> logger) {
        _configuration = configuration;
        _dateTime = dateTime;
        _logger = logger;
    }

    public SignInResult Authenticate(Login login) {
        var validation = login.Validate();
        if (validation.IsInvalid) {
            _logger.LogDebug("Login attempt with invalid request.");
            return Invalid(validation.Errors);
        }

        if (!IsCorrect(login)) {
            _logger.LogDebug("Login attempt for '{email}' failed.", login.Email);
            return Failure();
        }

        var token = GenerateSignInToken();
        _logger.LogDebug("Login for '{email}' succeeded.", login.Email);
        return Success(token);
    }

    private string GenerateSignInToken() {
        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, _configuration["Security:DefaultUser:Id"]!),
            new Claim(ClaimTypes.GivenName, _configuration["Security:DefaultUser:Name"]!),
            new Claim(ClaimTypes.Name, _configuration["Security:DefaultUser:FolderName"]!),
            new Claim(ClaimTypes.Email, _configuration["Security:DefaultUser:Email"]!)
        };

        var issuerSigningKey = Ensure.IsNotNullOrWhiteSpace(_configuration["Security:IssuerSigningKey"]);
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(issuerSigningKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenExpirationInHours = int.Parse(_configuration["Security:TokenExpirationInHours"]!);
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(claims),
            Expires = _dateTime.Now.AddHours(tokenExpirationInHours),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private bool IsCorrect(Login login)
        => login.Email.ToLower().Equals(_configuration["Security:DefaultUser:Email"]!.ToLower())
        && login.Password.Equals(_configuration["Security:DefaultUser:Password"]);
}