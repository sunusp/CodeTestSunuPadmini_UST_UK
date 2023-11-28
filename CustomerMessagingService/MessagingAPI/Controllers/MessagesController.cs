using DomainModel;
using MessagingService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MessagingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagingService _messagingService;
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(
            IMessagingService messagingService,
            ILogger<MessagesController> logger)
        {
            _messagingService = messagingService;
            _logger = logger;

        }

        /// <summary>
        /// Send Email to receipients
        /// </summary>
        [HttpGet]
        public async Task<bool> SendEmail(string toEmail, Enums.UserTypes userType, Enums.NotificationTypes notificationType)
        {
            _logger.LogInformation($"Sending {notificationType} email to the {userType} address: {toEmail}");

            var sentMailResult = await _messagingService.SendEmail(toEmail, userType, notificationType);

            if (sentMailResult.Sent)
                _logger.LogInformation($"{notificationType} email to the {userType} address: {toEmail} successfully sent");
            else
                _logger.LogInformation($"{notificationType} email to the {userType} address: {toEmail} failed. Errors Occured : {sentMailResult.Error?.Message} /n {sentMailResult.Error?.Source} /n {sentMailResult.Error?.StackTrace}");

            return sentMailResult.Sent;
        }
    }
}