namespace TelegramBot.Services.TextHandlers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Library.Interfaces;

public class UnrecognizedTextMessageHandler : ITextMessageHandler
{
	public Task<string> GetResponseAsync()
	{
		return Task.FromResult("Please enter currency and date or /help");
	}
}
