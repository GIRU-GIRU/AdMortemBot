using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AdMortemBot
{
    public class BotSettings
    {
        [JsonProperty]
        public int MessageCacheSize { get; init; }

        [JsonProperty]
        public ulong GuildID { get; init; }

        [JsonProperty]
        public string BotToken { get; init; }

        [JsonProperty]
        public ulong LogChannelID { get; init; }

        [JsonProperty]
        public ulong MainChannelID { get; init; }


    }
}
