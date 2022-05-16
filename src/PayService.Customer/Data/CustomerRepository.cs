using PayService.Customer;
using System.Text.Json;
using System.Text.Json.Serialization;
using PayService.Contract.Model;
using PayService.Contract.Data;
using PayService.Data.Redis;

namespace PayService.Customer.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RedisClient _client;

        public CustomerRepository()
        {
            _client = new RedisClient(DatabaseType.CUSTOMERS);

        }

        public async Task<List<ICustomer>> ListAllCustomers()
        {
            var list = new List<ICustomer>();

            foreach (var key in _client.Server.Keys((int) DatabaseType.CUSTOMERS, pattern: "*"))
            {
                var result = await _client.Database.StringGetAsync(key);
                var customer = JsonSerializer.Deserialize<Customer>(result);

                if (customer == null)
                {
                    continue;
                }

                list.Add(new Customer(customer.Name, customer.State, key));
            }

            return list;
        }

        public async Task<ICustomer?> FindByCpf(string cpf)
        {
            try
            {
                var result = await _client.Database.StringGetAsync(cpf);

                if (result == string.Empty)
                {
                    return null;
                }

                var customer = JsonSerializer.Deserialize<Customer>(result);

                if (customer == null)
                {
                    return null;
                }

                return new Customer(customer.Name, customer.State, cpf);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<ICustomer?> InsertNewCustomer(ICustomer customer)
        {
            try
            {
                var result = await FindByCpf(customer.Cpf);
                if (result != null)
                {
                    return result;
                }

                await _client.Database.StringSetAsync(customer.Cpf, JsonSerializer.Serialize(customer));

                return customer;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
