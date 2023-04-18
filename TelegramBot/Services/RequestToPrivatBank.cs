using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Library;
using TelegramBot.Model;

namespace TelegramBot.Services
{
	public class RequestToPrivatBank : IRequestToPrivatBank
	{
		public List<CurrencyRate> GetCurrencyRates()
		{
			throw new NotImplementedException();
		}
	}
}
