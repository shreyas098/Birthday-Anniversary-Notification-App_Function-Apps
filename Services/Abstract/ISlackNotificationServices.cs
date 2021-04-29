using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailSendingFunctionApp.Services.Abstract
{
    public interface ISlackNotificationServices
    {
        Task PostMessageToChannel(string msg);
        Task NotifyUser(string payload);
        Task<string> GetUserByEmail(string email);
    }
}
