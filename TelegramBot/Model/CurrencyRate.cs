using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Model
{
	public class CurrencyRate
	{
		public readonly string BaseCurrency;
		public readonly string Currency;
		public readonly decimal SaleRateNB;
		public readonly decimal PurchaseRateNB;
		public readonly decimal? SaleRate;
		public readonly decimal? PurchaseRate;

		public CurrencyRate(string baseCurrency, string currency,
							decimal saleRateNB, decimal purchaseRateNB,
							decimal? saleRate = null, decimal? purchaseRate = null)
		{
			BaseCurrency = baseCurrency;
			Currency = currency;
			SaleRateNB = saleRateNB;
			PurchaseRateNB = purchaseRateNB;
			SaleRate = saleRate;
			PurchaseRate = purchaseRate;
		}
	}
}
