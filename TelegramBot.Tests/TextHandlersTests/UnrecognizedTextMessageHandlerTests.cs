﻿namespace TelegramBot.Tests.TextHandlersTests;

using TelegramBot.Services.TextHandlers;

[TestClass]
public class UnrecognizedTextMessageHandlerTests
{
    [TestMethod]
    public void GetResponseAsync_Get_MessageExpected()
    {
        //Arrange
        UnrecognizedTextMessageHandler handler = new UnrecognizedTextMessageHandler();
        string expectedResponse = "Please enter currency and date or /help";

        //Act
        string actual = handler.GetResponseAsync().Result;

        //Assert
        Assert.AreEqual(expectedResponse, actual);
    }

}
