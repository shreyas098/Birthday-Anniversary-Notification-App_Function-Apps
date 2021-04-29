using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSendingFunctionApp.Models
{
    public class CommonModel
    {       
        public class UserLookUpResponseMessage
        {
            public bool ok { get; set; }
            public SlackUserModel user { get; set; }            
        }

        public class SlackUserModel { 
            public string id { get; set; }
            public string team_id { get; set; }
            public string name { get; set; }
            public string real_name { get; set; }
        }        
    }

}
