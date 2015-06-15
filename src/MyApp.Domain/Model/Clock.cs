using System;

namespace MyApp.Domain.Model
{
    public class Clock
    {
        public static Func<DateTime> Now = () => DateTime.UtcNow;

        public static void Reset()
        {
            Now = () => DateTime.UtcNow;
        }
    }
}