using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using TelegramBot.Library;
using TelegramBot.Services;
using TelegramBot.Services.TextHandlers;

IConfiguration config = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json")
	.Build();

var myBot = new TelegramBotClient(config["TelegramBot:AccessToken"]);
myBot.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync);
Console.WriteLine($"Hello, I'm {myBot.GetMeAsync().Result.Username}");

Console.ReadLine();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
	if (update.Message is not { } message)
		return;
	if (message.Text is not { } messageText)
		return;

	var chatId = message.Chat.Id;

	Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

	TextMessageHandler messageHandler = new TextMessageHandler(messageText);
	string response = await messageHandler.GetResponseAsync();

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