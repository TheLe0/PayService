using PayService.Core.Exception;
using PayService.Customer;
using Xunit;

namespace PayService.Customer.Test.Model
{
    public class CustomerTest
    {
        [Fact]
        public void CustomerEmptyNameThrows()
        {
            Assert.Throws<DomainException>(() => new Customer("", "RS", "03661861085"));
        }

        [Fact]
        public void CustomerEmptyStateThrows()
        {
            Assert.Throws<DomainException>(() => new Customer("Leonardo", "", "03661861085"));
        }

        [Fact]
        public void CustomerEmptyCpfThrows()
        {
            Assert.Throws<DomainException>(() => new Customer("Leonardo", "RS", ""));
        }

        [Fact]
        public void CustomerCpfOutOfRangeThrows()
        {
            Assert.Throws<DomainException>(() => new Customer("Leonardo", "RS", "123"));
        }

        [Fact]
        public void CustomerSetFormatCpf()
        {
            var customer = new Customer("Leonardo", "RS", "036.618.610-85");

            Assert.Equal("03661861085", customer.Cpf);
        }
    }
}
