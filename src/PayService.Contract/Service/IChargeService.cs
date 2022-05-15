using PayService.Contract.Model;
namespace PayService.Contract.Service
{
    public interface IChargeService
    {
        public Task<ICharge?> CreateTransaction(string cpf, double totalAmount, DateTime dueDate);
        public Task<List<ICharge>> ListTransactionsByCpf(string cpf);
        public Task<List<ICharge>> ListTransactionsByMonth(string month);
    }
}
