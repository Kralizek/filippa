namespace Filippa;

public abstract class Trick
{
    public abstract Suit? PreviousSuit { get; }

    public Suit? CurrentSuit => PlayedCards.FirstOrDefault()?.Card.Suit; 
    
    public abstract IEnumerable<Play> PlayedCards { get; }

    public abstract void PlayCard(Player player, Card card);
}

public record Play(Player Player, Card Card);