using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using TelegramBot.Library;
using TelegramBot.Services;
using TelegramBot.Services.TextHandlers;

var myBot = new TelegramBotClient("5962347349:AAHOYGIfohV1Rw8AFmnYtkaPvOzLJIQaspg");
myBot.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync);

Console.ReadLine();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
	if (update.Message is not { } message)
		return;
	if (message.Text is not { } messageText)
		return;

	TextMessageHandler messageHandler = new TextMessageHandler(messageText);
	string response = await messageHandler.GetResponseAsync();

	var chatId = message.Chat.Id;

	Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

	Message sentMessage = await botClient.SendTextMessageAsync(
		chatId: chatId,
		text: response);
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