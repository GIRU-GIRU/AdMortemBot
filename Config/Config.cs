using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using AdMortemBot.Logging;
using Discord;
using Newtonsoft.Json;

namespace AdMortemBot
{
    public static class Config
    {
        public static BotSettings BotSettings;
        public static void InitializeConfig()
        {

            var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var folder = "Config";
            var fileName = "Config.Json";

            var path = Path.Combine(assemblyLocation, folder, fileName);


            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                BotSettings = (BotSettings)serializer.Deserialize(file, typeof(BotSettings));
            }

        }
    }
}
