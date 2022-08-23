namespace Filippa;

public class FilippaMatch
{
    private readonly IList<Player> _players = new List<Player>();

    public bool PassCards { get; init; } = true;

    public void AddPlayer(Player player)
    {
        if (_players.Count >= 4) throw new InvalidOperationException("You can't add another player to this match");

        _players.Add(player);
    }

    public MatchResults Play(int winningScore = 100)
    {
        if (_players.Count != 4) throw new InvalidOperationException("You can't start a match without 4 players");

        if (PassCards)
        {
            throw new NotSupportedException("Passing cards is not yet supported");
        }

        var result = new MatchResults();

        do
        {
            var playerEngines = _players
                .Zip(Deck.Default.Shuffle(10).Chunk(13))
                .Select(c => c.First.PlayHand(c.Second))
                .ToArray();

            var playerCards = new Dictionary<Player, IList<Card>>(); 

            Suit? previousSuit = default;

            for (var i = 0; i < 13; i++)
            {
                Console.WriteLine($"--- Trick {i+1} ---");
                
                var trick = new TrickImpl(previousSuit); 
                
                foreach (var pe in playerEngines)
                {
                    var playedCard = pe.PlayTrick(trick);

                    if (playedCard.Value == 0)
                    {
                        Console.WriteLine($"{pe.Player.Name} played {playedCard}");
                    }
                    else
                    {
                        Console.WriteLine($"{pe.Player.Name} played {playedCard} for {playedCard.Value} points");
                    }
                }

                var winner = trick.GetWinner();

                foreach (var pe in playerEngines)
                {
                    var cards = trick.GetPlayerCards(pe.Player);

                    if (playerCards.ContainsKey(pe.Player))
                    {
                        foreach (var card in cards)
                        {
                            playerCards[pe.Player].Add(card);
                        }
                    }
                    else
                    {
                        playerCards.Add(pe.Player, new List<Card>(cards));
                    }
                }

                previousSuit = trick.PlayedCards.First().Suit;
            }

            result.Scores = playerCards.ToDictionary(k => k.Key, v => v.Value.Sum(p => p.Value));

        } while (false);

        return result;
    }
}

public record MatchResults
{
    public IReadOnlyDictionary<Player, int> Scores { get; set; }
}

public class TrickImpl : Trick
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