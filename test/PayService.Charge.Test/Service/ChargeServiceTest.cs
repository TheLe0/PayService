using PayService.Charge.Service;
using PayService.Contract.Service;
using PayService.Core.Exception;
using Xunit;

namespace PayService.Charge.Test.Service
{
    public class ChargeServiceTest
    {
        private readonly IChargeService _service;
        private readonly string _cpf;

        public ChargeServiceTest()
        {
            _service = new ChargeService();
            _cpf = "631.464.720-74";
        }

        [Fact]
        public async Task CreateTransactionTest()
        {
            var result = await _service.CreateTransaction(_cpf, 123.87, DateTime.Now);

            Assert.NotNull(result);
            Assert.Equal("63146472074", result!.Cpf);
        }

        [Fact]
        public async Task ListTransactionsByCpfTest()
        {
            var result = await _service.ListTransactionsByCpf(_cpf);

            Assert.True(result.Count >= 1);
        }

        [Theory]
        [InlineData("")]
        [InlineData("806.246.150-570")]
        [InlineData("8O62A6150S7")]
        public async Task ListTransactionsByCpfThrowsTest(string cpf)
        {
            await Assert.ThrowsAsync<DomainException>(async () => await _service.ListTransactionsByCpf(cpf));
        }


        [Fact]
        public async Task ListTransactionsByMonthTest()
        {
            var currentMonth = DateTime.Now.Month.ToString();

            var result = await _service.ListTransactionsByMonth(currentMonth);

            Assert.True(result.Count >= 1);
        }

        [Theory]
        [InlineData("")]
        [InlineData("00")]
        [InlineData("18")]
        public async Task ListTransactionsByMonthThrowsTest(string month)
        {
            await Assert.ThrowsAsync<DomainException>(async () => await _service.ListTransactionsByMonth(month));
        }
    }
}
