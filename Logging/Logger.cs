using Discord;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdMortemBot.Logging
{
    public static class Logger
    {
        private static DiscordLogger _DiscordLogger;
        private static DateTime _BotStartTime;
        private static string _LogFileName;
        public static Task InitializeLogger()
        {
            _DiscordLogger = new DiscordLogger();
            _BotStartTime = DateTime.UtcNow;

            string LogDate = DateTime.UtcNow.ToShortDateString().Replace("/", String.Empty);
            string LogTime = DateTime.UtcNow.ToShortTimeString().Replace("/", String.Empty);
            _LogFileName = $"Log {LogDate} {LogTime}";

            using StreamWriter file = new(_LogFileName);
            file.WriteLine("Initializing Ad Mortem bot");

            return Task.CompletedTask;
        }

        public async static Task LogMessageAsync(LogMessage arg)
        {
            //Routing for delegate not wanting optional params on method signature
            await LogMessageAsync(arg, false, null, true);
        }

        public async static Task LogMessageAsync(LogMessage arg, bool publicLogOutput = false, ITextChannel Channel = null, bool logToConsole = false, bool logToDiscord = false)
        {
            try
            {

                if (logToDiscord && AdMortemBot.CheckIfOnline() && arg.Exception != null)
                {

                    if (publicLogOutput)
                    {

                        _DiscordLogger.HandleExceptionPublically(arg.Message, arg.Exception, Channel);


                    }
                    else
                    {
                        _DiscordLogger.HandleExceptionQuietly(arg.Message, arg.Exception);
                    }

                }

                string logMessage = arg.Message;

                if (arg.Exception != null)
                {
                    string exceptionMessage = GetInnermostException(arg.Exception).Message;
                    logMessage = String.Join(logMessage, $"\nEXCEPTION = {exceptionMessage}");
                }

                if (logToConsole)
                {
                    Console.WriteLine($"{DateTime.UtcNow.ToShortDateString()}: {logMessage}");
                }


                await LogMessageToLogFileAsync(logMessage);

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in LogMessageAsync ! {ex.Message}");
            }
        }


        public async static Task LogMessageAsync(string msg)
        {
            try
            {
                string logMsg = $"{DateTime.UtcNow.ToShortDateString()}: {msg}";

                Console.WriteLine(logMsg);
                await LogMessageToLogFileAsync(logMsg);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in LogMessageAsync ! {ex.Message}");
            }
        }

        private async static Task LogMessageToLogFileAsync(string msg)
        {
            try
            {


                using StreamWriter file = new(_LogFileName, append: true);

                await file.WriteLineAsync(msg);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in LogMessageToLogFileAsync ! {ex.Message}");
            }
        }

        public static Exception GetInnermostException(Exception ex)
        {

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            return ex;
        }
        public static string GetAsyncMethodName([CallerMemberName] string name = "unknown") => name;
    }
}
