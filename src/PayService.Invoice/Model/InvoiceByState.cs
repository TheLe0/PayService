using PayService.Contract.Model;

namespace PayService.Invoice.Model
{
    public class InvoiceByState : IInvoiceByState
    {
        public string State { get; set; }

        public double TotalAmount { get; set; }

        public InvoiceByState()
        {
            State = string.Empty;
        }

        public InvoiceByState(string state, double totalAmount)
        {
            State = state;
            TotalAmount = totalAmount;
        }
    }
}
