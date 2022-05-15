using Xunit;
using PayService.API.BodyRequests;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using Newtonsoft.Json;

namespace PayService.API.Test.Integration
{
    public class ChargeApiTest
    {
        private readonly HttpClient _client;

        public ChargeApiTest()
        {
            var server = new WebApplicationFactory<Program>();
            _client = server.CreateDefaultClient();
        }

        [Fact]
        public async Task PostCreateChargeSuccessTest()
        {
            var body = new ChargeBodyRequest
            {
                Cpf = "036.618.610-85",
                TotalAmount = 56.78,
                DueDate = DateTime.Now
            };

            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/payservice/charge", data);

            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task PostCreateChargeFailCpfNotFoundTest()
        {
            var body = new ChargeBodyRequest
            {
                Cpf = "000.000.000-00",
                TotalAmount = 56.78,
                DueDate = DateTime.Now
            };

            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/payservice/charge", data);

            Assert.Equal("NotFound", response.StatusCode.ToString());
        }

        [Fact]
        public async Task GetListTransactionsByCpfSuccessTest()
        {
            var response = await _client.GetAsync("/payservice/charge?cpf=03661861085");

            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task GetListTransactionsByMonthSuccessTest()
        {
            var response = await _client.GetAsync("/payservice/charge?month=6");

            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task GetListTransactionsNoParametersFailTest()
        {
            var response = await _client.GetAsync("/payservice/charge");

            Assert.Equal("BadRequest", response.StatusCode.ToString());
        }
    }
}
