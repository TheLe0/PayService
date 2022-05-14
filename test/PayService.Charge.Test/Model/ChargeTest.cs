using PayService.Core.Exception;
using PayService.Charge;
using Xunit;

namespace PayService.Charge.Test.Model
{
    public class ChargeTest
    {
        [Fact]
        public void ChargeEmptyCpfThrows()
        {
            Assert.Throws<DomainException>(() => new Charge("", 123.45, DateTime.Now));
        }

        [Fact]
        public void ChargeInvalidNegativeAmountThrows()
        {
            Assert.Throws<DomainException>(() => new Charge("304.890.880-31", -123.45, DateTime.Now));
        }

        [Fact]
        public void ChargeInvalidNotMonetaryAmountAmountThrows()
        {
            Assert.Throws<DomainException>(() => new Charge("304.890.880-31", 0, DateTime.Now));
        }

        [Fact]
        public void ChargeInvalidDueDateThrows()
        {
            Assert.Throws<DomainException>(() => new Charge("304.890.880-31", 56, DateTime.MinValue));
        }
    }
}
