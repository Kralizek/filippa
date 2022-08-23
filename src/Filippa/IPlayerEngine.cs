namespace Filippa;

public interface IPlayerEngine
{
    Player Player { get; }
    
    Card[] PassCards();

    void ReceivePassedCards(Card[] cards);

    Card PlayTrick(Trick trick);
    
    IEnumerable<Card> CurrentCards { get; }
}

public abstract class PlayerEngineBase : IPlayerEngine
{
    private IList<Card> _cards;

    protected PlayerEngineBase(Player player, IReadOnlyList<Card> cards)
    {
        Player = player ?? throw new ArgumentNullException(nameof(player));
        _cards = cards?.ToList() ?? throw new ArgumentNullException(nameof(cards));
    }

    public Player Player { get; }

    protected void AddCard(Card card)
    {
        if (_cards.Count >= 13)
        {
            throw new InvalidOperationException("Cards above limit of 13.");
        }

        _cards.Add(card);

        _cards = _cards.OrderBy(c => c.Suit).ThenBy(c => c.Rank).ToList();
    }

    protected bool TryPickCard(Card card)
    {
        if (!_cards.Contains(card)) return false;

        _cards.Remove(card);

        return true;
    }

    public Card[] PassCards()
    {
        var cardsToPass = SelectCardsToPass();

        foreach (var card in cardsToPass)
        {
            if (!TryPickCard(card))
            {
                throw new ArgumentOutOfRangeException(nameof(card), "Impossible to pick the selected card");
            }
        }

        return cardsToPass;
    }

    protected virtual Card[] SelectCardsToPass() => throw new NotSupportedException();

    public void ReceivePassedCards(Card[] cards)
    {
        if (cards.Length != 3)
        {
            throw new InvalidOperationException("The amount of received cards should be equal to 3.");
        }

        foreach (var card in cards)
        {
            AddCard(card);
        }
    }

    public abstract Card PlayTrick(Trick trick);

    public IEnumerable<Card> CurrentCards => _cards;
}