using System;
using System.Threading;
using System.Threading.Tasks;
using AdMortemBot;
using AdMortemBot.Logging;
using Discord;

namespace AdMortemBot
{
    class Program
    {
        private static CancellationTokenSource _tokenSource;
        static async Task Main()
        {
            await Logger.InitializeLogger();
            await Logger.LogMessageAsync("Initializing Config");

            Config.InitializeConfig();
            await Logger.LogMessageAsync("Launching Bot");


            await AdMortemBot.LaunchBotAsync();

            _tokenSource = new CancellationTokenSource();


            if (!_tokenSource.IsCancellationRequested)
            {
                await AdMortemBot.RunBotAsync(_tokenSource.Token);
            }
        }


        public static async Task RestartBot()
        {
            try
            {
                _tokenSource.Cancel();
                _tokenSource = null;
                await Main();
            }
            catch (Exception ex)
            {
                LogMessage logMsg = new LogMessage(LogSeverity.Critical, Logger.GetAsyncMethodName(), ex.Message, ex);
                await Logger.LogMessageAsync(logMsg);
            }

        }
    }
}
