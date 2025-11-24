using Application.Common.Interfaces;

namespace Test.Common.Services;

public class InMemoryEmailSendingService : IEmailSendingService
{
    public Task SendEmailAsync(string emailTo, string emailBody)
    {
        return Task.CompletedTask;
    }
}