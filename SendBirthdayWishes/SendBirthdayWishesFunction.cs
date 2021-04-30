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
                var msgList = AssociateQueryServices.GetBirthdayWishesByAssociateId(bd.AssociateId);
                var msg = "";
                if (msgList.Count > 0)
                {
                    msg = "<h4>We have a surprise birthday wishes for you</h4> <br/>";
                    msgList.ForEach(m =>
                    {
                        msg += $"<span>{m.Message}</span><br/> <span>-{m.SenderName}</span><br/><br/>";
                    });
                }
                else {
                    msg = "We Wish you a year Full of Minutes of Love and Happiness";
                }
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
