using System.ComponentModel.DataAnnotations;
using Filippa;

var match = new FilippaMatch { PassCards = false };

match.AddPlayer(new FakePlayer("Player 1"));
match.AddPlayer(new FakePlayer("Player 2"));
match.AddPlayer(new FakePlayer("Player 3"));
match.AddPlayer(new FakePlayer("Player 4"));

var results = match.Play(winningScore: 200);

Console.WriteLine("--- Results ---");

foreach (var item in results.Scores)
{
    Console.WriteLine($"{item.Key.Name}: {item.Value}");
}

Console.WriteLine($"Sum: {results.Scores.Values.Sum()}");