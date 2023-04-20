using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Library.Interfaces
{
	public interface ITextMessageHandler
	{
		Task<string> GetResponseAsync();
	}
}
