using System;

namespace MyApp.Domain.Model
{
    public interface ISystemClock
    {
        DateTime UtcNow { get; }
    }

    public class SystemClock : ISystemClock
    {
        public DateTime UtcNow
        {
            get
            {                
                return  DateTime.UtcNow;
            }
        }
    }
}