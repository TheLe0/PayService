using PayService.Core.Exception;
using PayService.Core.ValueObject;
using PayService.Contract.Model;

namespace PayService.Charge
{
    public class Charge : ICharge
    {
        public DateTime DueDate {  get; set; }
        public double TotalAmount { get; set; }
        public string Cpf { get; set; }

        public Charge(string cpf, double totalAmount, DateTime dueDate)
        {
            ValidateParameters(cpf, totalAmount, dueDate);

            DueDate = dueDate;
            TotalAmount = totalAmount;
            Cpf = new Cpf(cpf).ToString();
        }

        private void ValidateParameters(string cpf, double totalAmount, DateTime dueDate)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                throw new DomainException("You must inform a valid cpf!");
            }

            if (totalAmount <= 0.00)
            {
                throw new DomainException("You must inform a valid payment amount!");
            }

            if (dueDate <= DateTime.MinValue)
            {
                throw new DomainException("You must inform a valid due date!");
            }

        }
    }
}