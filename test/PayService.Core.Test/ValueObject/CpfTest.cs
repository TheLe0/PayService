using PayService.Core.Exception;
using PayService.Core.ValueObject;
using Xunit;

namespace PayService.Core.Test.ValueObject
{
    public class CpfTest
    {
        [Theory]
        [InlineData("806.246.150-57")]
        [InlineData("806246150-57")]
        [InlineData("80624615057")]
        public void CpfFormatedTest(string cpf)
        {
            var _cpf = new Cpf(cpf);

            Assert.Equal("80624615057", _cpf.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData("806.246.150-570")]
        [InlineData("8O62A6150S7")]
        public void CpfWrongFormatEntryThrowsTest(string cpf)
        {
            Assert.Throws<DomainException>(() => new Cpf(cpf));
        }
    }
}
