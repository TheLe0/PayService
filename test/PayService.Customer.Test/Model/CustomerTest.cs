using PayService.Core.Exception;
using PayService.Customer;
using Xunit;

namespace PayService.Customer.Test.Model
{
    public class CustomerTest
    {
        [Fact]
        public void CustomerEmptyNameThrowsTest()
        {
            Assert.Throws<DomainException>(() => new Customer("", "RS", "03661861085"));
        }

        [Fact]
        public void CustomerEmptyStateThrowsTest()
        {
            Assert.Throws<DomainException>(() => new Customer("Leonardo", "", "03661861085"));
        }

        [Fact]
        public void CustomerEmptyCpfThrowsTest()
        {
            Assert.Throws<DomainException>(() => new Customer("Leonardo", "RS", ""));
        }

        [Fact]
        public void CustomerCpfOutOfRangeThrowsTest()
        {
            Assert.Throws<DomainException>(() => new Customer("Leonardo", "RS", "123"));
        }

        [Fact]
        public void CustomerSetFormatCpfTest()
        {
            var customer = new Customer("Leonardo", "RS", "036.618.610-85");

            Assert.Equal("03661861085", customer.Cpf);
        }

        [Fact]
        public void CalculateCostTest()
        {
            var customer = new Customer("Leonardo", "RS", "036.618.610-85");

            Assert.Equal(385.00, customer.CalculateCost());
        }
    }
}
