
namespace PayService.Contract.Model
{
    public interface IInvoiceByState
    {
        public string State { get; set; }

        public double TotalAmount { get; set; }
    }
}
