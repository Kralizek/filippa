namespace Filippa;

public class FilippaMatch
{
    private readonly IList<Player> _players = new List<Player>();

    public bool ShowPlayedCards { get; init; } = true;

    public void AddPlayer(Player player)
    {
        if (_players.Count >= 4) throw new InvalidOperationException("You can't add another player to this match");

        _players.Add(player);
    }

    public MatchResults Play(int winningScore = 100)
    {
        if (_players.Count != 4) throw new InvalidOperationException("You can't start a match without 4 players");

        var result = new MatchResults();

        var round = 0;

        do
        {
            round++;

            var currentStandingPoints = result.StandingPoints;

            Console.WriteLine($"--- Round {round} ---");

            var playerEngines = _players
                .Zip(Deck.Default.Shuffle(10).Chunk(13))
                .Select(c => c.First.PlayHand(c.Second))
                .ToArray();

            var loop = playerEngines.ToLoop();

            playerEngines
                .Select(pe => new { CardsToPass = pe.PassCards() })
                .Zip(loop.Skip(1).Take(4))
                .ToList()
                .ForEach(zip => zip.Second.ReceivePassedCards(zip.First.CardsToPass));
            
            var playerCards = new Dictionary<Player, IList<Card>>();

            Suit? previousSuit = default;

            var firstPlayer = playerEngines.PickAny();

            for (var i = 0; i < 13; i++)
            {
                var trick = new TrickImpl(previousSuit);

                foreach (var pe in loop.SkipWhile(p => p != firstPlayer).Take(4))
                {
                    var playedCard = pe.PlayTrick(trick);

                    if (ShowPlayedCards)
                    {
                        Console.WriteLine($"--- Trick {i + 1} ---");
                        
                        if (playedCard.Value == 0)
                        {
                            Console.WriteLine($"{pe.Player.Name} played {playedCard}");
                        }
                        else
                        {
                            Console.WriteLine($"{pe.Player.Name} played {playedCard} for {playedCard.Value} points");
                        }
                    }
                }

                var winner = trick.GetWinner();

                firstPlayer = playerEngines.Single(pe => pe.Player == winner.Item1);

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

            var roundResults = new RoundResults(result, playerCards);

            result += roundResults;

            Console.WriteLine("--- Result of the round ---");

            foreach (var item in roundResults.Scores)
            {
                Console.WriteLine($"{item.Key.Name}: {item.Value}");
            }

            Console.WriteLine($"Standing points before the round: {currentStandingPoints}");
            Console.WriteLine($"Standing points after the round: {result.StandingPoints}");

            Console.WriteLine();

        } while (result.Scores.Values.All(p => p < winningScore));

        return result;
    }
}