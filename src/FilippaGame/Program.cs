using Filippa;

var match = new FilippaMatch();

match.RoundStarted += (_, args) => Console.WriteLine($"--- Round {args.Round} ---");

match.RoundCompleted += (_, args) =>
{
    Console.WriteLine("--- Result of the round ---");

    if (args.RoundResult.Scores.Values.Any(c => c > 0))
    {
        Console.WriteLine($"Round won by: {string.Join(", ", args.RoundResult.Scores.Where(c => c.Value > 0).Select(c => c.Key))}");
    }
    else
    {
        Console.WriteLine("Round with no winners");
    }

    foreach (var item in args.RoundResult.Scores)
    {
        Console.WriteLine($"{item.Key}: {item.Value}");
    }

    Console.WriteLine($"Standing points before the round: {args.CurrentStandingPoints}");
    Console.WriteLine($"Standing points after the round: {args.MatchResult.StandingPoints}");

    Console.WriteLine();
};

match.TrickStarted += (_, args) => Console.WriteLine($"--- Trick {args.Trick} ---");

match.TrickCompleted += (_, args) =>
{
    Console.WriteLine();
    
    Console.WriteLine($"Trick of {args.Suit} won by {args.Winner} with {args.WinningCard} for a total of {args.TotalValue} points");
    
    Console.WriteLine();
};

match.CardPlayed += (_, args) =>
{
    if (args.Card.Value == 0)
    {
        Console.WriteLine($"{args.Player} played {args.Card}");
    }
    else
    {
        Console.WriteLine($"{args.Player} played {args.Card} for {args.Card.Value} points");
    }
};

match.MatchCompleted += (_, args) =>
{
    Console.WriteLine("--- Result of the match ---");

    var winner = args.MatchResults.Scores.MaxBy(c => c.Value).Key;
    
    Console.WriteLine($"Match won by {winner} ({winner.GetType().Name}) in {args.Round} rounds");

    foreach (var item in args.MatchResults.Scores.OrderByDescending(p => p.Value))
    {
        Console.WriteLine($"{item.Key}: {item.Value}");
    }
};

match.CardsReceived += (_, args) =>
{
    Console.WriteLine($"{args.Receiver} received {string.Join(", ", args.Cards.Select(s => s.ToString()))} from {args.Sender}");
};

match.AddPlayer(new DumbPlayer("Player 1"));
match.AddPlayer(new DumbPlayer("Player 2"));
match.AddPlayer(new DumbPlayer("Player 3"));
match.AddPlayer(new DumbPlayer("Player 4"));

match.Play(winningScore: 100);
