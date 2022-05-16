using PayService.Customer.Data;
using PayService.Contract.Data;
using PayService.Contract.Service;
using PayService.Contract.Model;
using PayService.Invoice.Model;

namespace PayService.Invoice.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ICustomerRepository _repository;

        public InvoiceService()
        {
            _repository = new CustomerRepository();
        }

        public async Task<List<IInvoiceByState>> CalculateByState()
        {
            var invoices = new List<IInvoiceByState>();
            var list = new Dictionary<string, double>();

            var customers = await _repository.ListAllCustomers();

            foreach(var customer in customers)
            {
                if (list.ContainsKey(customer.State))
                {
                    list[customer.State] += customer.CalculateCost();
                }
                else
                {
                    list.Add(customer.State, customer.CalculateCost());
                }
            }

            foreach (KeyValuePair<string, double> row in list)
            {
                invoices.Add(new InvoiceByState(row.Key, row.Value));
            }

            return invoices;
        }
    }
}
