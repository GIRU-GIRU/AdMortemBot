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
                await Logger.LogMessageAsync(
                    new LogMessage(LogSeverity.Critical, Logger.GetAsyncMethodName(), ex.Message, ex));
            }
        }

        internal async Task PerformDisconnectCleanup()
        {
            try
            {

            }
            catch (Exception ex)
            {
                await Logger.LogMessageAsync(
                                    new LogMessage(LogSeverity.Critical, Logger.GetAsyncMethodName(), ex.Message, ex));
            }
        

        }
    }
}
