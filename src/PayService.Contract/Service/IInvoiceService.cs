
using PayService.Contract.Model;

namespace PayService.Contract.Service
{
    public interface IInvoiceService
    {
        public Task<List<IInvoiceByState>> CalculateByState();
    }
}
