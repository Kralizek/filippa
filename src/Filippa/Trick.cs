namespace Filippa;

public abstract class Trick
{
    public abstract IEnumerable<Card> GetPlayedCards();

    public abstract void PlayCard(Player player, Card card);
}