# Filippa

Filippa is a card game of the [Hearts group][1] that is very popular in my family. Probably, the most popular incarnation is [Microsoft Hearts][2] that has been part of Windows since Windows for Workgroup 3.1.

This repository contains an implementation of the game in .NET that is designed to allow different bots to play each other.

If you want to test your ability in creating a winning bot, all you need to do is:

- Fork the repository
- Add a class library in the `src/players` directory (more later) that
  - references the `Filippa` project
  - contains an implementation of the `IPlayerEngine` interface
- Add a reference to your class library in the `FilippaGame` project
- Modify the `Program.cs` file so that your player is added to the table
- Run the program and win the match

If you want, you can make a pull request to share the implementation of your bot!

Note: the repository already contains a dumb implementation of the `IPlayerEngine` interface. Also, in the `Filippa` project, you'll find an abstract class implementing the same interface. You can use this class if you want most to avoid most of the trivial work.

## Gameplay

The game is played by four players and uses a standard 52-card deck with cards ranking from Ace (high) down to the two.

Like in the [Black Lady][3] game, the Quenn of Spades and the cards of the heart suit are penalty cards. In Filippa, the penalty cards award a negative amount of points.

Each game is divided in rounds. Each round is played in tricks and negative and positive points are awarded when all tricks are played. The players who have no penalties at the end of the round are awarded with positive points.

The game ends when the first player reaches a set amount of positive points, typically 100, thus winning the match.

### Before the round starts

Each player receives thirteen cards and selects any of three to pass to the player playing after them.

### Tricks

The player at the left of the dealer leads to the first trick. Players must follow suit if able; othewise they may play any card. The trick is won by the highest card of the led suit and the trick winner leads to the next trick.

### End of the round

When all thirteen cards are played, the round ends and the scores are assigned to all players.

The following table can be used to summarize the penalties awarded by each card.

|Card|Penalty|
|-|-|
|Q♠️|-13|
|A♥️|-5|
|K♥️|-4|
|Q♥️|-3|
|J♥️|-2|
|2♥️ - 10♥️|-1 each|
|**Total**|**-36**|

All players with no penalties share 36 positive points.

If all players have penalties, the 36 positive points are stored and added to the prize pool of the next round.

Once all points have been assigned, if any player has passed the set threshold of positive points, the one with the highest amount, wins the match.

[1]: https://en.wikipedia.org/wiki/Hearts_group
[2]: https://en.wikipedia.org/wiki/Microsoft_Hearts
[3]: https://en.wikipedia.org/wiki/Black_Lady