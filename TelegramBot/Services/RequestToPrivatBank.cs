namespace TelegramBot.Services;

using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Library;
using TelegramBot.Model;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

public class RequestToPrivatBank
{
	private readonly string _date;

	private static HttpClient _httpClient;
	private static string _baseUrl;

	public RequestToPrivatBank(DateOnly date)
	{
		_date = date.ToShortDateString();
	}
	static RequestToPrivatBank()
	{
		_httpClient = new HttpClient();

		IConfiguration config = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.Build();

		_baseUrl = config["URLs:PrivatBank"];
	}

	public async Task<List<CurrencyRate>> GetCurrencyRatesAsync()
	{
		List<CurrencyRate> currencyRates = new List<CurrencyRate>();

		try
		{
			var requiredRate = await _httpClient.GetFromJsonAsync<RequiredRate>(_baseUrl + _date);
			currencyRates = requiredRate?.ExchangeRate.ToList();
		}
		catch (Exception)
		{
		}

		return currencyRates;
	}
}
