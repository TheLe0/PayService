using PayService.Invoice.Model;
using Xunit;

namespace PayService.Invoice.Test.Model
{
    public class InvoiceByStateTest
    {
        [Fact]
        public void InstanceObjectByParametersTest()
        {
            var state = "RS";
            var totalAmount = 145.56;

            var invoice = new InvoiceByState(state, totalAmount);

            Assert.Equal(state, invoice.State);
            Assert.Equal(totalAmount, invoice.TotalAmount);
        }

        [Fact]
        public void InstanceObjectWithNoParametersTest()
        {
            var state = "RS";
            var totalAmount = 145.56;

            var invoice = new InvoiceByState();

            invoice.State = state;
            invoice.TotalAmount = totalAmount;

            Assert.Equal(state, invoice.State);
            Assert.Equal(totalAmount, invoice.TotalAmount);
        }
    }
}
