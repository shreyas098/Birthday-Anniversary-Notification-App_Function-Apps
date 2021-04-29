using EmailSendingFunctionApp.Models;
using EmailSendingFunctionApp.Services.Abstract;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailSendingFunctionApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly string EmailApiKey;
        public EmailService(string emailApiKey)
        {
            EmailApiKey = emailApiKey;
        }
        public async Task SendAsync(EmailRequest request)
        {
            if (IsReceiverKiproshEmail(request.To))
            {
                var client = new SendGridClient(EmailApiKey);
                var from = new EmailAddress(request.From, "KiproshAdmin");
                var subject = request.Subject;
                var to = new EmailAddress(request.To);
                var plainTextContent = request.Body;
                var htmlContent = request.Body;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                var response = await client.SendEmailAsync(msg);
            }
        }

        private bool IsReceiverKiproshEmail(string toEmail)
        {
            return toEmail.EndsWith("@kiprosh.com");
        }
    }
}
