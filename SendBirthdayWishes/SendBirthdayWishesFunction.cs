using System;
using System.Threading.Tasks;
using EmailSendingFunctionApp.Services.Abstract;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace EmailSendingFunctionApp.EventReminderEmails
{    
    public interface ISendBirthdayWishesFunction
    {
        Task InvokeAsync();
    }
    public class SendBirthdayWishesFunction : ISendBirthdayWishesFunction
    {
        private readonly INotificationServices NotificationServices;
        private readonly IAssociateQueryServices AssociateQueryServices;        
        public SendBirthdayWishesFunction(IAssociateQueryServices associateQueryServices,
            INotificationServices notificationServices)
        {
            AssociateQueryServices = associateQueryServices;
            NotificationServices = notificationServices;            
        }
        public async Task InvokeAsync()
        {
            var birthdayList = AssociateQueryServices.GetBirthdayPersonList();
            foreach(var bd in birthdayList)
            {
                var msg = "";
                var msgList = AssociateQueryServices.GetBirthdayWishesByAssociateId(bd.AssociateId);
                msgList.ForEach(m =>
                {
                    msg += $"<div style=\"display:flex; flex: 1 1 auto;\"><h2>{m.Message}</h2><h3>By {m.SenderName}</h3></div>";
                });

                //Send Email Notification
                await NotificationServices.SendEmail(new Models.BirthdayNotificationModel
                {
                    To = bd.AssociateEmail,
                    From = "anjali.sharma@kiprosh.com",                    
                    Subject = $"Happy Birthday {bd.AssociateName}",
                    Template = "BirthdayEmail.html"                    
                });

                await NotificationServices.NotifyUser(bd.AssociateEmail, msgList);
            }                      
        }
    }
}
