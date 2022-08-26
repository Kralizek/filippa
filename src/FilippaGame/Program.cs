using Filippa;
using Spectre.Console;
using Spectre.Console.Rendering;

var match = new FilippaMatch();

match.RoundStarted += (_, args) => AnsiConsole.MarkupLineInterpolated($"[bold]--- Round {args.Round} ---[/]");

match.RoundCompleted += (_, args) =>
{
    AnsiConsole.MarkupLine("[bold]--- Result of the round ---[/]");

    if (args.RoundResult.Scores.Values.Any(c => c > 0))
    {
        AnsiConsole.MarkupLineInterpolated($"Round won by: [green]{string.Join(", ", args.RoundResult.Scores.Where(c => c.Value > 0).Select(c => c.Key))}[/]");
    }
    else
    {
        AnsiConsole.MarkupLine("[red]Round with no winners[/]");
    }

    var table = new Table { Width = 50 };
    table.AddColumn("Player");
    table.AddColumn("Engine");
    table.AddColumn("Points");
    
    foreach (var item in args.RoundResult.Scores)
    {
        table.AddRow(item.Key.ToString(), item.Key.GetType().Name, item.Value.ToString());
    }
    
    AnsiConsole.Write(table);

    AnsiConsole.MarkupLineInterpolated($"Standing points before the round: [yellow]{args.CurrentStandingPoints}[/]");
    AnsiConsole.MarkupLineInterpolated($"Standing points after the round: [yellow]{args.MatchResult.StandingPoints}[/]");

    AnsiConsole.WriteLine();
};

match.TrickCompleted += (_, args) =>
{
    AnsiConsole.MarkupLineInterpolated($"[bold]--- Trick {args.Trick} ---[/]");

    var table = new Table { Width = 75 };
    table.AddColumn("Player");
    table.AddColumn("Engine");
    table.AddColumn("Played card");
    table.AddColumn("Points");
    table.AddColumn("Winner");
    
    foreach (var item in args.PlayedCards)
    {
        var cells = new List<IRenderable>
        {
            new Text(item.Player.ToString()),
            new Text(item.Player.GetType().Name),
            new Text(item.Card.ToString()),
            new Text(item.Card.Value > 0 ? item.Card.Value.ToString() : string.Empty),
            new Text(item.Player == args.Winner ? "x" : string.Empty)
        };
        

        table.AddRow(new TableRow(cells));
    }
    
    AnsiConsole.Write(table);
    
    AnsiConsole.MarkupLineInterpolated($"Trick won by [orange3]{args.Winner}[/] for a total of [red]{args.PlayedCards.Sum(c => c.Card.Value)}[/] points");
    
    AnsiConsole.WriteLine();
};

match.MatchCompleted += (_, args) =>
{
    AnsiConsole.MarkupLine("[bold]--- Result of the match ---[/]");

    var table = new Table { Width = 50 };
    table.AddColumn("Player");
    table.AddColumn("Engine");
    table.AddColumn("Points");
    
    foreach (var item in args.MatchResults.Scores.OrderByDescending(p => p.Value))
    {
        table.AddRow(item.Key.ToString(), item.Key.GetType().Name, item.Value.ToString());
    }

    AnsiConsole.Write(table);
    
    AnsiConsole.MarkupLineInterpolated($"Rounds: [yellow]{args.Round}[/]");
};


match.AddPlayer(new DumbPlayer("Player 1"));
match.AddPlayer(new DumbPlayer("Player 2"));
match.AddPlayer(new DumbPlayer("Player 3"));
match.AddPlayer(new DumbPlayer("Player 4"));

match.Play(winningScore: 100);
