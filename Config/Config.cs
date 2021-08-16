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
        public static BotSettings _botSettings; //TODO make private + getter 
        public static BotSettings InitializeConfig()
        {

            string assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string folder = "Config";
            string fileName = "AdMortemBotConfig.Json";

            string path = Path.Combine(assemblyLocation, folder, fileName);


            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                _botSettings = (BotSettings)serializer.Deserialize(file, typeof(BotSettings));
            }

            return _botSettings;
        }
    }
}
