namespace Filippa;

public class Deck
{
    private static readonly Random Random = new Random();

    private Card[] _cards = CreateDeck().ToArray();
    
    private static IEnumerable<Card> CreateDeck()
    {
        for (var i = 0; i < 4; i++)
        {
            var suit = (Suit)i;

            for (var j = 2; j <= 14; j++)
            {
                var rank = (Rank)j;

                yield return new Card(suit, rank);
            }
        }
    }

    public IReadOnlyList<Card> Cards => _cards;

    public void Shuffle(int passes = 1)
    {
        for (var i = 0; i < passes; i++)
        {
            ShuffleImpl(ref _cards);
        }

        static void ShuffleImpl(ref Card[] cards)
        {
            var n = cards.Length;

            while (n > 1)
            {
                n--;

                var k = Random.Next(n + 1);

                (cards[k], cards[n]) = (cards[n], cards[k]);
            }
        }
    }
}