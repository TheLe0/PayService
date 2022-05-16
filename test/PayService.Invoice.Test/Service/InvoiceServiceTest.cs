using PayService.Contract.Service;
using PayService.Invoice.Service;
using Xunit;

namespace PayService.Invoice.Test.Service
{
    public class InvoiceServiceTest
    {
        private readonly IInvoiceService _repository;

        public InvoiceServiceTest()
        {
            _repository = new InvoiceService();
        }

        [Fact]
        public async Task CalculateInvoiceByStateTest()
        {
            var invoices = await _repository.CalculateByState();

            Assert.True(invoices.Count > 1);
        }
    }
}
