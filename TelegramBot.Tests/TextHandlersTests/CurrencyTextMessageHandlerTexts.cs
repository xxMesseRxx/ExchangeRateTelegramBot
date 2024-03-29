﻿namespace TelegramBot.Tests.TextHandlersTests;

using TelegramBot.Services.TextHandlers;

[TestClass]
public class CurrencyTextMessageHandlerTexts
{
    [TestMethod]
    public void GetResponseAsync_CorMessage_RequiredMessageExpected()
    {
        //Arrange
        string[] messageWords = new string[] { "USD", "20/04/2023" };
        CurrencyTextMessageHandler handler = new CurrencyTextMessageHandler(messageWords);
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
		string[] messageWords = new string[] { "sd", "USD", "20/04/2023" };
		CurrencyTextMessageHandler handler = new CurrencyTextMessageHandler(messageWords);
        string expectedResponse = "MessageWords are incorrect";

        //Act
        string actual = handler.GetResponseAsync().Result;

        //Assert
        Assert.AreEqual(expectedResponse, actual);
    }
    [TestMethod]
    public void GetResponseAsync_IncorDate_ErrorMessageExpected()
    {
		//Arrange
		string[] messageWords = new string[] { "USD", "20/04/3023" };
		CurrencyTextMessageHandler handler = new CurrencyTextMessageHandler(messageWords);
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
		string[] messageWords = new string[] { "UUU", "20/04/2023" };
		CurrencyTextMessageHandler handler = new CurrencyTextMessageHandler(messageWords);
        string expectedResponse = "Currency type wasn't found";

        //Act
        string actual = handler.GetResponseAsync().Result;

        //Assert
        Assert.AreEqual(expectedResponse, actual);
    }
}
