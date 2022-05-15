using Xunit;
using PayService.API.BodyRequests;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using Newtonsoft.Json;

namespace PayService.API.Test.Integration
{
    public class CustomerApiTest
    {
        private readonly HttpClient _client;
        
        public CustomerApiTest()
        {
            var server = new WebApplicationFactory<Program>();
            _client = server.CreateDefaultClient();
        }

        [Fact]
        public async Task GetCustomerByCpfSuccessTest()
        {
            var response = await _client.GetAsync("/payservice/customer?cpf=03661861085");

            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task GetCustomerByCpfNotFoundTest()
        {
            var response = await _client.GetAsync("/payservice/customer?cpf=99999999999");

            Assert.Equal("NotFound", response.StatusCode.ToString());
        }

        [Fact]
        public async Task GetCustomerByCpfWrongFormatTest()
        {
            var response = await _client.GetAsync("/payservice/customer?cpf=abcdefghijk");

            Assert.Equal("BadRequest", response.StatusCode.ToString());
        }

        [Fact]
        public async Task PostCreateCustomerSuccessTest()
        {
            var body = new CustomerBodyRequest
            {
                Name = "Leonardo",
                State = "RS",
                Cpf = "036.618.610-85"
            };

            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/payservice/customer", data);

            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task PostCreateCustomerFailMissingNameTest()
        {
            var body = new CustomerBodyRequest
            {
                Name = "",
                State = "RS",
                Cpf = "036.618.610-85"
            };

            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/payservice/customer", data);

            Assert.Equal("BadRequest", response.StatusCode.ToString());
        }

        [Fact]
        public async Task PostCreateCustomerFailMissingStateTest()
        {
            var body = new CustomerBodyRequest
            {
                Name = "Leonardo",
                State = "",
                Cpf = "036.618.610-85"
            };

            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/payservice/customer", data);

            Assert.Equal("BadRequest", response.StatusCode.ToString());
        }

        [Fact]
        public async Task PostCreateCustomerFailMissingCpfTest()
        {
            var body = new CustomerBodyRequest
            {
                Name = "Leonardo",
                State = "RS",
                Cpf = ""
            };

            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/payservice/customer", data);

            Assert.Equal("BadRequest", response.StatusCode.ToString());
        }

        [Fact]
        public async Task PostCreateCustomerFailWrongCpfTest()
        {
            var body = new CustomerBodyRequest
            {
                Name = "Leonardo",
                State = "RS",
                Cpf = "ABC-DEF-GHI-JK"
            };

            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/payservice/customer", data);

            Assert.Equal("BadRequest", response.StatusCode.ToString());
        }
    }
}
