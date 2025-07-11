using MailKit.Net.Smtp;
using MimeKit;
using SOE.Configuration;
using Microsoft.Extensions.Options;

namespace SOE.Services;

public interface IEmailSender {
    Task SendOtpAsync(string email, string otp);
}

public class EmailSender(IOptions<SmtpConfig> config, ILogger<EmailSender> logger): IEmailSender {
    
    public async Task SendOtpAsync(string email, string otp) {
        logger.LogDebug("Sending OTP {Otp}to email {Email}", otp, email);

        var message = BuildOtpMessage(email, otp);
        await SendEmailAsync(message);
        
        logger.LogDebug("Successfully sent OTP to email {Email}", email);
    }

    private MimeMessage BuildOtpMessage(string email, string otp) {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(config.Value.From));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = "SOE - Your OTP Code";
        message.Body = new TextPart("plain") {
            Text = $"Your OTP code is: {otp}.\nDo not share this code with anyone."
        };
        return message;
    }
    
    private async Task SendEmailAsync(MimeMessage message) {
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(config.Value.Server, 587, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(config.Value.From, config.Value.Password);
        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);
    }
    
}