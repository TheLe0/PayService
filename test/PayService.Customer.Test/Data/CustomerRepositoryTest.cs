using PayService.Customer.Data;
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
        public async void InsertNewCustomerTest()
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
        public async void InsertCustomerCpfAlreadyExistsTest()
        {
            var repository = new CustomerRepository();

            var customer = new Customer("Sheldon", "CA", _cpf);
            var result = await repository.InsertNewCustomer(customer);

            Assert.NotNull(result);
            Assert.Equal("63146472074", result!.Cpf);
        }
    }
}
