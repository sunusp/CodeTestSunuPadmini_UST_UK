using System.Net.Http;
using System;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using DomainModel;

namespace ServiceClient.httpClients
{
    public class MessagingClient : IMessagingClient
    {
        private readonly HttpClient httpClient;
        public MessagingClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> SendEmail(string toEmail, Enums.UserTypes userType, Enums.NotificationTypes notificationType)
        {
            var response = await httpClient.GetStringAsync($"/Messages?toEmail={toEmail}&userType={(int)userType}&notificationType={(int)notificationType}");
            return JsonConvert.DeserializeObject<bool>(response);
        }
    }
}
