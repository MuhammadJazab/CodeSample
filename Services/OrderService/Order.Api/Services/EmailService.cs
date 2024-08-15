//file="EmailService.cs" >

namespace Order.Api.Services;

/// <summary>
/// Defines the <see cref="EmailService" />.
/// </summary>
/*
public class EmailService : IEmailService
{
    /// <summary>
    /// Defines the EmailConfigurations.
    /// </summary>
    private readonly EmailConfigurations emailSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailService"/> class.
    /// </summary>
    /// <param name="options"><see cref="IOptions{EmailConfigurations}"/></param>
    public EmailService(IOptions<EmailConfigurations> options)
    {
        this.emailSettings = options.Value;
    }

    /// <summary>
    /// Defines the SendEmail.
    /// </summary>
    /// <param name="email"><see cref="Email"/></param>
    public async Task<bool> SendEmail(Email email)
    {
        var client = new SendGridClient(emailSettings.ApiKey);

        var subject = email.Subject;
        var to = new EmailAddress(email.To);
        var emailBody = email.Body;

        var from = new EmailAddress
        {
            Email = emailSettings.FromAddress,
            Name = emailSettings.FromName
        };

        var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
        var response = await client.SendEmailAsync(sendGridMessage);

        if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
            return true;


        return false;
    }
}

*/