using TelegramBot.Library;
using TelegramBot.Services;

namespace TelegramBot.Tests
{
	[TestClass]
	public class ExchangeRateServiceTests
	{
		[TestMethod]
		public void GetSaleRateNBAsync_CorArg_RateForRequiredTypeExpected()
		{
			//Arrange
			ExchangeRateService service = new ExchangeRateService(CurrencyType.USD, new DateOnly(2023, 04, 20));
			decimal expectedRate = 36.5686000m;

			//Act
			decimal actual = service.GetSaleRateNBAsync().Result;

			//Assert
			Assert.AreEqual(expectedRate, actual);
		}
		[TestMethod]
		[ExpectedException(typeof(AggregateException))]
		public void GetSaleRateNBAsync_IncorDate_ArgumentOutOfRangeExceptionExpected()
		{
			//Arrange
			ExchangeRateService service = new ExchangeRateService(CurrencyType.USD, new DateOnly(3023, 04, 20));

			//Act
			decimal actual = service.GetSaleRateNBAsync().Result;
		}
		[TestMethod]
		[ExpectedException(typeof(AggregateException))]
		public void GetSaleRateNBAsync_UnexistCurrencyType_ArgumentOutOfRangeExceptionExpected()
		{
			//Arrange
			ExchangeRateService service = new ExchangeRateService(CurrencyType.RUB, new DateOnly(2023, 04, 20));

			//Act
			decimal actual = service.GetSaleRateNBAsync().Result;
		}
	}
}
