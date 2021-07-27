using Discord;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdMortemBot.Logging
{
    public class DiscordLogger
    {
        public async void HandleExceptionQuietly(string message, Exception exception)
        {
            try
            {
                var innermostExceptionMessage = Logger.GetInnermostException(exception).Message;

                await Domain.GetLogChannel().SendMessageAsync($"Exception thrown in {message} - {exception.Message}");
            }
            catch (Exception ex)
            {
                LogMessage msg = new LogMessage(LogSeverity.Error, $"{ GetType().FullName }: { Logger.GetAsyncMethodName()}", ex.Message, ex);

                await Logger.LogMessageAsync(msg);
            }
        }


        public async void HandleExceptionPublically(string message, Exception exception, ITextChannel chnl = null)
        {
            try
            {
                var innermostExceptionMessage = Logger.GetInnermostException(exception).Message;

                if (chnl == null)
                {
                    await Domain.GetMainChannel().SendMessageAsync($"Exception thrown in {message} - {exception.Message}");
                }
                else
                {
                    await chnl.SendMessageAsync($"Exception thrown in {message} - {exception.Message}");
                }
            }
            catch (Exception ex)
            {
                LogMessage msg = new LogMessage(LogSeverity.Error, $"{ GetType().FullName }: { Logger.GetAsyncMethodName()}", ex.Message, ex);

                await Logger.LogMessageAsync(msg);
            }
        }



    }
}
