﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Library;
using TelegramBot.Library.Interfaces;

namespace TelegramBot.Services.TextHandlers
{
    public class CurrencyTextMessageHandler : ITextMessageHandler
	{
        private string _message;
        private CurrencyType _currencyType;
        private DateOnly _date;

        public CurrencyTextMessageHandler(string message)
        {
            _message = message;
        }

        public async Task<string> GetResponseAsync()
        {
            try
            {
                SetCurrencyAndDate();
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }

            decimal rate;

            ExchangeRateService rateService = new ExchangeRateService(_currencyType, _date);

            try
            {
                rate = await rateService.GetSaleRateNBAsync();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return $"Argument {ex.ParamName} is out of range";
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }

            return $"Currency rate ({_currencyType}) for {_date.ToShortDateString()} date = {rate.ToString()}";
        }

        private void SetCurrencyAndDate()
        {
            List<string> messageParts = SplitMessage();

            if (!Enum.TryParse(messageParts[0].ToUpper(), out _currencyType))
            {
                throw new ArgumentException("Currency type wasn't found");
            }

            if (!DateOnly.TryParse(messageParts[1], new CultureInfo("ru-RU"), DateTimeStyles.None, out _date))
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
