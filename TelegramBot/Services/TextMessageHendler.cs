using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Library;

namespace TelegramBot.Services
{
	public class TextMessageHendler
	{
		private string _message;
		private CurrencyType _currencyType;
		private DateOnly _date;

		public TextMessageHendler(string message)
		{ 
			_message = message;
		}

		private string GetResponse()
		{
			throw new NotImplementedException();
		}

		private void SetCurrencyAndDate()
		{
			List<string> messageParts = SplitMessage();

			if (!(Enum.TryParse<CurrencyType>(messageParts[0], out _currencyType)))
			{
				throw new ArgumentException("Currency type wasn't found");
			}

			if (!(DateOnly.TryParse(messageParts[1], out _date)))
			{
				throw new ArgumentException("Date is incorrect");
			}	
		}
		private List<string> SplitMessage()
		{
			List<string> messageParts = new List<string>(_message.Split(' '));

			if (messageParts.Count != 2)
			{
				throw new ArgumentException("Message is incorrect");
			}

			return messageParts;
		}
	}
}
