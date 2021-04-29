using EmailSendingFunctionApp.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using static EmailSendingFunctionApp.Models.CommonModel;

namespace EmailSendingFunctionApp.Services
{
    public class SlackNotificationServices: ISlackNotificationServices
    {
        private HttpClient apiClient = new HttpClient();
        private readonly string SlackToken;
        public SlackNotificationServices(string token)
        {
            SlackToken = token;            
        }

        public async Task PostMessageToChannel(string msg)
        {
            using (var message = new HttpRequestMessage(HttpMethod.Post, Environment.GetEnvironmentVariable("SlackBirthdayChannelAPi")))
            {
                apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                message.Content = new StringContent(msg, Encoding.UTF8, "application/json");
                await apiClient.SendAsync(message);               
            }
        }

        public async Task NotifyUser(string payload)
        {
            using (var message = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/chat.postMessage"))
            {
                apiClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SlackToken);                
                message.Content = new StringContent(payload, Encoding.UTF8, "application/json");
                await apiClient.SendAsync(message);
            }
        }

        public async Task<SlackUserModel> GetUserByEmail(string email)
        {
            using (var message = new HttpRequestMessage(HttpMethod.Get, $"https://slack.com/api/users.lookupByEmail?email={email}"))
            {
                apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SlackToken);                
                var response =await apiClient.SendAsync(message);
                var data = JsonSerializer.Deserialize<UserLookUpResponseMessage>(response.Content.ReadAsStringAsync().Result);
                return data.user;
            }
        }
    }
}
