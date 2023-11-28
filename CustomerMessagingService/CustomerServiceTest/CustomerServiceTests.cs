using customerService = CustomerService.Services;
using Moq;
using ServiceClient.httpClients;
using CustomerAPI.RequestModels;
using Xunit;

namespace CustomerServiceTest
{
    public class CustomerServiceTests
    {
        #region HasEmailBeenSentAsync Success

        [Theory, MemberData(nameof(CustomerEmailScheduleDataWithInTimeWindow))]
        public async void HasEmailBeenSentAsync_Should_Send_Email_To_Customer_If_Scheduled_Time_Is_Within_Time_Window(int customerId, string timeOfDay, string email)
        {
            var mockMessagingClient = new Mock<IMessagingClient>();
            mockMessagingClient.Setup(
                p => p.SendEmail(
                    email,
                    DomainModel.Enums.UserTypes.Customer,
                    DomainModel.Enums.NotificationTypes.Information))
                .ReturnsAsync(true);
            var customerService = new customerService.CustomerService(mockMessagingClient.Object);

            var actualResult = await customerService.HasEmailBeenSent(
                new HasEmailBeenSentRequest { CustomerId = customerId, TimeOfDay = timeOfDay });

            Assert.True(actualResult);
        }

        public static IEnumerable<object[]> CustomerEmailScheduleDataWithInTimeWindow =>
        new List<object[]>
        {
            new object[] { 1, 5 , "john@yahoo.com" },
            new object[] { 1, 8, "john@yahoo.com" },
            new object[] { 1, 11, "john@yahoo.com" },
            new object[] { 2, 9, "sunu@gmail.com" },
            new object[] { 2, 10, "sunu@gmail.com" },
            new object[] { 2, 13, "sunu@gmail.com" },
        };

        #endregion

        #region HasEmailBeenSentAsync Failure

        [Theory, MemberData(nameof(CustomerEmailScheduleDataOutsideTimeWindow))]
        public async void HasEmailBeenSentAsync_Should_Send_Email_To_Customer_If_Scheduled_Time_Is_Outside_Time_Window(int customerId, string timeOfDay)
        {
            var mockMessagingClient = new Mock<IMessagingClient>();
            var customerService = new customerService.CustomerService(mockMessagingClient.Object);

            var actualResult = await customerService.HasEmailBeenSent(
                new HasEmailBeenSentRequest { CustomerId = customerId, TimeOfDay = timeOfDay });

            Assert.False(actualResult);
        }

        public static IEnumerable<object[]> CustomerEmailScheduleDataOutsideTimeWindow =>
        new List<object[]>
        {
            new object[] { 1, 2 },
            new object[] { 1, 13 },
            new object[] { 1, 4 },
            new object[] { 2, 7 },
            new object[] { 2, 9 },
            new object[] { 2, 17 },
        };

        #endregion
    }
}