using static System.Results.SignInResult;

namespace RolePlayReady.Handlers.Auth;

public class AuthHandler : CrudHandler<User, UserRow, IUserRepository>, IAuthHandler {
    private readonly IHasher _hasher;
    private readonly IConfiguration _configuration;
    private readonly IDateTime _dateTime;
    private readonly ILogger<AuthHandler> _logger;

    public AuthHandler(IUserRepository repository, IHasher hasher, IConfiguration configuration, IDateTime dateTime, ILogger<AuthHandler> logger) 
        : base(repository) {
        _hasher = hasher;
        _configuration = configuration;
        _dateTime = dateTime;
        _logger = logger;
    }

    public async Task<SignInResult> SignInAsync(SignIn signIn, CancellationToken cancellation = default) {
        var validation = signIn.Validate();
        if (validation.IsInvalid) {
            _logger.LogDebug("Login attempt with invalid request.");
            return Invalid(validation);
        }

        var user = await Repository.VerifyAsync(signIn, cancellation);
        if (user is null) {
            _logger.LogDebug("Login attempt for '{email}' failed.", signIn.Email);
            return Failure();
        }

        var token = GenerateSignInToken(user);
        _logger.LogDebug("Login for '{email}' succeeded.", signIn.Email);
        return Success(token);
    }

    public async Task<CrudResult> RegisterAsync(SignOn signOn, CancellationToken cancellation = default) {
        var validation = signOn.Validate();
        if (validation.IsInvalid) {
            _logger.LogDebug("Register attempt with invalid request.");
            return CrudResult.Invalid(validation);
        }

        var user = new User {
            Id = Guid.NewGuid(),
            FirstName = signOn.FirstName,
            LastName = signOn.LastName,
            Email = signOn.Email,
            HashedPassword = _hasher.HashSecret(signOn.Password),
        };

        var addedUser = await Repository.AddAsync(user, cancellation);

        return addedUser is null
            ? CrudResult.Conflict()
            : CrudResult.Success();
    }

    public async Task<CrudResult> GrantRoleAsync(UserRole userRole, CancellationToken cancellation = default) {
        var user = await Repository.GetByIdAsync(userRole.UserId, cancellation);
        if (user is null) {
            return CrudResult.NotFound();
        }

        user.Roles.Add(userRole.Role);
        await UpdateAsync(user, cancellation);

        return CrudResult.Success();
    }

    public async Task<CrudResult> RevokeRoleAsync(UserRole userRole, CancellationToken cancellation = default) {
        var user = await Repository.GetByIdAsync(userRole.UserId, cancellation);
        if (user is null) {
            return CrudResult.NotFound();
        }

        user.Roles.Remove(userRole.Role);
        await UpdateAsync(user, cancellation);

        return CrudResult.Success();
    }

    private string GenerateSignInToken(User user) {
        var claims = GenerateClaims(user);
        var expirationInHours = int.Parse(_configuration["Security:TokenExpirationInHours"]!);
        var credentials = GetCredentials();

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(claims),
            Expires = _dateTime.Now.AddHours(expirationInHours),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private SigningCredentials GetCredentials() {
        var issuerSigningKey = Ensure.IsNotNullOrWhiteSpace(_configuration["Security:IssuerSigningKey"]);
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(issuerSigningKey));
        return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    }

    private static IEnumerable<Claim> GenerateClaims(User user) {
        var claims = new List<Claim> {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.UserData, user.FolderName),
            new(ClaimTypes.Email, user.Email),
        };
        if (user.FirstName is not null)
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
        claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.ToString())));
        return claims;
    }
}