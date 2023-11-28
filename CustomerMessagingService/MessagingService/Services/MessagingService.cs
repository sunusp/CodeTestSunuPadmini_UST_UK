using DomainModel;
using log4net;
using MessagingService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;


namespace MessagingService.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly SMTPClientSettings _smtpClientSettings;
        private readonly List<GlobalEmailTemplate> globalEmailTemplates = new List<GlobalEmailTemplate>()
        {
            new GlobalEmailTemplate
            {
                UserType = DomainModel.Enums.UserTypes.Customer,
                NotificationType = DomainModel.Enums.NotificationTypes.Information,
                Body = "Message body for Customer Information Email",
                Subject = "Message Subject for Customer Information Email",
                CcEmail = "sample@cc.com"
            },
            new GlobalEmailTemplate
            {
                UserType = DomainModel.Enums.UserTypes.Customer,
                NotificationType = DomainModel.Enums.NotificationTypes.Warning,
                Body = "Message body for Customer Warning Email",
                Subject = "Message Subject for Customer Warning Email",
                CcEmail = "sample@cc.com"
            },
            new GlobalEmailTemplate
            {
                UserType = DomainModel.Enums.UserTypes.Employee,
                NotificationType = DomainModel.Enums.NotificationTypes.Warning,
                Body = "Message body for Employee Warning Email",
                Subject = "Message Subject for Employee Warning Email",
                CcEmail = "sample@cc.com"
            },
        };

        public MessagingService(
                IOptions<SMTPClientSettings> smtpClientSettings)
        {
            _smtpClientSettings = smtpClientSettings.Value;
        }

        public GlobalEmailTemplate GetGlobalEmailTemplate(Enums.UserTypes userType, Enums.NotificationTypes notificationType)
        {
            return globalEmailTemplates.Single(e => e.UserType == userType && e.NotificationType == notificationType);
        }

        public async Task<SendMailResponse> SendEmail(string toEmail, Enums.UserTypes userType, Enums.NotificationTypes notificationType)
        {
            try
            {
                var smtpClient = new SmtpClient(_smtpClientSettings.Host, _smtpClientSettings.Port)
                {
                    Credentials = new System.Net.NetworkCredential(_smtpClientSettings.NetworkUserName, _smtpClientSettings.NetworkPassword),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true
                };

                var globalEmailTemplate = GetGlobalEmailTemplate(userType, notificationType);
                var mail = new MailMessage()
                {
                    Subject = globalEmailTemplate.Subject,
                    Body = globalEmailTemplate.Body,
                    From = new MailAddress(_smtpClientSettings.FromEmail)
                };

                mail.To.Add(new MailAddress(toEmail));
                if (globalEmailTemplate.CcEmail != null)
                    mail.CC.Add(new MailAddress(globalEmailTemplate.CcEmail));

                await smtpClient.SendMailAsync(mail);
                return new SendMailResponse { Sent = true };
            }
            catch (Exception exception)
            {
                return new SendMailResponse { Sent = false, Error = exception };
            }
        }
    }
}
