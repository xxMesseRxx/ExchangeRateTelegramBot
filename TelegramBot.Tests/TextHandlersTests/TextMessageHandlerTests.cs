namespace TelegramBot.Tests.TextHandlersTests;

using TelegramBot.Services.TextHandlers;

[TestClass]
public class TextMessageHandlerTests
{
	[TestMethod]
	public void GetResponseAsync_StartMessage_WelcomeMessageExpected()
	{
		//Arrange
		TextMessageHandler handler = new TextMessageHandler("/start");
		string expectedResponse = "Hello, i'm bot which can show exchange rate from selected currency to UAH on selected date, " +
									"enter currency and date like this formats: 'USD 21.01.2021' or 'EUR 21/01/2021'";

		//Act
		string actual = handler.GetResponseAsync().Result;

		//Assert
		Assert.AreEqual(expectedResponse, actual);
	}
	[TestMethod]
	public void GetResponseAsync_HelpMessage_HelpMessageExpected()
	{
		//Arrange
		TextMessageHandler handler = new TextMessageHandler("/help");
		string expectedResponse = "Enter currency and date like this formats: 'USD 21.01.2021' or 'EUR 21/01/2021'";

		//Act
		string actual = handler.GetResponseAsync().Result;

		//Assert
		Assert.AreEqual(expectedResponse, actual);
	}
	[TestMethod]
	public void GetResponseAsync_UnexistCommand_ErrorMessageExpected()
	{
		//Arrange
		TextMessageHandler handler = new TextMessageHandler("/baddffg");
		string expectedResponse = "Command isn't exist";

		//Act
		string actual = handler.GetResponseAsync().Result;

		//Assert
		Assert.AreEqual(expectedResponse, actual);
	}
	[TestMethod]
	public void GetResponseAsync_CorMessageWithCurRateAndDate_RequiredMessageExpected()
	{
		//Arrange
		TextMessageHandler handler = new TextMessageHandler("USD 20/04/2023");
		string expectedResponse = "Currency rate (USD) for 20.04.2023 date = 36,5686000";

		//Act
		string actual = handler.GetResponseAsync().Result;

		//Assert
		Assert.AreEqual(expectedResponse, actual);
	}
	[TestMethod]
	public void GetResponseAsync_IncorMessage_ErrorMessageExpected()
	{
		//Arrange
		TextMessageHandler handler = new TextMessageHandler("sd USD 20/04/2023");
		string expectedResponse = "Please enter currency and date or /help";

		//Act
		string actual = handler.GetResponseAsync().Result;

		//Assert
		Assert.AreEqual(expectedResponse, actual);
	}
	[TestMethod]
	public void GetResponseAsync_IncorDate_ErrorMessageExpected()
	{
		//Arrange
		TextMessageHandler handler = new TextMessageHandler("USD 20/04/3023");
		string expectedResponse = "Argument date is out of range";

		//Act
		string actual = handler.GetResponseAsync().Result;

		//Assert
		Assert.AreEqual(expectedResponse, actual);
	}
	[TestMethod]
	public void GetResponseAsync_UnexistCurrencyType_ErrorMessageExpected()
	{
		//Arrange
		TextMessageHandler handler = new TextMessageHandler("UUU 20/04/3023");
		string expectedResponse = "Currency type wasn't found";

		//Act
		string actual = handler.GetResponseAsync().Result;

		//Assert
		Assert.AreEqual(expectedResponse, actual);
	}
}
