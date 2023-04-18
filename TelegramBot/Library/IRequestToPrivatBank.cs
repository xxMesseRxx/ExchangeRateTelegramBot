using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Model;

namespace TelegramBot.Library
{
	public interface IRequestToPrivatBank
	{
		List<CurrencyRate> GetCurrencyRates();
	}
}
