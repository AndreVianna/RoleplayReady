using System.Utilities;

namespace RolePlayReady.Security.Handlers;

public class AuthenticationHandler : IAuthenticationHandler {
    private readonly IConfiguration _configuration;
    private readonly IDateTime _dateTime;

    public AuthenticationHandler(IConfiguration configuration, IDateTime dateTime) {
        _configuration = configuration;
        _dateTime = dateTime;
    }

    public Result<string> Authenticate(Login login) {
        var result = login.Validate();
        if (result.HasErrors) {
            return result.WithValue(string.Empty);
        }

        if (!IsCorrect(login)) {
            return Result.Failure(string.Empty, "AuthenticationFailed", nameof(login));
        }

        // Generate the JWT
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, _configuration["Security:DefaultUser:Id"]!),
            new Claim(ClaimTypes.GivenName, _configuration["Security:DefaultUser:Name"]!),
            new Claim(ClaimTypes.Name, _configuration["Security:DefaultUser:Username"]!),
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
        => login.Email.Equals(_configuration["Security:DefaultUser:Email"])
        && login.Password.Equals(_configuration["Security:DefaultUser:Password"]);
}