using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSendingFunctionApp.Models
{
    public class AssociateDetailModel
    {
        public string AssociateName { get; set; }
        public string AssociateEmail { get; set; }
        public int AssociateId { get; set; }
    }

    public class MessageModel
    {
        public string SenderName { get; set; }
        public int SenderId { get; set; }
        public string Message { get; set; }
    }    
}

