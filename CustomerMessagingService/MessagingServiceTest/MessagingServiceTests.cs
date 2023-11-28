using DomainModel;
using MessagingService.Models;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using messagingService = MessagingService.Services;

namespace MessagingServiceTest
{
    public class MessagingServiceTests
    {
        [Fact]
        public async void SendEmail_Should_Not_Send_Email_When_SMTP_Client_Is_Not_Available()
        {
            var smtpClientSettings = new SMTPClientSettings { Host = "myDomain", Port = 25, FromEmail = "info@myDomain.com" };
            var smtpClientSettingsMock = new Mock<IOptions<SMTPClientSettings>>();
            smtpClientSettingsMock.Setup(x => x.Value).Returns(smtpClientSettings);
            var messagingService = new messagingService.MessagingService(smtpClientSettingsMock.Object);

            var actualResult = await messagingService.SendEmail("sunu@gmail.com", Enums.UserTypes.Customer, Enums.NotificationTypes.Information);

            Assert.False(actualResult.Sent);
            Assert.NotNull(actualResult.Error);
        }
    }
}