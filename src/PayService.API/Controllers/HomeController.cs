using Microsoft.AspNetCore.Mvc;
using PayService.API.BodyRequests;
using PayService.Core.Exception;

namespace PayService.API.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
        public HomeController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Welcome()
        {
            try
            {
                return Ok("Welcome to the Pay Service API!");
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return BadRequest($"Error: {exc.Message}");
            }
        }
    }
}