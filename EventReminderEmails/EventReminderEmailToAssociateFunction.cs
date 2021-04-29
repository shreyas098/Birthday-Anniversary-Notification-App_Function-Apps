using System;
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
            var bdList = AssociateQueryServices.GetUpcomingAssociateBirthdays();
            var associates = AssociateQueryServices.GetAssociateEmailList();
            foreach (var item in bdList)
            {
                foreach (var asc in associates)
                {
                    if (asc.AssociateId != item.AssociateId)
                    {
                        await NotificationServices.SendEmail(new Models.BirthdayReminderNotificationModel
                        {
                            To = asc.AssociateEmail,                            
                            Subject = AssociateQueryServices.GetValue("BirthdayReminder_Subject"),
                            Template = AssociateQueryServices.GetValue("BirthdayReminderTemplate")
                        });
                    }
                }              
            }
            await NotificationServices.NotifyOnSlackChannel(AssociateQueryServices.GetValue("BirthDayReminderMessage"));
        }
    }
}
