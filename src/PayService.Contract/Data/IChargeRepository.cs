using PayService.Contract.Model;

namespace PayService.Contract.Data
{
    public interface IChargeRepository
    {
        public Task<ICharge?> InsertNewTransactionByCpf(ICharge transaction);
        public Task<ICharge?> InsertNewTransactionByDueDate(ICharge transaction);
        public Task<List<ICharge>> ListTransactionsByCpf(string cpf);
        public Task<List<ICharge>> ListTransactionsByDueDate(DateTime dueDate);
    }
}
