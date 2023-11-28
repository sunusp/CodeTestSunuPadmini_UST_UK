

using CustomerAPI.RequestModels;
using DomainModel.Customer;
using ServiceClient.httpClients;

namespace CustomerService.Services
{
    public class CustomerService : ICustomerService
    {
        #region Mocked Data
        private static readonly List<Customer> customers = new()
        {
            new Customer() { Id =1, Name ="john", Email = "john@yahoo.com"},
            new Customer() { Id =2, Name ="Sunu", Email = "sunu@gmail.com"}
        };

        private static readonly List<CustomerMessageSchedule> customerMessageSchedules = new()
        {
            new CustomerMessageSchedule() { CustomerId =1, StartsFrom = new TimeSpan(4, 0, 0, 0), EndsOn = new TimeSpan(12, 0, 0, 0)},
            new CustomerMessageSchedule() { CustomerId =2, StartsFrom = new TimeSpan(7, 0, 0, 0), EndsOn = new TimeSpan(14, 0, 0, 0)},

        };
        #endregion

        private readonly IMessagingClient _messagingClient;

        public CustomerService(IMessagingClient messagingClient)
        {
            _messagingClient = messagingClient;
        }

        public Customer GetCustomers(int id)
        {
            return customers.Single(c => c.Id == id);
        }

        public CustomerMessageSchedule GetCustomerMessageSchedules(int id)
        {
            return customerMessageSchedules.Single(c => c.CustomerId == id);
        }

        public async Task<bool> HasEmailBeenSent(HasEmailBeenSentRequest hasEmailBeenSentRequest)
        {
            var customer = GetCustomers(hasEmailBeenSentRequest.CustomerId);
            var customerMessageSchedule = GetCustomerMessageSchedules(hasEmailBeenSentRequest.CustomerId);
            var timeOfDay = TimeSpan.Parse(hasEmailBeenSentRequest.TimeOfDay);

            if ((timeOfDay > customerMessageSchedule.StartsFrom) && (timeOfDay < customerMessageSchedule.EndsOn))
            {
                return await _messagingClient.SendEmail(customer.Email, DomainModel.Enums.UserTypes.Customer, DomainModel.Enums.NotificationTypes.Information);
            }
            else
                return false;
        }


    }
}