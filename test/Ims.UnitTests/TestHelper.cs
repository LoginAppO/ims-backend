using AutoFixture;

namespace Ims.UnitTests;

public class TestHelper
{
    public static T GetRandom<T>()
    {
        return new Fixture().Create<T>();
    }

    public static IEnumerable<T> GetRandomList<T>()
    {
        return new Fixture().CreateMany<T>();
    }
}
