using System.ComponentModel;

namespace PayService.API.BodyRequests
{
    public class CustomerBodyRequest
    {
        [DefaultValue("Leonardo")]
        public string Name { get; set; }

        [DefaultValue("RS")]
        public string State { get; set; }

        [DefaultValue("999.999.999-99")]
        public string Cpf { get; set; }
    }
}
