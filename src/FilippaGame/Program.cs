using Filippa;

var match = new FilippaMatch();

match.RoundStarted += (_, args) => Console.WriteLine($"--- Round {args.Round} ---");

match.RoundCompleted += (_, args) =>
{
    Console.WriteLine();
    
    Console.WriteLine("--- Result of the round ---");

    foreach (var item in args.RoundResult.Scores)
    {
        Console.WriteLine($"{item.Key.Name}: {item.Value}");
    }

    Console.WriteLine($"Standing points before the round: {args.CurrentStandingPoints}");
    Console.WriteLine($"Standing points after the round: {args.MatchResult.StandingPoints}");

    Console.WriteLine();
};

match.TrickStarted += (_, args) => Console.WriteLine($"--- Trick {args.Trick} ---");

match.CardPlayed += (_, args) =>
{
    if (args.Card.Value == 0)
    {
        Console.WriteLine($"{args.Player.Name} played {args.Card}");
    }
    else
    {
        Console.WriteLine($"{args.Player.Name} played {args.Card} for {args.Card.Value} points");
    }
};

match.MatchCompleted += (_, args) =>
{
    Console.WriteLine("--- Result of the match ---");

    foreach (var item in args.MatchResults.Scores.OrderByDescending(p => p.Value))
    {
        Console.WriteLine($"{item.Key.Name}: {item.Value}");
    }
};

match.AddPlayer(new DumbPlayer("Player 1"));
match.AddPlayer(new DumbPlayer("Player 2"));
match.AddPlayer(new DumbPlayer("Player 3"));
match.AddPlayer(new DumbPlayer("Player 4"));

match.Play(winningScore: 100);
