namespace TelegramBot.Services.TextHandlers;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TelegramBot.Library.Interfaces;

public class TextMessageHandler : ITextMessageHandler
{
    private string _message;
    private string _response;
    private ITextMessageHandler _messageHandler;

    public TextMessageHandler(string message)
    {
        _message = message;
    }

    public async Task<string> GetResponseAsync()
    {
        ChooseAnswerService();
        _response = await _messageHandler.GetResponseAsync();

		return _response;
    }

    private void ChooseAnswerService()
    {
		RemoveExtraSpaces();
        string[] messageWords = _message.Split(' ');

		if (messageWords.Count() == 1)
        {
            _messageHandler = new CommandTextMessageHandler(_message);
		}
        else if (messageWords.Count() == 2)
        {
			_messageHandler = new CurrencyTextMessageHandler(messageWords);        
        }
        else
        {
            _messageHandler = new UnrecognizedTextMessageHandler();
        }
	}
    private void RemoveExtraSpaces()
    {
        _message.Trim();

        Regex spacesRegex = new Regex(@"\s+");
        _message = spacesRegex.Replace(_message, " ");
    }
}
