namespace TelegramBot.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Library;
using TelegramBot.Model;

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

	public async Task<decimal> GetSaleRateNBAsync()
	{
		await SetRequiredCurrencyRateAsync();

		return _requiredCurrencyRate.SaleRateNB;
	}

	private async Task SetRequiredCurrencyRateAsync()
	{
		RequestToPrivatBank request = new RequestToPrivatBank(_date);
		List<CurrencyRate> currencyRates = await request.GetCurrencyRatesAsync();

		if (currencyRates.Count == 0)
		{
			throw new ArgumentOutOfRangeException("date");
		}

		_requiredCurrencyRate = currencyRates.Find(c => c.Currency == _currencyType.ToString());

		if (_requiredCurrencyRate is null)
		{
			throw new ArgumentException("Currency type wasn't found");
		}
	}
}
