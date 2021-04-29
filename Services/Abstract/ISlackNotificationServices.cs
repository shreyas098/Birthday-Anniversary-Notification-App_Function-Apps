using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static EmailSendingFunctionApp.Models.CommonModel;

namespace EmailSendingFunctionApp.Services.Abstract
{
    public interface ISlackNotificationServices
    {
        Task PostMessageToChannel(string msg);
        Task NotifyUser(string payload);
        Task<SlackUserModel> GetUserByEmail(string email);
    }
}
