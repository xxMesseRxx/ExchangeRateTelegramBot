using TelegramBot.Model;
using TelegramBot.Services;

namespace TelegramBot.Tests
{
	[TestClass]
	public class RequestToPrivatBankTests
	{
		[TestMethod]
		public void GetCurrencyRatesAsync_DateWithExistData_26CurrencyRateExpected()
		{
			//Arrange
			RequestToPrivatBank request = new RequestToPrivatBank(new DateOnly(2023, 04, 20));
			int expectedCurrencyRate = 26;

			//Act
			int actual = request.GetCurrencyRatesAsync().Result.Count();

			//Assert
			Assert.AreEqual(expectedCurrencyRate, actual);
		}
		[TestMethod]
		public void GetCurrencyRatesAsync_UnexistedDate_0CurrencyRateExpected()
		{
			//Arrange
			RequestToPrivatBank request = new RequestToPrivatBank(new DateOnly(3023, 04, 20));
			int expectedCurrencyRate = 0;

			//Act
			int actual = request.GetCurrencyRatesAsync().Result.Count();

			//Assert
			Assert.AreEqual(expectedCurrencyRate, actual);
		}
	}
}