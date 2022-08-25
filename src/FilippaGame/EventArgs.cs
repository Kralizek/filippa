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

public class TrickCompletedEventArgs : EventArgs
{
    public Suit? Suit { get; }

    public Player Winner { get; }

    public Card WinningCard { get; }

    public int TotalValue { get; }

    public TrickCompletedEventArgs(Suit? suit, Player winner, Card winningCard, int totalValue)
    {
        Suit = suit;
        Winner = winner;
        WinningCard = winningCard;
        TotalValue = totalValue;
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

    public int Round { get; }

    public MatchCompletedEventArgs(MatchResults matchResults, int round)
    {
        MatchResults = matchResults;
        Round = round;
    }
}

public class CardsReceivedEventArgs : EventArgs
{
    public Player Receiver { get; }

    public Card[] Cards { get; }

    public Player Sender { get; }

    public CardsReceivedEventArgs(Player receiver, Card[] cards, Player sender)
    {
        Receiver = receiver;
        Cards = cards;
        Sender = sender;
    }
}