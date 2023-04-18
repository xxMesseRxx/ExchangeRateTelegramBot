using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Library
{
	public interface IExchangeRateService
	{
		decimal GetRate(CurrencyType currencyType);
	}
}
