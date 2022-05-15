using PayService.Core.Exception;
using PayService.Core.ValueObject;
using Xunit;

namespace PayService.Core.Test.ValueObject
{
    public class MonthTest
    {
        [Theory]
        [InlineData("7")]
        [InlineData("07")]
        public void MonthFormatedTest(string month)
        {
            var _month = new Month(month);

            Assert.Equal("07", _month.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData("177")]
        [InlineData("17")]
        public void MonthFormatedInvalidTest(string month)
        {
            Assert.Throws<DomainException>(() => new Month(month));
        }
    }
}
