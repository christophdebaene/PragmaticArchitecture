namespace Bricks.Model
{
    public static class SystemClock
    {
        public static Func<DateTime> GetUtcNow = () => DateTime.UtcNow;
        public static void Reset()
        {
            GetUtcNow = () => DateTime.UtcNow;
        }
    }
}
