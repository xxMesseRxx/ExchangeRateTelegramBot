namespace TelegramBot.Services.TextHandlers;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Library;
using TelegramBot.Library.Interfaces;

public class CurrencyTextMessageHandler : ITextMessageHandler
{
    private string[] _messageWords;
    private CurrencyType _currencyType;
    private DateOnly _date;

    public CurrencyTextMessageHandler(string[] messageWords)
    {
		_messageWords = messageWords;
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
        CheckMessageWordsCount();

		if (!Enum.TryParse(_messageWords[0].ToUpper(), out _currencyType))
        {
            throw new ArgumentException("Currency type wasn't found");
        }

        if (!DateOnly.TryParse(_messageWords[1], new CultureInfo("ru-RU"), DateTimeStyles.None, out _date))
        {
            throw new ArgumentException("Date is incorrect");
        }
    }
    private void CheckMessageWordsCount()
    {
        if (_messageWords.Count() != 2)
        {
            throw new ArgumentException("MessageWords are incorrect");
        }
    }
}
