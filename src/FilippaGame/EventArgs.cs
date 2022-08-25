namespace Filippa;


public class RoundStartedEventArgs : EventArgs
{
    public int Round { get; }

    public RoundStartedEventArgs(int round)
    {
        Round = round;
    }
}

public class RoundCompletedEventArgs : EventArgs
{
    public MatchResults MatchResult { get; }

    public RoundResults RoundResult { get; }

    public int CurrentStandingPoints { get; }

    public RoundCompletedEventArgs(MatchResults matchResult, RoundResults roundResult, int currentStandingPoints)
    {
        MatchResult = matchResult;
        RoundResult = roundResult;
        CurrentStandingPoints = currentStandingPoints;
    }
}

public class TrickStartedEventArgs : EventArgs
{
    public int Trick { get; }

    public TrickStartedEventArgs(int trick)
    {
        Trick = trick;
    }
}

public class CardPlayedEventArgs : EventArgs
{
    public CardPlayedEventArgs(Player player, Card card)
    {
        Player = player;
        Card = card;
    }

    public Player Player { get; }

    public Card Card { get; }
}

public class MatchCompletedEventArgs : EventArgs
{
    public MatchResults MatchResults { get; }

    public MatchCompletedEventArgs(MatchResults matchResults)
    {
        MatchResults = matchResults;
    }
}