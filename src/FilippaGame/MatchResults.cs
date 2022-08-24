namespace Filippa;

public record MatchResults
{
    public IReadOnlyDictionary<Player, int> Scores { get; private init; } = new Dictionary<Player, int>();
    
    public int StandingPoints { get; private init; }
    
    public static MatchResults operator +(MatchResults match, RoundResults round)
    {
        var dictionary = new Dictionary<Player, int>(match.Scores);

        foreach (var item in round.Scores)
        {
            if (dictionary.ContainsKey(item.Key))
            {
                dictionary[item.Key] += round.Scores[item.Key];
            }
            else
            {
                dictionary.Add(item.Key, round.Scores[item.Key]);
            }
        }

        var standingPoints = match.StandingPoints;

        if (round.PlayersWithNoPoints == 0)
        {
            standingPoints += 36;
        }
        else
        {
            standingPoints = 0;
        }

        return new MatchResults { Scores = dictionary, StandingPoints = standingPoints };
    }
}