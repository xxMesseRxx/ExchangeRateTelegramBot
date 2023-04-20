
using TelegramBot.Services.TextHandlers;

namespace TelegramBot.Tests.TextHandlersTests
{
	[TestClass]
	public class CommandTextMessageHandlerTests
	{
		[TestMethod]
		public void GetResponseAsync_StartMessage_WelcomeMessageExpected()
		{
			//Arrange
			CommandTextMessageHandler handler = new CommandTextMessageHandler("/start");
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
			CommandTextMessageHandler handler = new CommandTextMessageHandler("/help");
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
			CommandTextMessageHandler handler = new CommandTextMessageHandler("/baddffg");
			string expectedResponse = "Command isn't exist";

			//Act
			string actual = handler.GetResponseAsync().Result;

			//Assert
			Assert.AreEqual(expectedResponse, actual);
		}
	}
}
