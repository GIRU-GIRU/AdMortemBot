using AdMortemBot;
using AdMortemBot.Logging;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdMortemBot
{
    class LoginHandler
    {
        DiscordSocketClient _client;
        public LoginHandler(DiscordSocketClient client)
        {
            _client = client;
        }

        internal async Task PerformReadyProcedures()
        {
            try
            {
                await Domain.InitializeDomain(_client);
                await Domain.GetLogChannel().SendMessageAsync("Bot initialized");
            }
            catch (Exception ex)
            {
                LogMessage logMsg = new LogMessage(LogSeverity.Critical, Logger.GetAsyncMethodName(), ex.Message, ex);
                await Logger.LogMessageAsync(logMsg);
            }
        }

        internal async Task PerformDisconnectCleanup()
        {
            try
            {
                throw new NotImplementedException(); 
            }
            catch (Exception ex)
            {
                LogMessage logMsg = new LogMessage(LogSeverity.Critical, Logger.GetAsyncMethodName(), ex.Message, ex);
                await Logger.LogMessageAsync(logMsg);
            }


        }
    }
}
