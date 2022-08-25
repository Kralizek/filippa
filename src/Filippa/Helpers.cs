namespace Filippa;

public static class Helpers
{
    public static Suit GetNextSuit(Suit suit) => (Suit)(((int)suit + 1) % 4);

    private static readonly Random Random = new(DateTimeOffset.UtcNow.Millisecond);
    
    public static Suit GetRandomSuit() => (Suit)Random.Next(0, 4);
}