using System.Text.Json;
using PayService.Core.ValueObject;
using PayService.Contract.Data;
using PayService.Contract.Model;
using PayService.Data.Redis;

namespace PayService.Charge.Data
{
    public class ChargeRepository : IChargeRepository
    {
        private readonly RedisClient _clientTransactionsByCpf;
        private readonly RedisClient _clientTransactionsByMonth;

        public ChargeRepository()
        {
            _clientTransactionsByCpf = new RedisClient(DatabaseType.TRANSACTIONS_BY_CPF);
            _clientTransactionsByMonth = new RedisClient(DatabaseType.TRANSACTIONS_BY_MONTH);
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

        public async Task<List<ICharge>> ListTransactionsByMonth(string month)
        {
            var list = new List<ICharge>();

            try
            {
                var result = await _clientTransactionsByMonth.Database.StringGetAsync(month);

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

        public async Task<ICharge?> InsertNewTransactionByMonth(ICharge transaction)
        {
            var month = new Month(transaction.DueDate.Month.ToString());

            var listTransacions = await ListTransactionsByMonth(month.ToString());

            listTransacions.Add(transaction);

            await _clientTransactionsByMonth.Database.StringSetAsync(month.ToString(), JsonSerializer.Serialize(listTransacions));

            return transaction;
        }
    }
}
