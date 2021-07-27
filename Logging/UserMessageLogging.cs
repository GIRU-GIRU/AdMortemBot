using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdMortemBot.Logging
{
    class UserMessageLogging
    {
        public static async Task LogDeletedMessage(Discord.Cacheable<Discord.IMessage, ulong> arg1, ISocketMessageChannel arg2)
        {



        }



        public static async Task LogEditedMessage(Discord.Cacheable<Discord.IMessage, ulong> arg1, SocketMessage arg2, ISocketMessageChannel arg3)
        {

        }


        public static async Task LogBulkDelete(IReadOnlyCollection<Discord.Cacheable<Discord.IMessage, ulong>> arg1, ISocketMessageChannel arg2)
        {

        }
    }
}
