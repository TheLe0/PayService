using PayService.Core.ValueObject;
using PayService.Customer.Data;
using PayService.Contract.Model;
using PayService.Contract.Service;

namespace PayService.Customer.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerRepository _repository;

        public CustomerService()
        {
            _repository = new CustomerRepository();
        }

        public async Task<ICustomer?> CreateCustomer(string name, string state, string cpf)
        {
            var _customer = new Customer(name, state, cpf);
            var result = await _repository.InsertNewCustomer(_customer);

            return result;
        }

        public async Task<ICustomer?> FindCustomerByCpf(string cpf)
        {
            var _cpf = new Cpf(cpf);
            var customer = await _repository.FindByCpf(_cpf.ToString());

            return customer;
        }
    }
}
