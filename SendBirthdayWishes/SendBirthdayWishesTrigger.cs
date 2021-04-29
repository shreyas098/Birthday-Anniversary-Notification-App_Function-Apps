using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailSendingFunctionApp.EventReminderEmails
{
    class SendBirthdayWishesTrigger
    {
        const string Timer = "0 */5 * * * *";
        private readonly ISendBirthdayWishesFunction TheFunction;
        public SendBirthdayWishesTrigger(ISendBirthdayWishesFunction theFunction)
        {
            TheFunction = theFunction;
        }

        [FunctionName("SendBirthdayWishesFunction")]
        public async Task Run([TimerTrigger(Timer)] TimerInfo myTimer)
        {
            await TheFunction.InvokeAsync();
        }
    }
}
