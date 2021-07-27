using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdMortemBot.Reliability
{
    public static class Retrier
    {
        /// <summary> A function that will retry methods that return objects if they return null or false,
        /// else it will just repeat the method as many times as specified
        /// </summary>
        public static T Attempt<T>(Func<T> action, TimeSpan retryInterval, int maxAttemptCount = 3)
        {
            var exceptions = new List<Exception>();

            for (int attempts = 0; attempts < maxAttemptCount; attempts++)
            {
                try
                {
                    T result = action();

                    if (typeof(T) == typeof(bool))
                    {
                        if (Convert.ToBoolean(result))
                        {
                            return result;
                        }
                    }

                    if (result != null)
                    {
                        return result;
                    }

                    Task.Delay(retryInterval);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            throw new AggregateException(exceptions);
        }
    }
}
