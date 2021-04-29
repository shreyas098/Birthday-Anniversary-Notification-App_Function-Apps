using EmailSendingFunctionApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailSendingFunctionApp.Services.Abstract
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
