using Microsoft.AspNetCore.Mvc;
using PayService.Contract.Service;
using PayService.Core.Exception;

namespace PayService.API.Controllers
{

    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly ILogger<ChargeController> _logger;
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(ILogger<ChargeController> logger, IInvoiceService invoiceService)
        {
            _logger = logger;
            _invoiceService = invoiceService;
        }

        [HttpGet]
        [Route("payservice/invoice/state")]
        public async Task<IActionResult> CalculateInvoiceByState()
        {
            try
            {
                var result = await _invoiceService.CalculateByState();

                return Ok(result);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return BadRequest(exc.Message);
            }
        }
    }
}
