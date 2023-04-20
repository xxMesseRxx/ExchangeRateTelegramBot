using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Library.Interfaces;

namespace TelegramBot.Services.TextHandlers
{
	public class UnrecognizedTextMessageHandler : ITextMessageHandler
	{
		public Task<string> GetResponseAsync()
		{
			return Task.FromResult("Please enter currency and date or /help");
		}
	}
}
