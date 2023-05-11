using static System.Results.SignInResult;

namespace RolePlayReady.Handlers.Auth;

public class AuthHandler : CrudHandler<User, UserRow, IUserRepository>, IAuthHandler {
    private readonly IHasher _hasher;
    private readonly IConfiguration _configuration;
    private readonly IDateTime _dateTime;
    private readonly ILogger<AuthHandler> _logger;

    public AuthHandler(IUserRepository repository, IHasher hasher, IConfiguration configuration, IDateTime dateTime, ILogger<AuthHandler> logger) : 
        base(repository) {
        _hasher = hasher;
        _configuration = configuration;
        _dateTime = dateTime;
        _logger = logger;
    }

    public async Task<SignInResult> SignInAsync(SignIn signIn, CancellationToken cancellation = default) {
        var validation = signIn.Validate();
        if (validation.IsInvalid) {
            _logger.LogDebug("Login attempt with invalid request.");
            return Invalid(validation.Errors);
        }

        var isVerified = await Repository.VerifyAsync(signIn, cancellation);
        if (!isVerified) {
            _logger.LogDebug("Login attempt for '{email}' failed.", signIn.Email);
            return Failure();
        }

        var token = GenerateSignInToken();
        _logger.LogDebug("Login for '{email}' succeeded.", signIn.Email);
        return Success(token);
    }

    public async Task<CrudResult> RegisterAsync(SignOn signOn, CancellationToken cancellation = default) {
        var validation = signOn.Validate();
        if (validation.IsInvalid) {
            _logger.LogDebug("Register attempt with invalid request.");
            return CrudResult.Invalid(validation.Errors);
        }

        var user = new User {
            Id = Guid.NewGuid(),
            Name = signOn.Name,
            Email = signOn.Email,
            HashedPassword = _hasher.HashSecret(signOn.Password),
        };

        //ToDo - Verify if user already exists by email
        var addedUser = await Repository.AddAsync(user, cancellation);

        return addedUser is null
            ? CrudResult.Conflict()
            : CrudResult.Success();
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
}