namespace Ims.SharedCore;

public class SystemClock : ISystemClock
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}
