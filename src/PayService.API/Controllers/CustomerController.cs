using Microsoft.AspNetCore.Mvc;
using PayService.Contract.Service;
using PayService.Core.Exception;
using PayService.Customer.Service;

namespace PayService.API.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _service;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
            _service = new CustomerService();
        }

        [HttpPost]
        [Route("payservice/customer")]
        public async Task<IActionResult> CreateCustomer(string name, string state, string cpf)
        {
            try
            {
                var customer = await _service.CreateCustomer(name, state, cpf);

                if (customer == null)
                {
                    return StatusCode(500, $"Error: Internal server error!");
                }

                return Ok(customer);
            }
            catch (DomainException exc)
            {
                return StatusCode(400, $"Error: {exc.Message}");
            }
        }

        [HttpGet]
        [Route("payservice/customer")]
        public async Task<IActionResult> FindCustomerByCpf(string cpf)
        {
            try
            {
                var customer = await _service.FindCustomerByCpf(cpf);

                if (customer == null)
                {
                    return StatusCode(500, $"Error: Internal server error!");
                }

                return Ok(customer);
            }
            catch (DomainException exc)
            {
                return StatusCode(400, $"Error: {exc.Message}");
            }
        }
    }
}