using Microsoft.AspNetCore.Mvc;
using PayService.API.BodyRequests;
using PayService.Contract.Service;
using PayService.Core.Exception;
using PayService.Customer.Data;

namespace PayService.API.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _service;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [Route("payservice/customer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerBodyRequest body)
        {
            try
            {
                string name = body.Name;
                string state = body.State; 
                string cpf = body.Cpf;

                var customer = await _service.CreateCustomer(name, state, cpf);

                if (customer == null)
                {
                    return NotFound($"Error: Fail during the customer creation, verify the data and try again!");
                }

                return Ok(customer);
            }
            catch (DomainException exc)
            {
                _logger.LogError(exc.Message);
                return BadRequest($"Error: {exc.Message}");
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
                    return NotFound($"Error: Customer not found!");
                }

                return Ok(customer);
            }
            catch (DomainException exc)
            {
                _logger.LogError(exc.Message);
                return BadRequest($"Error: {exc.Message}");
            }
        }
    }
}