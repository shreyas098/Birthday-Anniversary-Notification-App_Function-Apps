using EmailSendingFunctionApp.Models;
using EmailSendingFunctionApp.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailSendingFunctionApp.Services
{
    public class NotificationServices : INotificationServices
    {
        
        private readonly ISlackNotificationServices SlackNotificationServices;
        private readonly IEmailService EmailService;
        private readonly ITemplateStoreService TemplateStoreServices;

        public NotificationServices(ISlackNotificationServices slackNotificationServices,
            IEmailService emailService,
            ITemplateStoreService templateStoreService)
        {
            SlackNotificationServices = slackNotificationServices;
            EmailService = emailService;
            TemplateStoreServices = templateStoreService;
        }
        public async Task SendEmail(BirthdayNotificationModel request)
        {
            var content = TemplateStoreServices.GetTemplate(request.Template);
            content=content.Replace("Data", request.Message);
            content = content.Replace("AssociateName", request.AssociateName);
            await EmailService.SendAsync(new EmailRequest { 
                To = request.To,
                Body = content,
                From = request.From,
                Subject = request.Subject,                
            });
        }

        public async Task SendEmail(BirthdayReminderNotificationModel request)
        {
            var content = TemplateStoreServices.GetTemplate(request.Template);           
            await EmailService.SendAsync(new EmailRequest
            {
                To = request.To,
                Body = content,
                From = request.From,
                Subject = request.Subject,
            });
        }


        public async Task NotifyOnSlackChannel(string msg)
        {
            await SlackNotificationServices.PostMessageToChannel(msg);
        }

        public async Task NotifyUser(string email, List<MessageModel> msgList)
        {
            var slackUserId = await SlackNotificationServices.GetUserByEmail(email);
            //await SlackNotificationServices.NotifyUser();
        }
       
    }
}
