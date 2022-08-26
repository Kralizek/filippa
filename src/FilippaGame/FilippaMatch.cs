namespace Filippa;

public class FilippaMatch
{
    private readonly IList<Player> _players = new List<Player>();

    public void AddPlayer(Player player)
    {
        if (_players.Count >= 4) throw new InvalidOperationException("You can't add another player to this match");

        _players.Add(player);
    }

    public void Play(int winningScore = 100)
    {
        if (_players.Count != 4) throw new InvalidOperationException("You can't start a match without 4 players");

        var result = new MatchResults();

        var round = 0;

        do
        {
            round++;

            var currentStandingPoints = result.StandingPoints;
            
            RoundStarted?.Invoke(this, new RoundStartedEventArgs(round));

            var playerEngines = _players
                .Zip(Deck.Default.Shuffle(10).Chunk(13))
                .Select(c => c.First.PlayHand(c.Second))
                .ToArray();

            var loop = playerEngines.ToLoop();

            playerEngines
                .Select(pe => new { Player = pe.Player, CardsToPass = pe.PassCards() })
                .Zip(loop.Skip(1).Take(4))
                .ToList()
                .ForEach(zip =>
                {
                    zip.Second.ReceivePassedCards(zip.First.CardsToPass);
                    CardsReceived?.Invoke(this, new CardsReceivedEventArgs(zip.Second.Player, zip.First.CardsToPass, zip.First.Player));
                });
            
            var playerCards = new Dictionary<Player, IList<Card>>();

            Suit? previousSuit = default;

            var firstPlayer = playerEngines.PickAny();

            for (var i = 0; i < 13; i++)
            {
                var trick = new TrickImpl(previousSuit);

                TrickStarted?.Invoke(this, new TrickStartedEventArgs(i + 1));

                foreach (var pe in loop.SkipWhile(p => p != firstPlayer).Take(4))
                {
                    var playedCard = pe.PlayTrick(trick);

                    CardPlayed?.Invoke(this, new CardPlayedEventArgs(pe.Player, playedCard));
                }

                var winner = trick.GetWinner();

                firstPlayer = playerEngines.Single(pe => pe.Player == winner.Player);

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

                previousSuit = trick.PlayedCards.First().Card.Suit;

                TrickCompleted?.Invoke(this, new TrickCompletedEventArgs(i + 1, previousSuit, winner.Player, winner.Card, trick.PlayedCards));
            }

            var roundResults = new RoundResults(result, playerCards);

            result += roundResults;
            
            RoundCompleted?.Invoke(this, new RoundCompletedEventArgs(result, roundResults, currentStandingPoints));

        } while (result.Scores.Values.All(p => p < winningScore));
        
        MatchCompleted?.Invoke(this, new MatchCompletedEventArgs(result, round));
    }

    public event EventHandler<TrickStartedEventArgs>? TrickStarted;

    public event EventHandler<TrickCompletedEventArgs>? TrickCompleted; 

    public event EventHandler<CardPlayedEventArgs>? CardPlayed;

    public event EventHandler<CardsReceivedEventArgs>? CardsReceived; 

    public event EventHandler<RoundStartedEventArgs>? RoundStarted;

    public event EventHandler<RoundCompletedEventArgs>? RoundCompleted;

    public event EventHandler<MatchCompletedEventArgs>? MatchCompleted;
}
