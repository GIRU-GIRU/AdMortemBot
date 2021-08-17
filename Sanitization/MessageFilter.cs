using AdMortemBot.Logging;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdMortemBot.Sanitization
{
    public static class MessageFilter
    {

        public static string[] profanityArray = new string[] {
            "stupid",
            "idiot",
        };

       


        public async static Task<bool> CheckToFilterMessage(string messageToFilter)
        {
            try
            {
                return await CheckMatchesFilterRegex(messageToFilter) || await CheckProfanity(messageToFilter);
            }
            catch (Exception ex)
            {
                await Logger.LogMessageAsync(new LogMessage(LogSeverity.Error, $"MessageFilter: { Logger.GetAsyncMethodName()}", ex.Message, ex));
            }

            return false;
        }



        public static Task<bool> CheckMatchesFilterRegex(string messageToFilter)
        {
            bool matchesRegex = false;






            return Task.FromResult(matchesRegex);
        }


        public static Task<bool> CheckProfanity(string messageToFilter)
        {
            bool containsProfanity = false;

            foreach (var badWord in profanityArray)
            {
                if (messageToFilter.Contains(badWord))
                {
                    containsProfanity = true;
                    break;
                }
            }

            return Task.FromResult(containsProfanity);
        }
    }
}
