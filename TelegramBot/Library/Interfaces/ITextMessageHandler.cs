namespace TelegramBot.Library.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ITextMessageHandler
{
	Task<string> GetResponseAsync();
}
