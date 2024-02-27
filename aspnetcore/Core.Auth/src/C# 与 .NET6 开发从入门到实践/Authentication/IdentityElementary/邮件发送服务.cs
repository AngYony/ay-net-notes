using Microsoft.AspNetCore.Identity.UI.Services;

namespace OpenIddictServer;

public class DesktopFileEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        File.WriteAllText($@"{dir}\email.html", $"email:{email}\r\nsubject:{subject}\r\ncontent:{htmlMessage}");
        return Task.CompletedTask;
    }
}
