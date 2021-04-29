using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSendingFunctionApp.Services.Abstract
{
    public interface ITemplateStoreService
    {
        string GetTemplate(string name);
    }
}
