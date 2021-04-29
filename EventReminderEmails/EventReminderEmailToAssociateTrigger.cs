using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailSendingFunctionApp.EventReminderEmails
{
    class EventReminderEmailToAssociateTrigger
    {
        const string Timer = "0 0 0 * * *";
        private readonly IEventReminderEmailToAssociateFunction TheFunction;
        public EventReminderEmailToAssociateTrigger(IEventReminderEmailToAssociateFunction theFunction)
        {
            TheFunction = theFunction;
        }

        [FunctionName("EventReminderEmailToAssociateFunction")]
        public async Task Run([TimerTrigger(Timer)] TimerInfo myTimer)
        {
            await TheFunction.InvokeAsync();
        }
    }
}
