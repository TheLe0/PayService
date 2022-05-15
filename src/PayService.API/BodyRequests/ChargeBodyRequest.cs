using PayService.Contract.Model;
using System.ComponentModel;

namespace PayService.API.BodyRequests
{
    public class ChargeBodyRequest : ICharge
    {
        [DefaultValue("2022-05-15")]
        public DateTime DueDate { get; set; }

        [DefaultValue(0.00)]
        public double TotalAmount { get; set; }

        [DefaultValue("999.999.999-99")]
        public string Cpf { get; set; }
    }
}
