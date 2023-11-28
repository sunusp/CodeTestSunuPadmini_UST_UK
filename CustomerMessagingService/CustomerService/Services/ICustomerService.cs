using CustomerAPI.RequestModels;
using DomainModel.Customer;

namespace CustomerService.Services
{
    public interface ICustomerService
    {
        public Customer GetCustomers(int id);

        public CustomerMessageSchedule GetCustomerMessageSchedules(int id);

        public Task<bool> HasEmailBeenSent(HasEmailBeenSentRequest hasEmailBeenSentRequest);
    }
}