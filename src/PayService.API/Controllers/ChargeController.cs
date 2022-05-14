using Microsoft.AspNetCore.Mvc;
using PayService.Contract.Service;
using PayService.Core.Exception;
using PayService.Charge.Service;
using PayService.Customer.Service;

namespace PayService.API.Controllers
{
    [ApiController]
    public class ChargeController : ControllerBase
    {
        private readonly ILogger<ChargeController> _logger;
        private readonly IChargeService _service;

        public ChargeController(ILogger<ChargeController> logger)
        {
            _logger = logger;
            _service = new ChargeService();
        }

        [HttpPost]
        [Route("payservice/charge")]
        public async Task<IActionResult> CreateCharge(string cpf, double totalAmount, DateTime dueDate)
        {
            try
            {
                var customerService = new CustomerService();

                var result = await customerService.FindCustomerByCpf(cpf);

                if (result == null)
                {
                    return StatusCode(400, $"Error: Cpf not found on the system!");
                }
                else
                {
                    var charge = await _service.CreateTransaction(cpf, totalAmount, dueDate);

                    if (charge == null)
                    {
                        return StatusCode(500, $"Error: Internal server error!");
                    }

                    return Ok(charge);
                }
            }
            catch (DomainException exc)
            {
                return StatusCode(400, $"Error: {exc.Message}");
            }
        }

        [HttpGet]
        [Route("payservice/charge")]
        public async Task<IActionResult> ListTransactions(string? cpf, DateTime? dueDate)
        {
            if (cpf != null)
            {
                var result = await _service.ListTransactions(cpf);
                return Ok(result);
            }
            else if (dueDate != null)
            {
                var result = await _service.ListTransactions((DateTime)dueDate);
                return Ok(result);
            }

            return StatusCode(400, $"Error: You must specify a cpf or a due date!");
        }
    }
}
