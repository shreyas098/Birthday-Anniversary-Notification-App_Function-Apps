using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSendingFunctionApp.Models
{
    public class EmailRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class BirthdayNotificationModel : EmailRequest
    {
        public string Message { get; set; }
        public string AssociateName { get; set; }
        public string Template { get; set; }
    }

    public class BirthdayReminderNotificationModel : EmailRequest
    {
        public string Associates { get; set; }        
        public string Template { get; set; }
    }
}
