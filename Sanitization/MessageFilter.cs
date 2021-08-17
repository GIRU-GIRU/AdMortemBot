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
