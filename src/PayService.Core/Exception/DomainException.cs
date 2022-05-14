
namespace PayService.Core.Exception
{
    public class DomainException : System.Exception
    {
        public DomainException() { }

        public DomainException(string message) : base(message){ }
    }
}