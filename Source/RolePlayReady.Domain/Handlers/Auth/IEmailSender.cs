namespace RolePlayReady.Handlers.Auth;

public interface IEmailSender {
    Task SendEmailConfirmationMessage(User user, CancellationToken ct);
}