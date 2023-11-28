using DomainModel;
using MessagingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DomainModel.Enums;

namespace MessagingService.Services
{
    public interface IMessagingService
    {
        public Task<SendMailResponse> SendEmail(string toEmail, Enums.UserTypes userType, Enums.NotificationTypes notificationType);
        public GlobalEmailTemplate GetGlobalEmailTemplate(UserTypes userType, NotificationTypes notificationType);
    }
}
