using EmailSendingFunctionApp.Database;
using EmailSendingFunctionApp.EventReminderEmails;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.SqlServer;
using EmailSendingFunctionApp.Services.Abstract;
using EmailSendingFunctionApp.Services;
using Microsoft.Azure.WebJobs;

[assembly: WebJobsStartup(typeof(EmailSendingFunctionApp.Startup))]
namespace EmailSendingFunctionApp
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            try
            {               
                //Register Functions
                builder.Services.AddSingleton<IEventReminderEmailToAssociateFunction, EventReminderEmailToAssociateFunction>();
                builder.Services.AddSingleton<ISendBirthdayWishesFunction, SendBirthdayWishesFunction>();

                //Register Services
                builder.Services.AddSingleton<IEmailService>(new EmailService(Environment.GetEnvironmentVariable("EmailApiKey")));

                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DbConnectionString"));
                builder.Services.AddSingleton<IAssociateQueryServices>( new AssociateQueryServices(optionsBuilder.Options));

                builder.Services.AddSingleton<ISlackNotificationServices>(new SlackNotificationServices(Environment.GetEnvironmentVariable("SlackApiToken")));

                builder.Services.AddSingleton<ITemplateStoreService>(new TemplateStoreServices(Environment.GetEnvironmentVariable("AzureWebJobsStorage"), Environment.GetEnvironmentVariable("TemplateContainer")));
                builder.Services.AddSingleton<INotificationServices, NotificationServices>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
