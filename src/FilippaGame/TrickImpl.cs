namespace Filippa;

internal class TrickImpl : Trick
{
    private readonly IList<(Player player, Card card)> _playedCards = new List<(Player, Card)>();

    public TrickImpl(Suit? previousSuit)
    {
        PreviousSuit = previousSuit;
    }
    
    public override Suit? PreviousSuit { get; }

    public override IEnumerable<Card> PlayedCards => _playedCards.Select(c => c.card);

    public IEnumerable<Card> GetPlayerCards(Player player) => _playedCards.Where(p => p.player == player).Select(c => c.card);

    public override void PlayCard(Player player, Card card)
    {
        _playedCards.Add((player, card));
    }

    public (Player, Card) GetWinner() => _playedCards.Where(c => c.card.Suit == CurrentSuit).OrderByDescending(c => c.card.Rank).FirstOrDefault();
}