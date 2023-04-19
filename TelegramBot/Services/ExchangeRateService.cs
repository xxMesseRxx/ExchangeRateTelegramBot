using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Library;
using TelegramBot.Model;

namespace TelegramBot.Services
{
	public class ExchangeRateService
	{
		private CurrencyType _currencyType;
		private DateOnly _date;
		private CurrencyRate? _requiredCurrencyRate;

		public ExchangeRateService(CurrencyType currencyType, DateOnly date) 
		{ 
			_currencyType = currencyType;
			_date = date;
		}

		public decimal GetSaleRateNB()
		{
			SetRequiredCurrencyRate();

			return _requiredCurrencyRate.SaleRateNB;
		}

		private void SetRequiredCurrencyRate()
		{
			RequestToPrivatBank request = new RequestToPrivatBank(_date);
			List<CurrencyRate> currencyRates = request.GetCurrencyRates();

			if (currencyRates.Count == 0)
			{
				throw new ArgumentOutOfRangeException("date");
			}

			_requiredCurrencyRate = currencyRates.Find(c =>
			{
				Enum.TryParse<CurrencyType>(c.Currency, out CurrencyType cType);
				return cType == _currencyType;
			});

			if (_requiredCurrencyRate is null)
			{
				throw new ArgumentException("Currency type wasn't found");
			}
		}
	}
}
