using EmailSendingFunctionApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailSendingFunctionApp.Services.Abstract
{
    public interface INotificationServices
    {
        Task SendEmail(BirthdayNotificationModel request);
        Task SendEmail(BirthdayReminderNotificationModel request);        
        Task NotifyOnSlackChannel(string msg);
        Task NotifyUser(string email, List<MessageModel> msgList);

    }
}
