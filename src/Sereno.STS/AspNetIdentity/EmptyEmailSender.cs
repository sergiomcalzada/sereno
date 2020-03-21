using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;

namespace Sereno.STS.AspNetIdentity
{
    public class EmptyEmailSender : IEmailSender
    {
        private readonly ILogger<EmptyEmailSender> logger;

        public EmptyEmailSender(ILogger<EmptyEmailSender> logger)
        {
            this.logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
           this.logger.LogDebug("Sending confirmation to {email}: {htmlMessage}", email, htmlMessage );
           return Task.CompletedTask;
        }
    }
}
