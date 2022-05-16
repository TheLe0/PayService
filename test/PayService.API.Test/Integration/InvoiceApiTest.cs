using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using Newtonsoft.Json;

namespace PayService.API.Test.Integration
{
    public class InvoiceApiTest
    {
        private readonly HttpClient _client;

        public InvoiceApiTest()
        {
            var server = new WebApplicationFactory<Program>();
            _client = server.CreateDefaultClient();
        }

        [Fact]
        public async Task CalculateInvoiceByStateSuccessTest()
        {
            var response = await _client.GetAsync("payservice/invoice/state");

            Assert.Equal("OK", response.StatusCode.ToString());
        }
    }
}
