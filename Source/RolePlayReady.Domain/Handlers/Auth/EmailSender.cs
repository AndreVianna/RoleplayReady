using System.Net;
using System.Net.Mail;

namespace RolePlayReady.Handlers.Auth;

public class EmailSender : IEmailSender {
    private readonly IConfiguration _configuration;
    private readonly IDateTime _dateTime;

    public EmailSender(IConfiguration configuration, IDateTime dateTime) {
        _configuration = configuration;
        _dateTime = dateTime;
    }

    public async Task SendEmailConfirmationMessage(User user, CancellationToken ct) {
        var token = GenerateEmailConfirmationToken(user);
        var mailBody = GetConfirmationEmailBody(token);
        var mailMessage = new MailMessage {
            From = new MailAddress("no-reply@roleplayready.com"),
            Body = mailBody,
            Subject = "Account Confirmation",
            IsBodyHtml = true,
            BodyEncoding = Encoding.UTF8,
            DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
            Priority = MailPriority.High,
            To = { new MailAddress(user.Email) }
        };

        using var client = new SmtpClient("smtp.gmail.com", 587) {
            EnableSsl = true,
            Credentials = new NetworkCredential("roleplayready@gmail.com", "password")
        };
        await client.SendMailAsync(mailMessage, ct);
    }

    private string GenerateEmailConfirmationToken(User user) {
        var claims = new List<Claim> {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };
        var credentials = GetCredentials();

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(claims),
            Expires = _dateTime.Now.AddMinutes(30),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token).Replace("+", "-").Replace("/", "_");
    }

    private string GetConfirmationEmailBody(string token)
        => $"""
           <h1>Please click the following link to confirm your account:</h1><br/>
           <a href=\"https://example.com/confirm?token={token}\">Confirm your account</a>";
           """;

    private SigningCredentials GetCredentials() {
        var issuerSigningKey = Ensure.IsNotNullOrWhiteSpace(_configuration["Security:IssuerSigningKey"]);
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(issuerSigningKey));
        return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    }
}