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
            content=content.Replace("[Messages]", request.Message);
            content = content.Replace("[AssociateName]", request.AssociateName);
            await EmailService.SendAsync(new EmailRequest { 
                To = request.To,
                Body = content,
                From = "anjali.sharma@kiprosh.com",
                Subject = request.Subject               
            });
        }

        public async Task SendEmail(BirthdayReminderNotificationModel request)
        {
            var content = TemplateStoreServices.GetTemplate(request.Template);           
            await EmailService.SendAsync(new EmailRequest
            {
                To = request.To,
                Body = content,
                From = "anjali.sharma@kiprosh.com",
                Subject = request.Subject,
            });
        }


        public async Task NotifyOnSlackChannel(string msg)
        {
            await SlackNotificationServices.PostMessageToChannel(msg);
        }

        public async Task NotifyUser(string email, List<MessageModel> msgList)
        {
            var slackUser = await SlackNotificationServices.GetUserByEmail(email);
            await SlackNotificationServices.NotifyUser(CreatePayload(msgList, slackUser.id));
        }

        private string CreatePayload(List<MessageModel> msgList, string id) {
            var payload = "{\"channel\":\"" + id + "\",\"blocks\": [{\"type\": \"section\",\"text\": {\"type\": \"mrkdwn\",\"text\": \"Hey <@UBFUGRUDA> :wave: Here are the birthday wishes for you :tada:\"}},{\"type\": \"image\",\"title\": {\"type\": \"plain_text\",\"text\": \"image1\",\"emoji\": true},\"image_url\": \"https://thumbs.dreamstime.com/b/colorful-happy-birthday-cupcakes-candles-spelling-148323072.jpg\",\"alt_text\": \"image1\"},{\"type\": \"divider\"},{\"type\": \"section\",\"fields\": [";
            var fields ="";
            msgList.ForEach(x =>
            {
                var f = "{\"type\": \"mrkdwn\",\"text\":\"*" + x.Message + "*\\n -_" + x.SenderName +"_\"}";
                if (!string.IsNullOrWhiteSpace(fields))
                {
                    fields += ",";
                }
                fields += f;
            });
            payload += fields +"]}]}";           
            return payload;
        }
       
    }
}
