using System.ComponentModel.DataAnnotations;
using Filippa;

var match = new FilippaMatch { PassCards = false, ShowPlayedCards = false};

match.AddPlayer(new DumbPlayer("Player 1"));
match.AddPlayer(new DumbPlayer("Player 2"));
match.AddPlayer(new DumbPlayer("Player 3"));
match.AddPlayer(new DumbPlayer("Player 4"));

var results = match.Play(winningScore: 200);

Console.WriteLine("--- Result of the match ---");

foreach (var item in results.Scores.OrderByDescending(p => p.Value))
{
    Console.WriteLine($"{item.Key.Name}: {item.Value}");
}