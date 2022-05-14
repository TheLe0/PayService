using PayService.Core.Exception;
using PayService.Core.ValueObject;
using PayService.Contract.Model;

namespace PayService.Customer
{
    public class Customer :ICustomer
    {
        public string Name { get; set; }
        public string State { get; set; }
        public string Cpf { get; set; }

        public Customer(string name, string state, string cpf)
        {
            ValidateParameters(name, state, cpf);
            Name = name;
            State = state;
            Cpf = new Cpf(cpf).ToString();
        }

        private void ValidateParameters(string name, string state, string cpf)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new DomainException("You must inform a customer name!");
            }

            if (string.IsNullOrEmpty(state))
            {
                throw new DomainException("You must inform a customer state!");
            }

            if (string.IsNullOrEmpty(cpf))
            {
                throw new DomainException("You must inform a customer cpf!");
            }
        }

    }
}