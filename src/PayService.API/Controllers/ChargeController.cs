using Microsoft.AspNetCore.Mvc;
using PayService.API.BodyRequests;
using PayService.Contract.Service;
using PayService.Core.Exception;

namespace PayService.API.Controllers
{
    [ApiController]
    public class ChargeController : ControllerBase
    {
        private readonly ILogger<ChargeController> _logger;
        private readonly IChargeService _chargeService;
        private readonly ICustomerService _customerService;

        public ChargeController(ILogger<ChargeController> logger, IChargeService chargeService, ICustomerService customerService)
        {
            _logger = logger;
            _chargeService = chargeService;
            _customerService = customerService;
        }

        [HttpPost]
        [Route("payservice/charge")]
        public async Task<IActionResult> CreateCharge([FromBody] ChargeBodyRequest body)
        {
            try
            {
                var cpf = body.Cpf;
                var totalAmount = body.TotalAmount;
                var dueDate = body.DueDate;

                var result = await _customerService.FindCustomerByCpf(cpf);

                if (result == null)
                {
                    return StatusCode(404, $"Error: Cpf not found on the system!");
                }
                else
                {
                    var charge = await _chargeService.CreateTransaction(cpf, totalAmount, dueDate);

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
        public async Task<IActionResult> ListTransactions(string? cpf, string? month)
        {
            try
            {
                if (cpf != null)
                {
                    var result = await _chargeService.ListTransactionsByCpf(cpf);
                    return Ok(result);
                }
                else if (month != null)
                {
                    var result = await _chargeService.ListTransactionsByMonth(month);
                    return Ok(result);
                }

                return StatusCode(400, $"Error: You must specify a cpf or a due date!");
            }
            catch (DomainException exc)
            {
                return StatusCode(400, $"Error: {exc.Message}");
            }
        }
    }
}
