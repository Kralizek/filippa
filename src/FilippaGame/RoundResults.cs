namespace Filippa;

public record RoundResults
{
    public RoundResults(MatchResults matchResults, IReadOnlyDictionary<Player, IList<Card>> playerCards)
    {
        IEnumerable<Score> playerScores = playerCards.Select(c => new Score(c.Key, c.Value.Sum(d => d.Value) * -1)).ToArray();

        var playersWithNoPoints = playerScores.Count(i => i.Points == 0);

        if (playersWithNoPoints > 0)
        {
            var pointsPerPlayer = (36 + matchResults.StandingPoints) / playersWithNoPoints;

            playerScores = playerScores.Select(c => c with { Points = c.Points == 0 ? pointsPerPlayer : c.Points });
        }
        
        Scores = playerScores.ToDictionary(k => k.Player, v => v.Points);

        PlayersWithNoPoints = playersWithNoPoints;
    }
    
    public IReadOnlyDictionary<Player, int> Scores { get; }
    
    public int PlayersWithNoPoints { get; }
    
    private record Score(Player Player, int Points);
}