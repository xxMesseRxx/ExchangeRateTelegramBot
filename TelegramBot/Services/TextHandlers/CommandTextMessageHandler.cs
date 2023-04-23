namespace TelegramBot.Services.TextHandlers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Library.Interfaces;

public class CommandTextMessageHandler : ITextMessageHandler
{
	private string _message;

	public CommandTextMessageHandler(string message)
	{
		_message = message;
	}

	public Task<string> GetResponseAsync()
	{
		string response = "Command isn't exist";

		switch (_message)
		{
			case "/start":
				response = "Hello, i'm bot which can show exchange rate from selected currency to UAH on selected date, " +
							"enter currency and date like this formats: 'USD 21.01.2021' or 'EUR 21/01/2021'";
				break;
			case "/help":
				response = "Enter currency and date like this formats: 'USD 21.01.2021' or 'EUR 21/01/2021'";
				break;
		}

		return Task.FromResult(response);
	}
}
