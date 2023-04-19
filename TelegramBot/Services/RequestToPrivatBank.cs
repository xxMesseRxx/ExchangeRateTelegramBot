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
			_request = (HttpWebRequest)WebRequest.Create(_url);
			_request.Method = "GET";

			_response = (HttpWebResponse)_request.GetResponse();
			if (_response.StatusCode == HttpStatusCode.OK)
			{
				using (StreamReader reader = new StreamReader(_response.GetResponseStream()))
				{
					string str = reader.ReadToEnd();
					JsonSerializerOptions options = new JsonSerializerOptions();
					options.PropertyNameCaseInsensitive = true;
					options.IncludeFields = true;
					RequiredRate? req = JsonSerializer.Deserialize<RequiredRate>(str, options);

					//List<CurrencyRate> cur = new List<CurrencyRate>
					//{
					//	new CurrencyRate(baseCurrency: "fbdfh", currency: "sdfg",
					//					 saleRateNB: 22, purchaseRateNB: 5235)
					//};
					//RequiredRate rate = new RequiredRate(date: "sdsd", bank: "234sdf",
					//									 baseCurrency: 22, baseCurrencyLit: "SDs",
					//									 exchangeRate: cur);

					//string sff = JsonSerializer.Serialize(rate);
				}
			}

			throw new NotImplementedException();
		}
	}
}
