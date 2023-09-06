namespace Ims.SharedCore;

public interface ISystemClock
{
    DateTimeOffset Now { get; }
}
