namespace PayService.Contract.Model
{
    public interface ICharge
    {
        public DateTime DueDate { get; set; }
        public double TotalAmount { get; set; }
        public string Cpf { get; set; }
    }
}
