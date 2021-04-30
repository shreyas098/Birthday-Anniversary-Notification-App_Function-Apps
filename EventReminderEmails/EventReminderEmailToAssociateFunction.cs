using System;
using System.Linq;
using System.Threading.Tasks;
using EmailSendingFunctionApp.Services.Abstract;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace EmailSendingFunctionApp.EventReminderEmails
{    
    public interface IEventReminderEmailToAssociateFunction
    {
        Task InvokeAsync();
    }
    public class EventReminderEmailToAssociateFunction : IEventReminderEmailToAssociateFunction
    {        
        private readonly IAssociateQueryServices AssociateQueryServices;
        private readonly INotificationServices NotificationServices;
        public EventReminderEmailToAssociateFunction(IAssociateQueryServices associateQueryServices,
            INotificationServices notificationServices)
        {
            AssociateQueryServices = associateQueryServices;           
            NotificationServices = notificationServices;
        }
        public async Task InvokeAsync()
        {
            var allBDList = AssociateQueryServices.GetUpcomingAssociateBirthdays();           
            if (allBDList != null && allBDList.Count > 0)
            {
                var associates = AssociateQueryServices.GetAssociateEmailList();
                var subject = AssociateQueryServices.GetValue("BirthdayReminder_Subject");
                foreach (var asc in associates)
                {
                    var bdList = allBDList.Where(x => x.AssociateId != asc.AssociateId).ToList();                    
                    if (bdList!=null && bdList.Count > 0)
                    {
                        var data = string.Join(", ", bdList.Select(x => x.AssociateName).ToList());
                        await NotificationServices.SendEmail(new Models.BirthdayReminderNotificationModel
                        {
                            To = asc.AssociateEmail,
                            Subject = $"{subject} ({DateTime.UtcNow.AddDays(1).Date.ToShortDateString()})",
                            Template = AssociateQueryServices.GetValue("BirthdayReminderTemplate"),
                            Associates = data
                        });
                    }
                }
                await NotificationServices.NotifyOnSlackChannel(AssociateQueryServices.GetValue("BirthDayReminderMessage"));
            }
        }
    }
}
