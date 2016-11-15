using System;
using System.Threading;

namespace RPSLS
{
    /// <summary>
    /// Retrieves a thread-safe random number.
    /// </summary>
    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random Local;

        /// <summary>
        /// Gets current thread's random.
        /// </summary>
        internal static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }
}