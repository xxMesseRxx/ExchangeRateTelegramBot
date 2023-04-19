using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Library;
using TelegramBot.Model;

namespace TelegramBot.Services
{
	public class RequestToPrivatBank : IRequestToPrivatBank
	{
		private readonly string _url;

		private HttpWebRequest _request;
		private HttpWebResponse _response;

		public RequestToPrivatBank(DateOnly date)
		{
			_url = "https://api.privatbank.ua/p24api/exchange_rates?date=" + date.ToShortDateString();
		}

		public List<CurrencyRate> GetCurrencyRates()
		{
			List<CurrencyRate> currencyRates = new List<CurrencyRate>();

			_request = (HttpWebRequest)WebRequest.Create(_url);
			_request.Method = "GET";

			try
			{
				_response = (HttpWebResponse)_request.GetResponse();

				if (_response.StatusCode == HttpStatusCode.OK)
				{
					using (StreamReader reader = new StreamReader(_response.GetResponseStream()))
					{
						string jsonString = reader.ReadToEnd();

						JsonSerializerOptions options = new JsonSerializerOptions();
						options.PropertyNameCaseInsensitive = true;

						var requiredRate = JsonSerializer.Deserialize<RequiredRate>(jsonString, options);
						currencyRates = requiredRate?.ExchangeRate.ToList();
					}
				}
			}
			catch (Exception)
			{
			}

			return currencyRates;
		}
	}
}
