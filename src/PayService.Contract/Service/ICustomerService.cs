using PayService.Contract.Model;

namespace PayService.Contract.Service
{
    public interface ICustomerService
    {
        public Task<ICustomer?> CreateCustomer(string name, string state, string cpf);
        public Task<ICustomer?> FindCustomerByCpf(string cpf);
    }
}
