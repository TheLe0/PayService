namespace PayService.Contract.Model
{
    public interface ICustomer
    {
        public string Name { get; set; }
        public string State { get; set; }
        public string Cpf { get; set; }
    }
}