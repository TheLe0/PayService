using PayService.Customer.Service;
using System.Threading.Tasks;
using Xunit;

namespace PayService.Customer.Test.Service
{
    public class CustomerServiceTest
    {
        private CustomerService _customerService;

        public CustomerServiceTest()
        {
            _customerService = new CustomerService();
        }

        [Fact]
        public async Task CreateCustomerTest()
        {
            var cpf = "631.464.720-74";
            string name = "Leonardo";
            string state = "RS";

            var response = await _customerService.CreateCustomer(name, state, cpf);

            Assert.NotNull(response);
            Assert.Equal("63146472074", response!.Cpf);
        }

        [Theory]
        [InlineData("631.464.720-74")]
        [InlineData("631464720-74")]
        [InlineData("63146472074")]
        public async Task FindCustomerByCpfTest(string cpf)
        {
            var response = await _customerService.FindCustomerByCpf(cpf);

            Assert.NotNull(response);
            Assert.Equal("63146472074", response!.Cpf);
        }

        [Theory]
        [InlineData("552.087.230-93")]
        [InlineData("552087230-93")]
        [InlineData("55208723093")]
        public async Task FindCustomerByCpfNotFoundTest(string cpf)
        {
            var response = await _customerService.FindCustomerByCpf(cpf);

            Assert.Null(response);
        }
    }
}
