using AccountManagementAPI.Configuration;
using AccountManagementAPI.HelperServices.Email.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using AccountManagementAPI.Constants;
using AccountManagementAPI.HelperServices.Email.Models;
using MailKit.Security;
using AccountManagment.Libraries.Shared.Constants;

namespace AccountManagementAPI.HelperServices.Email.Repository;

public class EmailService(IOptionsSnapshot<SmtpConfiguration> smtpConfiguration,
                          ILogger<EmailService> logger) : IEmailService
{
    public async Task SendEmailAsync(EmailModel emailModel)
    {
        logger.LogInformation("EmailService => Attempting to send email");

        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(EmailConstants.senderName, EmailConstants.senderEmail));
        emailMessage.To.Add(new MailboxAddress(emailModel.recieverName, emailModel.recieverEmail));      
        emailMessage.Subject = emailModel.subject;
        emailMessage.Body = new TextPart("plain") { Text = emailModel.body };

        try 
        {
            using (var smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(smtpConfiguration.Value.Host, smtpConfiguration.Value.Port, SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(smtpConfiguration.Value.Username, smtpConfiguration.Value.Password);
                await smtpClient.SendAsync(emailMessage);
                await smtpClient.DisconnectAsync(true);
            }

            logger.LogInformation("{Announcement}: Attempt to send email was successful", 
                LoggerConstants.Succeeded);

        }catch (Exception ex) 
        {
            logger.LogInformation(ex, "{Announcement}: Attempt to send email was unsuccessful",
                LoggerConstants.Failed);
        }
    }
}
