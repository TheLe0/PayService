using PayService.Contract.Model;
using PayService.Contract.Service;
using PayService.Charge.Data;
using PayService.Core.ValueObject;

namespace PayService.Charge.Service
{
    public class ChargeService : IChargeService
    {
        private readonly ChargeRepository _repository;

        public ChargeService()
        {
            _repository = new ChargeRepository();
        }

        public async Task<ICharge?> CreateTransaction(string cpf, double totalAmount, DateTime dueDate)
        {
            var transaction = new Charge(cpf, totalAmount, dueDate);
            var result = await _repository.InsertNewTransactionByCpf(transaction);
            await _repository.InsertNewTransactionByMonth(transaction);

            return result;

        }

        public async Task<List<ICharge>> ListTransactionsByCpf(string cpf)
        {
            string _cpf = new Cpf(cpf).ToString();
            var transactions = await _repository.ListTransactionsByCpf(_cpf);
            return transactions;
        }

        public async Task<List<ICharge>> ListTransactionsByMonth(string month)
        {
            string _month = new Month(month).ToString();
            var transactions = await _repository.ListTransactionsByMonth(_month);
            return transactions;
        }
    }
}
