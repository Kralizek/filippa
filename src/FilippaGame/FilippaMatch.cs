namespace Filippa;

public class FilippaMatch
{
    private readonly Deck _deck;
    private readonly IList<Player> _players = new List<Player>();

    public FilippaMatch(Deck deck)
    {
        _deck = deck;
    }

    public bool PassCards { get; init; } = false;

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

        return new MatchResults();
    }
}

public record MatchResults
{
    
}