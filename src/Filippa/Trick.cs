namespace Filippa;

public abstract class Trick
{
    public abstract Suit? PreviousSuit { get; }

    public Suit? CurrentSuit => PlayedCards.FirstOrDefault()?.Suit; 
    
    public abstract IEnumerable<Card> PlayedCards { get; }

    public abstract void PlayCard(Player player, Card card);
}