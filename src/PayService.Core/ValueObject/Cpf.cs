using PayService.Core.Exception;
using System.Text.RegularExpressions;

namespace PayService.Core.ValueObject
{
    public class Cpf
    {
        private readonly string _cpf;

        public Cpf(string cpf)
        {
            ValidateParameters(cpf);
            _cpf = FormatCpf(cpf);
        }

        private void ValidateParameters(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                throw new DomainException("You must inform a customer cpf!");
            }

            if (FormatCpf(cpf).Length != 11)
            {
                throw new DomainException("You must inform a valid cpf!");
            }
        }

        private string FormatCpf(string cpf)
        {
            return Regex.Replace(cpf, @"[^\d]", "");
        }

        public override string ToString()
        {
            return _cpf;
        }
    }
}
