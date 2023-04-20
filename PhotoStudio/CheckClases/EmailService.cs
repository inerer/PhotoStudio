using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;

namespace PhotoStudio;

public class EmailService
{
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        using var emailMessage = new MimeMessage();
 
        emailMessage.From.Add(new MailboxAddress("Администрация сайта", "photostudioarshinov@yandex.ru"));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message
        };
             
        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("photostudioarshinov.yandex.ru", 25, false);
            await client.AuthenticateAsync("photostudioarshinov@yandex.ru", "DaNiLa04");
            await client.SendAsync(emailMessage);
 
            await client.DisconnectAsync(true);
        }
    }
}