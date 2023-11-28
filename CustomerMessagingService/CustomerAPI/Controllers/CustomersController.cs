using CustomerAPI.RequestModels;
using CustomerService.Services;
using DomainModel.Customer;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerService _customerService;

        public CustomersController(
            ICustomerService customerService,
            ILogger<CustomersController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        /// <summary>
        /// Get employees by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Customer Get([FromRoute] int id)
        {
            return _customerService.GetCustomers(id);
        }

        [HttpPost("emailSent")]
        public Task<bool> HasEmailBeenSent([FromBody] HasEmailBeenSentRequest hasEmailBeenSentRequest)
        {
            return _customerService.HasEmailBeenSent(hasEmailBeenSentRequest);
        }
    }
}