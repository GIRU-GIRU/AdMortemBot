using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdMortemBot
{
    public static class UserSanitizer
    {
        public static async Task Sanitize(SocketGuildUser arg)
        {
            throw new NotImplementedException();
        }

        public static string[] profanityArray = new string[] {
            "stupid",
            "idiot",
        };
        public static bool CheckProfanity(params string[] stringArray)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                for (int j = 0; j < profanityArray.Length; j++)
                {
                    Regex rx = new Regex(@$"{profanityArray[j]}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    MatchCollection matches = rx.Matches(stringArray[i]);
                    if (matches.Count > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
