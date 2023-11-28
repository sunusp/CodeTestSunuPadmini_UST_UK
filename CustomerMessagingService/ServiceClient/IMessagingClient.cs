using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClient.httpClients
{
    public interface IMessagingClient
    {
        public Task<bool> SendEmail(string toEmail, Enums.UserTypes userType, Enums.NotificationTypes notificationType);
    }
}
