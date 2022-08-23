namespace Filippa;

public record Card(Suit Suit, Rank Rank)
{
    public int Value => (Suit, Rank) switch
    {
        (Suit.Hearts, < Rank.Jack) => 1,
        (Suit.Hearts, Rank.Jack) => 2,
        (Suit.Hearts, Rank.Queen) => 3,
        (Suit.Hearts, Rank.King) => 4,
        (Suit.Hearts, Rank.Ace) => 5,
        (Suit.Spades, Rank.Queen) => 13,
        _ => 0
    };

    public override string ToString() => $"{Rank} of {Suit}";
}