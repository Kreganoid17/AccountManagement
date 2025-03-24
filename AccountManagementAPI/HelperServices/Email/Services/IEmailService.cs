using AccountManagementAPI.HelperServices.Email.Models;

namespace AccountManagementAPI.HelperServices.Email.Services;

public interface IEmailService
{
    Task SendEmailAsync(EmailModel emailModel);
}
