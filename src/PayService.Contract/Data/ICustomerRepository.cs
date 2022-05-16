using PayService.Contract.Model;

namespace PayService.Contract.Data
{
    public interface ICustomerRepository
    {
        public Task<ICustomer?> FindByCpf(string cpf);
        public Task<ICustomer?> InsertNewCustomer(ICustomer customer);
        public Task<List<ICustomer>> ListAllCustomers();
    }
}
