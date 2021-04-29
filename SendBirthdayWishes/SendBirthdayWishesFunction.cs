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
                    msg += $"<span>{m.Message}</span><br/> <span>-{m.SenderName}</span><br/><br/>";
                });

                //Send Email Notification
                await NotificationServices.SendEmail(new Models.BirthdayNotificationModel
                {
                    To = bd.AssociateEmail,                                      
                    Subject = $"Happy Birthday {bd.AssociateName}",
                    Template = AssociateQueryServices.GetValue("BirthdayTemplate"),
                    Message = msg,
                    AssociateName = bd.AssociateName                    
                });

                await NotificationServices.NotifyUser(bd.AssociateEmail, msgList);
            }                      
        }
    }
}
