using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TelegramBot.Library.Interfaces;

namespace TelegramBot.Services.TextHandlers
{
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
            string patternCourrencyRate = @"^\s*[a-z]{3}\s*[0-3]?\d[.\/][0-1]?\d[.\/]\d{4}\s*$";
            string patternCommand = @"^\s*\/[a-z]{1,15}\s*$";

            if (Regex.IsMatch(_message, patternCommand, RegexOptions.IgnoreCase))
            {
				RemoveExtraSpaces();
                _messageHandler = new CommandTextMessageHandler(_message);
			}
            else if (Regex.IsMatch(_message, patternCourrencyRate, RegexOptions.IgnoreCase))
            {
                RemoveExtraSpaces();
				_messageHandler = new CurrencyTextMessageHandler(_message);        
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
}
