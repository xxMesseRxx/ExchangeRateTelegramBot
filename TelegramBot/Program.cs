using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using TelegramBot.Library;
using TelegramBot.Services;

var myBot = new TelegramBotClient("5962347349:AAHOYGIfohV1Rw8AFmnYtkaPvOzLJIQaspg");
myBot.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync);
ExchangeRateService req = new ExchangeRateService(CurrencyType.USD, new DateOnly(2012, 3, 10));
decimal de = req.GetSaleRateNB();
Console.WriteLine(de);

Console.ReadLine();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
	// Only process Message updates: https://core.telegram.org/bots/api#message
	if (update.Message is not { } message)
		return;
	// Only process text messages
	if (message.Text is not { } messageText)
		return;

	var chatId = message.Chat.Id;

	Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

	// Echo received message text
	Message sentMessage = await botClient.SendTextMessageAsync(
		chatId: chatId,
		text: "You said:\n" + messageText);
}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
	var ErrorMessage = exception switch
	{
		ApiRequestException apiRequestException
			=> $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
		_ => exception.ToString()
	};

	Console.WriteLine(ErrorMessage);
	return Task.CompletedTask;
}