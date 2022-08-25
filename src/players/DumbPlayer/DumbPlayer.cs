namespace Filippa;
using static Helpers;

public class DumbPlayer : Player
{
    public DumbPlayer(string name) : base(name)
    {
    }

    public override IPlayerEngine PlayHand(IReadOnlyList<Card> cards)
    {
        return new DumbPlayerEngine(this, cards);
    }

    private class DumbPlayerEngine : PlayerEngineBase
    {
        public DumbPlayerEngine(Player player, IReadOnlyList<Card> cards) : base(player, cards)
        {
        }

        public override Card PlayTrick(Trick trick)
        {
            if (!CurrentCards.Any())
            {
                throw new InvalidOperationException("No cards left to play");
            }
            
            if (trick.PlayedCards.Any())
            {
                var playingSuit = trick.CurrentSuit!.Value;
                
                var currentSuit = playingSuit;

                while (true)
                {
                    var suit = CurrentCards.Where(c => c.Suit == currentSuit);

                    var cardToPlay = currentSuit == playingSuit ? suit.MinBy(c => c.Rank) : suit.MaxBy(c => c.Rank);
                    
                    if (cardToPlay is not null && TryPickCard(cardToPlay))
                    {
                        trick.PlayCard(Player, cardToPlay);

                        return cardToPlay;
                    }

                    currentSuit = GetNextSuit(currentSuit);
                }
            }
            else
            {
                // No card was played.

                var previousSuit = trick.PreviousSuit ?? GetRandomSuit();

                while (true)
                {
                    var currentSuit = GetNextSuit(previousSuit);

                    var cardToPlay = CurrentCards.Where(c => c.Suit == currentSuit).MinBy(c => c.Rank);

                    if (cardToPlay is not null && TryPickCard(cardToPlay))
                    {
                        trick.PlayCard(Player, cardToPlay);

                        return cardToPlay;
                    }

                    previousSuit = currentSuit;
                }
            }
        }

        protected override Card[] SelectCardsToPass() => CurrentCards
            .OrderByDescending(c => c.Value)
            .ThenByDescending(c => c.Rank)
            .ThenBy(c => c.Suit)
            .Take(3)
            .ToArray();
    }
}