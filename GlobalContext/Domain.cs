using AdMortemBot;
using AdMortemBot.Logging;
using AdMortemBot.Reliability;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdMortemBot
{
    public static class Domain
    {
        private static SocketGuild _guild;
        private static ITextChannel _logChannel;
        private static ITextChannel _MainChannel;
        private static ConcurrentDictionary<ulong, SocketRole> _guildRoles = new ConcurrentDictionary<ulong, SocketRole>();

        public async static Task InitializeDomain(DiscordSocketClient client)
        {
            try
            {
                await Retrier.Attempt(() => AssignDomainFields(client), TimeSpan.FromSeconds(3), 5);
            }
            catch (Exception ex)
            {
                LogMessage logMsg = new LogMessage(LogSeverity.Critical, MethodBase.GetCurrentMethod().Name, ex.Message, ex);

                await Logger.LogMessageAsync(logMsg);
            }

        }



        private static async Task<bool> AssignDomainFields(DiscordSocketClient client)
        {
            bool assignmentSuccessful = false;

            _guild = client.GetGuild(Config.BotSettings.GuildID);
            _logChannel = _guild.GetChannel(Config.BotSettings.LogChannelID) as ITextChannel;
            _MainChannel = _guild.GetChannel(Config.BotSettings.MainChannelID) as ITextChannel;

            if (_logChannel != null && _guild != null)
            {
                assignmentSuccessful = true;
            }


            return assignmentSuccessful;
        }



        public static ITextChannel GetLogChannel()
        {
            return _logChannel;
        }

        public static ITextChannel GetMainChannel()
        {
            return _MainChannel;
        }

        public static SocketGuild GetGuild()
        {
            return _guild;
        }

    }
}
