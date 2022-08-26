namespace Filippa;

internal class TrickImpl : Trick
{
    private readonly IList<Play> _playedCards = new List<Play>();

    public TrickImpl(Suit? previousSuit)
    {
        PreviousSuit = previousSuit;
    }
    
    public override Suit? PreviousSuit { get; }

    public override IEnumerable<Play> PlayedCards => _playedCards;

    public IEnumerable<Card> GetPlayerCards(Player player) => _playedCards.Where(p => p.Player == player).Select(c => c.Card);

    public override void PlayCard(Player player, Card card)
    {
        _playedCards.Add(new Play(player, card));
    }

    public Play GetWinner() => _playedCards.Where(c => c.Card.Suit == CurrentSuit).OrderByDescending(c => c.Card.Rank).First();
}