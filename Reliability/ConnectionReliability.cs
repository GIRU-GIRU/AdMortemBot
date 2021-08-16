using AdMortemBot.Logging;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdMortemBot.Reliability
{
    class ConnectionReliability
    {
        private static readonly TimeSpan _timeout = TimeSpan.FromSeconds(30);

        private static bool _attemptReset;
        private static int _attemptRetries;

        private readonly DiscordSocketClient _client;
        private CancellationTokenSource _cancellationToken;


        public ConnectionReliability(DiscordSocketClient client, bool attemptReset, int attemptRetries)
        {
            _cancellationToken = new CancellationTokenSource();
            _client = client;
        
            _attemptReset = attemptReset;
            _attemptRetries = attemptRetries;

            _client.Connected += ConnectedAsync;
            _client.Disconnected += DisconnectedAsync;
        }

        public async Task ConnectedAsync()
        {
            await Logger.LogMessageAsync("Client successfully reconnected");
            _cancellationToken.Cancel();
            _cancellationToken = new CancellationTokenSource();
        }

        public async Task DisconnectedAsync(Exception _e)
        {
            await Logger.LogMessageAsync("Client disconnected, checking to reconnect");

            if (_attemptReset)
            {
                _ = Task.Delay(_timeout, _cancellationToken.Token).ContinueWith(async _ =>
                {
                    await Logger.LogMessageAsync("Timeout expired, continuing to check client state...");
                    bool success = await CheckStateAsync();

                    if (success)
                    {
                        await Logger.LogMessageAsync("Client reconnected succesfully!");
                    }
                });
            }
        }

        private async Task<bool> CheckStateAsync()
        {
            if (_client.ConnectionState == ConnectionState.Connected) return true;

            if (_attemptReset)
            {
                for (int i = 0; i < _attemptRetries; i++)
                {
                    await Logger.LogMessageAsync("Attempting to restart bot");

                    Task timeout = Task.Delay(_timeout);
                    Task connect = Program.RestartBot();

                    Task task = await Task.WhenAny(timeout, connect);

                    if (task != timeout)
                    {
                        if (connect.IsFaulted)
                        {
                            await Logger.LogMessageAsync("CRITICAL ERROR - " + connect.Exception);
                        }
                        else if (connect.IsCompletedSuccessfully)
                        {                           
                            return true;
                        }
                    }
                }
            }

            await Logger.LogMessageAsync("Client has been disconnected from Discord");
            _cancellationToken.Cancel();
            return false;
        }
    }
}
