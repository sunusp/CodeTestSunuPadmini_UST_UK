using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DomainModel.Enums;

namespace MessagingService.Models
{
    public class GlobalEmailTemplate
    {
        public UserTypes UserType { get; set; }

        public NotificationTypes NotificationType { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string CcEmail { get; set; }
    }
}
