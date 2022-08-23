using Filippa;

public class FakePlayer : Player
{
    public FakePlayer(string name) : base(name)
    {
    }

    public override IPlayerEngine PlayHand(IReadOnlyList<Card> cards)
    {
        return new FakePlayerEngine(this);
    }

    private class FakePlayerEngine : IPlayerEngine
    {
        private readonly Player _player;

        public FakePlayerEngine(Player player)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
        }
        
        public Card[] PassCards()
        {
            throw new NotSupportedException();
        }

        public void ReceiveCards(Card[] cards)
        {
            throw new NotSupportedException();
        }

        public void PlayTrick(Trick trick)
        {
            throw new NotImplementedException();
        }
    }
}