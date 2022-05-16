using PayService.Customer.Data;
using System.Threading.Tasks;
using Xunit;

namespace PayService.Customer.Test.Data
{
    public class CustomerRepositoryTest
    {
        private readonly string _cpf;

        public CustomerRepositoryTest()
        {
            _cpf = "631.464.720-74";
        }

        [Fact]
        public async Task InsertNewCustomerTest()
        {
            var repository = new CustomerRepository();

            string name = "Leonardo";
            string state = "RS";

            var customer = new Customer(name, state, _cpf);
            var result = await repository.InsertNewCustomer(customer);

            Assert.NotNull(result);
            Assert.Equal("63146472074", result!.Cpf);
        }

        [Fact]
        public async Task ListAllCustomersTest()
        {
            var repository = new CustomerRepository();
            var list = await repository.ListAllCustomers();

            Assert.True(list.Count > 0);
        }

        [Fact]
        public async Task InsertCustomerCpfAlreadyExistsTest()
        {
            var repository = new CustomerRepository();

            var customer = new Customer("Sheldon", "CA", _cpf);
            var result = await repository.InsertNewCustomer(customer);

            Assert.NotNull(result);
            Assert.Equal("63146472074", result!.Cpf);
        }

        [Fact]
        public async Task FindByCpfTest()
        {
            var repository = new CustomerRepository();
            var result = await repository.FindByCpf("63146472074");

            Assert.NotNull(result);
            Assert.Equal("63146472074", result!.Cpf);
        }

        [Fact]
        public async Task FindByCpfNotFoundTest()
        {
            var repository = new CustomerRepository();
            var result = await repository.FindByCpf("55208723093");

            Assert.Null(result);
        }
    }
}
