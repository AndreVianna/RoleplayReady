﻿using System.Net;
using System.Net.Mail;

namespace RolePlayReady.Handlers.Auth;

public class EmailSender(ITokenGenerator tokenGenerator) : IEmailSender {
    public async Task SendEmailConfirmationMessage(User user, CancellationToken ct) {
        var token = tokenGenerator.GenerateEmailConfirmationToken(user);
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

    private static string GetConfirmationEmailBody(string token) => $"""
           <h1>Please click the following link to confirm your account:</h1><br/>
           <a href=\"https://example.com/confirm?token={token}\">Confirm your account</a>";
           """;
}