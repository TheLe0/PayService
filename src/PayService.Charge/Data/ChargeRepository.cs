using System.Text.Json;
using PayService.Contract.Data;
using PayService.Contract.Model;
using PayService.Data.Redis;

namespace PayService.Charge.Data
{
    public class ChargeRepository : IChargeRepository
    {
        private readonly RedisClient _clientTransactionsByCpf;
        private readonly RedisClient _clientTransactionsByDueDate;

        public ChargeRepository()
        {
            _clientTransactionsByCpf = new RedisClient(DatabaseType.TRANSACTIONS_BY_CPF);
            _clientTransactionsByDueDate = new RedisClient(DatabaseType.TRANSACTIONS_BY_DATE);
        }

        public async Task<List<ICharge>> ListTransactionsByCpf(string cpf)
        {
            var list = new List<ICharge>();

            try
            {
                var result = await _clientTransactionsByCpf.Database.StringGetAsync(cpf);

                if (result != string.Empty)
                {
                    var listTransactions = JsonSerializer.Deserialize<List<Charge>>(result);

                    if (listTransactions != null)
                    {
                        foreach (ICharge transaction in listTransactions)
                        {
                            list.Add(transaction);
                        }
                    }
                }

            }
            catch (Exception)
            {
                return new List<ICharge>();
            }

            return list;
        }

        public async Task<List<ICharge>> ListTransactionsByDueDate(DateTime dueDate)
        {
            var list = new List<ICharge>();

            try
            {
                var result = await _clientTransactionsByDueDate.Database.StringGetAsync(dueDate.ToString());

                if (result != string.Empty)
                {
                    var listTransactions = JsonSerializer.Deserialize<List<Charge>>(result);

                    if (listTransactions != null)
                    {
                        foreach (ICharge transaction in listTransactions)
                        {
                            list.Add(transaction);
                        }
                    }
                }

            }
            catch (Exception)
            {
                return new List<ICharge>();
            }

            return list;
        }

        public async Task<ICharge?> InsertNewTransactionByCpf(ICharge transaction)
        {
            var listTransacions = await ListTransactionsByCpf(transaction.Cpf);

            listTransacions.Add(transaction);

            var result = await _clientTransactionsByCpf.Database.StringSetAsync(transaction.Cpf, JsonSerializer.Serialize<List<ICharge>>(listTransacions));

            return transaction;
        }

        public async Task<ICharge?> InsertNewTransactionByDueDate(ICharge transaction)
        {
            var listTransacions = await ListTransactionsByDueDate(transaction.DueDate);

            listTransacions.Add(transaction);

            await _clientTransactionsByDueDate.Database.StringSetAsync(transaction.DueDate.ToString(), JsonSerializer.Serialize(listTransacions));

            return transaction;
        }
    }
}
